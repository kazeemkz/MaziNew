using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using MvcMembership;
using MvcMembership.Settings;
using JobHustler.Models;
using eLibrary.DAL;
using eLibrary.Model;
using Microsoft.Ajax.Utilities;
using System.Data.SqlClient;
using System.Data;
//using SampleWebsite.Mvc3.Areas.MvcMembership.Models.UserAdministration;

namespace JobHustler.Controllers
{
    // [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin,Adim")]
    // [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin")] // allows access if you're the only user, only validates role if role provider is enabled
    public class UserAdministrationController : Controller
    {
        // SelectList
        private const int PageSize = 30;
        private const string ResetPasswordBody = "Your new password is: ";
        private const string ResetPasswordSubject = "Your New Password";
        private readonly IRolesService _rolesService;
        private readonly ISmtpClient _smtpClient;
        private readonly IMembershipSettings _membershipSettings;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        UnitOfWork work = new UnitOfWork();

        public UserAdministrationController()
            : this(new AspNetMembershipProviderWrapper(), new AspNetRoleProviderWrapper(), new SmtpClientProxy())
        {
        }

        public UserAdministrationController(AspNetMembershipProviderWrapper membership, IRolesService roles, ISmtpClient smtp)
            : this(membership.Settings, membership, membership, roles, smtp)
        {
        }

        public UserAdministrationController(
            IMembershipSettings membershipSettings,
            IUserService userService,
            IPasswordService passwordService,
            IRolesService rolesService,
            ISmtpClient smtpClient)
        {
            _membershipSettings = membershipSettings;
            _userService = userService;
            _passwordService = passwordService;
            _rolesService = rolesService;
            _smtpClient = smtpClient;
        }
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin,Admin")]
        public ActionResult Index(int? page, string search, string Type, string Level, string Arm)
        {

            var user = from s in work.LibraryUserRepository.Get()
                       select s;

            // List<LibraryUser> userss = work.LibraryUserRepository.Get().ToList();
            // string[] level = {"JSS1", "JSS2", "JSS3", "SS1", "SS2", "SS3"};
            //  userss =  userss.Where(a => a.Level.Equals(level)).ToList();

            if (!String.IsNullOrEmpty(Type))
            {
                // string authorn = AuthorName.ToLower().Trim();
                // int theID = Convert.ToInt32(StudentIDString);
                if (Type == "Staff")
                {

                    user = user.Where(s => s.Level.Equals(Type));
                }

                if (Type == "NonTeaching-Staff")
                {

                    user = user.Where(s => s.Level.Equals(Type));
                }

                if (Type != "NonTeaching-Staff" && Type != "Staff")
                {
                    //  user.
                    user = user.Where(s => !s.Level.Contains("Staff"));
                    user = user.Where(s => !s.Level.Contains("NonTeaching-Staff"));
                    // user = user.Where(s => s.Level != "NonTeaching-Staff");

                }


            }
            List<LibraryUser> thLibUser = user.ToList();

            //IEnumerable<MembershipUser> memberList2 = new List<MembershipUser>();
            // List<MembershipUser> memberList = new List<MembershipUser>();
            //  foreach (var membershipUser in thLibUser)
            //  {
            //      string userName = membershipUser.UserName;
            //      Membership.GetUser(userName);
            //      memberList.Add(Membership.GetUser(userName));
            //  }
            //  memberList2 = memberList as IEnumerable<MembershipUser>;


            //  PagedList<>
            //  memberList.ToPagedList(pageNumber, pageSize)

            //  memberList.
            //var theMem = Membership.GetAllUsers();
            //foreach (var u in theMem)
            //{

            //}

            //memberList = string.IsNullOrWhiteSpace(search)
            //    ? _userService.FindAll(page ?? 1, PageSize)
            //    : search.Contains("@")
            //        ? _userService.FindByEmail(search, page ?? 1, PageSize)
            //        : _userService.FindByUserName(search, page ?? 1, PageSize);

            var users = string.IsNullOrWhiteSpace(search)
                ? _userService.FindAll(page ?? 1, PageSize)
                : search.Contains("@")
                    ? _userService.FindByEmail(search, page ?? 1, PageSize)
                    : _userService.FindByUserName(search, page ?? 1, PageSize);

            if (!string.IsNullOrWhiteSpace(search) && users.Count == 1)
                return RedirectToAction("Details", new { id = users[0].ProviderUserKey.ToString() });

            return View(new IndexViewModel
                            {
                                Search = search,
                                Users = users,
                                Roles = _rolesService.Enabled
                                    ? _rolesService.FindAll()
                                    : Enumerable.Empty<string>(),
                                IsRolesEnabled = _rolesService.Enabled
                            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin,Admin")]
        public RedirectToRouteResult CreateRole(string id)
        {
            if (_rolesService.Enabled)
                _rolesService.Create(id);
            MyRole role = new MyRole();
            role.RoleName = id;
            role.Exam = "";
            role.Book = "";
            role.BorrowedItem = "";
            role.Upload = "";
            role.Shelf = "";
            role.Course = "";
            //role.Exam = "";
            //role.Staff = "";
            //role.Store = "";
            //role.StudentFees = "";
            //role.OnlineExam = "";
            work.MyRoleRepository.Insert(role);
            work.Save();
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin")]
        public RedirectToRouteResult DeleteRole(string id)
        {
            _rolesService.Delete(id);
            List<MyRole> theRole = work.MyRoleRepository.Get(a => a.RoleName.Equals(id)).ToList();
            work.MyRoleRepository.Delete(theRole[0]);
            work.Save();
            return RedirectToAction("Index");
        }

        public ViewResult Role(string id)
        {
            return View(new RoleViewModel
                            {
                                Role = id,
                                Users = _rolesService.FindUserNamesByRole(id)
                                                     .ToDictionary(
                                                        k => k,
                                                        v => _userService.Get(v)
                                                     )
                            });
        }

        public ViewResult Details(Guid id)
        {
            var user = _userService.Get(id);
            var userRoles = _rolesService.Enabled
                ? _rolesService.FindByUser(user)
                : Enumerable.Empty<string>();
            return View(new DetailsViewModel
                            {
                                CanResetPassword = _membershipSettings.Password.ResetOrRetrieval.CanReset,
                                RequirePasswordQuestionAnswerToResetPassword = _membershipSettings.Password.ResetOrRetrieval.RequiresQuestionAndAnswer,
                                DisplayName = user.UserName,
                                User = user,
                                Roles = _rolesService.Enabled
                                    ? _rolesService.FindAll().ToDictionary(role => role, role => userRoles.Contains(role))
                                    : new Dictionary<string, bool>(0),
                                IsRolesEnabled = _rolesService.Enabled,
                                Status = user.IsOnline
                                            ? DetailsViewModel.StatusEnum.Online
                                            : !user.IsApproved
                                                ? DetailsViewModel.StatusEnum.Unapproved
                                                : user.IsLockedOut
                                                    ? DetailsViewModel.StatusEnum.LockedOut
                                                    : DetailsViewModel.StatusEnum.Offline
                            });
        }

        public ViewResult Password(Guid id)
        {
            var user = _userService.Get(id);
            var userRoles = _rolesService.Enabled
                ? _rolesService.FindByUser(user)
                : Enumerable.Empty<string>();
            return View(new DetailsViewModel
            {
                CanResetPassword = _membershipSettings.Password.ResetOrRetrieval.CanReset,
                RequirePasswordQuestionAnswerToResetPassword = _membershipSettings.Password.ResetOrRetrieval.RequiresQuestionAndAnswer,
                DisplayName = user.UserName,
                User = user,
                Roles = _rolesService.Enabled
                    ? _rolesService.FindAll().ToDictionary(role => role, role => userRoles.Contains(role))
                    : new Dictionary<string, bool>(0),
                IsRolesEnabled = _rolesService.Enabled,
                Status = user.IsOnline
                            ? DetailsViewModel.StatusEnum.Online
                            : !user.IsApproved
                                ? DetailsViewModel.StatusEnum.Unapproved
                                : user.IsLockedOut
                                    ? DetailsViewModel.StatusEnum.LockedOut
                                    : DetailsViewModel.StatusEnum.Offline
            });
        }

        public ViewResult UsersRoles(Guid id)
        {
            var user = _userService.Get(id);
            var userRoles = _rolesService.FindByUser(user);
            return View(new DetailsViewModel
            {
                CanResetPassword = _membershipSettings.Password.ResetOrRetrieval.CanReset,
                RequirePasswordQuestionAnswerToResetPassword = _membershipSettings.Password.ResetOrRetrieval.RequiresQuestionAndAnswer,
                DisplayName = user.UserName,
                User = user,
                Roles = _rolesService.FindAll().ToDictionary(role => role, role => userRoles.Contains(role)),
                IsRolesEnabled = true,
                Status = user.IsOnline
                            ? DetailsViewModel.StatusEnum.Online
                            : !user.IsApproved
                                ? DetailsViewModel.StatusEnum.Unapproved
                                : user.IsLockedOut
                                    ? DetailsViewModel.StatusEnum.LockedOut
                                    : DetailsViewModel.StatusEnum.Offline
            });
        }
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin,Admin")]
        public ViewResult CreateUser()
        {
            var model = new CreateUserViewModel
                            {
                                InitialRoles = _rolesService.FindAll().ToDictionary(k => k, v => false)
                            };
            LibraryUser theUser = work.LibraryUserRepository.Get().Last();
            Int32 theUserName = 2000000 + theUser.LibraryUserID + 1;

            ViewBag.UserName = theUserName;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin,Admin")]
        public ActionResult CreateUser(CreateUserViewModel createUserViewModel)
        {

            // Int32 theUserCount = work.LibraryUserRepository.Get().Count();
            // Int32 theUserName = 2000000 + theUserCount;
            //  ViewBag.UserName = theUserName;
            // string stringUserName = Convert.ToString(theUserName);
            //  createUserViewModel.Username = stringUserName;
            if (!ModelState.IsValid)
                return View(createUserViewModel);

            try
            {
                if (createUserViewModel.Password != createUserViewModel.ConfirmPassword)
                    throw new MembershipCreateUserException("Passwords do not match.");

                if (createUserViewModel.InitialRoles != null)
                {
                    var rolesToAddUserTo = createUserViewModel.InitialRoles.Where(x => x.Value).Select(x => x.Key);
                    if (rolesToAddUserTo.Count() == 0)
                    {
                        LibraryUser theUser = work.LibraryUserRepository.Get().Last();
                        Int32 theUserName = 2000000 + theUser.LibraryUserID + 1;

                        ViewBag.UserName = theUserName;
                        ModelState.AddModelError("", "Select a Role for User!");
                        return View(createUserViewModel);
                    }
                }

                var user = _userService.Create(
                    createUserViewModel.Username,
                    createUserViewModel.Password,
                    createUserViewModel.Email,
                    null, null,
                    //createUserViewModel.PasswordQuestion,
                    //createUserViewModel.PasswordAnswer,
                    true);
                //if we got this far, no error then
                LibraryUser theLabUser = new LibraryUser
                {
                    DOB = createUserViewModel.DOB,
                    FirstName = createUserViewModel.FirstName,
                    LastName = createUserViewModel.LastName,
                    Level = createUserViewModel.Level,
                    UserName = createUserViewModel.Username,
                    LevelType = createUserViewModel.LevelType,
                    LevelTaught = createUserViewModel.LevelTaught,
                    LevelTaughtType = createUserViewModel.LevelTaughtType,
                    TelePhoneNumber = createUserViewModel.Telephone,
                    ClassTeacher = createUserViewModel.ClassTeacher,
                    UserType = createUserViewModel.UserType,

                };

                //   work.LevelRepository.


                if (createUserViewModel.InitialRoles != null)
                {
                    var rolesToAddUserTo = createUserViewModel.InitialRoles.Where(x => x.Value).Select(x => x.Key);
                    //if (rolesToAddUserTo.Count() == 0)
                    //{
                    //LibraryUser theUser = work.LibraryUserRepository.Get().Last();
                    //Int32 theUserName = 2000000 + theUser.LibraryUserID + 1;

                    //ViewBag.UserName = theUserName;
                    //ModelState.AddModelError("", "Select a Role for User!");
                    //return View(createUserViewModel);
                    // }
                    foreach (var role in rolesToAddUserTo)
                    {
                        _rolesService.AddToRole(user, role);
                        break;
                    }
                }

                work.LibraryUserRepository.Insert(theLabUser);
                work.Save();
                return RedirectToAction("Create", "UserPhoto", new { id = theLabUser.LibraryUserID });
                //return RedirectToAction("Details", new { id = user.ProviderUserKey });
            }
            catch (MembershipCreateUserException e)
            {
                LibraryUser theUser = work.LibraryUserRepository.Get().Last();
                Int32 theUserName = 2000000 + theUser.LibraryUserID + 1;

                ViewBag.UserName = theUserName;
                ModelState.AddModelError(string.Empty, e.Message);
                return View(createUserViewModel);
            }
        }

        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin,Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult Details(Guid id, string email, string comments)
        {

            var user = _userService.Get(id);
            user.Email = email;
            user.Comment = comments;
            _userService.Update(user);
            return RedirectToAction("Details", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin")]
        public RedirectToRouteResult DeleteUser(Guid id)
        {
            var user = _userService.Get(id);
            string theUserName = user.UserName;
            LibraryUser theUser = work.LibraryUserRepository.Get(a => a.UserName.Equals(theUserName)).First();


            if (_rolesService.Enabled)
                _rolesService.RemoveFromAllRoles(user);

            _userService.Delete(user);
            Membership.DeleteUser(user.UserName);
            //   _userService.

            work.LibraryUserRepository.Delete(theUser);
            work.Save();


            // DELETE FROM table_name WHERE some_column=some_value
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["eLibrary"].ConnectionString;
            SqlConnection conn = new System.Data.SqlClient.SqlConnection(con);
            SqlCommand updateCmd = new SqlCommand("DELETE FROM Users " +
                //"SET LastActivityDate = @LastActivityDate " +
      "WHERE UserName = @UserName", conn);

            //  updateCmd.Parameters.Add("@LastActivityDate", SqlDbType.DateTime).Value = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now).AddMinutes(-10);
            updateCmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = theUserName;
            //updateCmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = m_ApplicationName;
            conn.Open();
            updateCmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin")]
        public RedirectToRouteResult ChangeApproval(Guid id, bool isApproved)
        {
            var user = _userService.Get(id);
            user.IsApproved = isApproved;
            _userService.Update(user);
            return RedirectToAction("Details", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin")]
        public RedirectToRouteResult Unlock(Guid id)
        {
            _passwordService.Unlock(_userService.Get(id));
            return RedirectToAction("Details", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult ResetPassword(Guid id)
        {
            var user = _userService.Get(id);
            var newPassword = _passwordService.ResetPassword(user);

            var body = ResetPasswordBody + newPassword;
            var msg = new MailMessage();
            msg.To.Add(user.Email);
            msg.Subject = ResetPasswordSubject;
            msg.Body = body;
            _smtpClient.Send(msg);

            return RedirectToAction("Password", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin")]
        public RedirectToRouteResult ResetPasswordWithAnswer(Guid id, string answer)
        {
            var user = _userService.Get(id);
            var newPassword = _passwordService.ResetPassword(user, answer);

            var body = ResetPasswordBody + newPassword;
            var msg = new MailMessage();
            msg.To.Add(user.Email);
            msg.Subject = ResetPasswordSubject;
            msg.Body = body;
            _smtpClient.Send(msg);

            return RedirectToAction("Password", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin")]
        public RedirectToRouteResult SetPassword(Guid id, string password)
        {
            var user = _userService.Get(id);
            _passwordService.ChangePassword(user, password);

            //var body = ResetPasswordBody + password;
            //var msg = new MailMessage();
            //msg.To.Add(user.Email);
            //msg.Subject = ResetPasswordSubject;
            //msg.Body = body;
            //_smtpClient.Send(msg);

            return RedirectToAction("Password", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin")]
        public RedirectToRouteResult AddToRole(Guid id, string role)
        {
            var user = _userService.Get(id);
            _rolesService.RemoveFromAllRoles(user);
            _rolesService.AddToRole(_userService.Get(id), role);
            return RedirectToAction("UsersRoles", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AuthorizeUnlessOnlyUser(Roles = "SuperAdmin")]
        public RedirectToRouteResult RemoveFromRole(Guid id, string role)
        {
            _rolesService.RemoveFromRole(_userService.Get(id), role);
            return RedirectToAction("UsersRoles", new { id });
        }
    }
}