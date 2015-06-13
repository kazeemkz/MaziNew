using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eLibrary.DAL;
using eLibrary.Model;
using eLibrary.Models;

namespace eLibrary.Controllers
{
    //[Authorize(Roles = "SuperAdmin")]
    public class MyRoleController : Controller
    {
        //
        // GET: /MyRole/

        UnitOfWork work = new UnitOfWork();

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MyRole/Details/5

        public ActionResult Details(int id)
        {
            //  work.MyRoleRepository.
            return View();
        }

        //
        // GET: /MyRole/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MyRole/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MyRole/Edit/5

        public ActionResult Edit(string roleName)
        {

            List<MyRole> theRoles = work.MyRoleRepository.Get(a => a.RoleName.Equals(roleName)).ToList();
            MyRole theRole = theRoles[0];
            //      public int MyRoleID { get; set; }
            //public string RoleName { get; set; }
            //public string Upload { get; set; }
            //public string Exam { get; set; }
            //public string Book { get; set; }
            //public string BorrowedItem { get; set; }

            string[] UploadActivities = theRole.Upload.Split('-');

            string[] ExamActivities = theRole.Exam.Split('-');
            string[] BookActivities = theRole.Book.Split('-');

            string[] BorrowedItemActivities = theRole.BorrowedItem.Split('-');
            string[] ShelfActivities = theRole.Shelf.Split('-');
            string[] CourseActivities = theRole.Course.Split('-');
            //string[] StudentFeesActivities = theRole.StudentFees.Split('-');
            //string[] StoreActivities = theRole.Store.Split('-');
            //string[] StaffActivities = theRole.Staff.Split('-');
            //string[] OnlineExamActivities = theRole.OnlineExam.Split('-');

            List<string> uploadActivities = new List<string>();
            List<string> examActivities = new List<string>();
            List<string> bookActivities = new List<string>();
            List<string> borrowedItemActivites = new List<string>();
            List<string> shelfActivites = new List<string>();
            List<string> courseActivities = new List<string>();
            //List<string> subjectActivites = new List<string>();
            //List<string> classSubjectActivites = new List<string>();
            //List<string> examActivites = new List<string>();
            //List<string> staffActivities = new List<string>();
            //List<string> onlineexamActivities = new List<string>();

            foreach (var role in UploadActivities)
            {
                uploadActivities.Add(role);
            }

            foreach (var role in CourseActivities)
            {
                courseActivities.Add(role);
            }

            foreach (var role in ShelfActivities)
            {
                shelfActivites.Add(role);
            }
            foreach (var role in ExamActivities)
            {
                examActivities.Add(role);
            }

            foreach (var role in BookActivities)
            {
                bookActivities.Add(role);
            }

            foreach (var role in BorrowedItemActivities)
            {
                borrowedItemActivites.Add(role);
            }

            //foreach (var role in LevelActivities)
            //{
            //    levelActivities.Add(role);
            //}

            //foreach (var role in ExamActivities)
            //{
            //    examActivites.Add(role);
            //}

            //foreach (var role in ClassSubjectActivities)
            //{
            //    classSubjectActivites.Add(role);
            //}


            //List<string> uploadActivities = new List<string>();
            //List<string> examActivities = new List<string>();
            //List<string> bookActivities = new List<string>();
            //List<string> borrowedItemActivites = new List<string>();

            //PopulateStudentActivity(theRole, studentActivites, "Student");
            PopulateActivity(theRole, uploadActivities, "Upload");
            PopulateActivity(theRole, examActivities, "Exam");
            PopulateActivity(theRole, bookActivities, "Book");
            PopulateActivity(theRole, borrowedItemActivites, "BorrowedItem");
            PopulateActivity(theRole, shelfActivites, "Shelf");
            PopulateActivity(theRole, courseActivities, "Course");
            
            return View(theRole);
            // return View();
        }

        private void PopulateActivity(MyRole instructo, List<string> roles, string type)
        {

            var allActivites = new List<string>() { "List", "Create", "Edit", "Delete", "Details" };
            var viewModel = new List<ActivityData>();
            foreach (var activity in allActivites)
            {
                viewModel.Add(new ActivityData
                {

                    // ActivityID = course.SubjectID,
                    Name = activity + " " + type,
                    Assigned = roles.Contains(activity)
                });
            }
            if (type.Equals("Upload"))
            {
                ViewBag.Upload = viewModel;
            }
            if (type.Equals("Exam"))
            {
                ViewBag.Exam = viewModel;
            }
            if (type.Equals("Book"))
            {
                ViewBag.Book = viewModel;
            }
            if (type.Equals("Shelf"))
            {
                ViewBag.Shelf = viewModel;
            }
            if (type.Equals("BorrowedItem"))
            {
                ViewBag.BorrowedItem = viewModel;
            }
            if (type.Equals("Course"))
            {
                ViewBag.Course = viewModel;
            }
            //if (type.Equals("Exam"))
            //{
            //    ViewBag.Exam = viewModel;
            //}
            //if (type.Equals("ClassSubject"))
            //{
            //    ViewBag.ClassSubject = viewModel;
            //}
            //if (type.Equals("Student"))
            //{
            //    ViewBag.Student = viewModel;
            //}
            //if (type.Equals("Subject"))
            //{
            //    ViewBag.Subject = viewModel;
            //}

            //if (type.Equals("OnlineExam"))
            //{
            //    ViewBag.OnlineExam = viewModel;
            //}
            //  ViewBag.Subject = viewModel;
        }
        //private void PopulateStudentActivity(MyRole instructo, List<string> roles)
        //{

        //    var allActivites = new List<string>() { "List", "Create", "Edit", "Delete" };
        //    var viewModel = new List<RoleActivityDataStudent>();
        //    foreach (var activity in allActivites)
        //    {
        //        viewModel.Add(new RoleActivityDataStudent
        //        {

        //            // ActivityID = course.SubjectID,
        //            Name = activity + " Student",
        //            Assigned = roles.Contains(activity)
        //        });
        //    }
        //    ViewBag.Student = viewModel;
        //}

        //
        // POST: /MyRole/Edit/5

        [HttpPost]
        public ActionResult Edit(MyRole model, string[] selectedCourses)
        {
            try
            {
               
                //List<string> uploadActivities = new List<string>();
                //List<string> examActivities = new List<string>();
                //List<string> bookActivities = new List<string>();
                //List<string> borrowedItemActivites = new List<string>();

                StringBuilder uploadActivities = new StringBuilder();
                StringBuilder examActivities = new StringBuilder();
                StringBuilder bookActivities = new StringBuilder();
                StringBuilder borrowedItemActivites = new StringBuilder();
                StringBuilder shelfActivites = new StringBuilder();
                StringBuilder courseActivites = new StringBuilder();

                              // strinb  studentActivites = "";
                foreach (var activities in selectedCourses)
                {
                    String[] fakeActivity = activities.Split(' ');
                    string theac = fakeActivity[1];

                    if (activities.EndsWith("Course"))
                    {
                        String[] realActivity = activities.Split(' ');
                        courseActivites.Append(realActivity[0]);
                        courseActivites.Append("-");
                    }

                    if (activities.EndsWith("Upload"))
                    {
                        String[] realActivity = activities.Split(' ');
                        uploadActivities.Append(realActivity[0]);
                        uploadActivities.Append("-");
                    }

                    if (activities.EndsWith("Shelf"))
                    {
                        String[] realActivity = activities.Split(' ');
                        shelfActivites.Append(realActivity[0]);
                        shelfActivites.Append("-");
                    }
                    if (activities.EndsWith("Exam"))
                    {
                        String[] realActivity = activities.Split(' ');
                        examActivities.Append(realActivity[0]);
                        examActivities.Append("-");
                    }


                    if (activities.EndsWith("Book"))
                    {

                        String[] realActivity = activities.Split(' ');
                        bookActivities.Append(realActivity[0]);
                        // subjectActivites.Append(activities);
                        bookActivities.Append("-");
                    }

                    if (activities.EndsWith("BorrowedItem"))
                    {

                        String[] bbActivity = activities.Split(' ');
                        borrowedItemActivites.Append(bbActivity[0]);
                        // subjectActivites.Append(activities);
                        borrowedItemActivites.Append("-");
                    }


                    //if (activities.EndsWith("Store"))
                    //{

                    //    String[] storeActivity = activities.Split(' ');
                    //    storeActivities.Append(storeActivity[0]);
                    //    // subjectActivites.Append(activities);
                    //    storeActivities.Append("-");
                    //}

                    //if (activities.EndsWith("StudentFees"))
                    //{

                    //    String[] studentFeesActivity = activities.Split(' ');
                    //    studentFeesActivities.Append(studentFeesActivity[0]);
                    //    // subjectActivites.Append(activities);
                    //    studentFeesActivities.Append("-");
                    //}

                    //if (activities.EndsWith("Level"))
                    //{

                    //    String[] levelActivity = activities.Split(' ');
                    //    levelActivities.Append(levelActivity[0]);
                    //    // subjectActivites.Append(activities);
                    //    levelActivities.Append("-");
                    //}


                    //if (activities.EndsWith("Exam") && theac == "Exam")
                    //{

                    //    String[] examlActivity = activities.Split(' ');
                    //    examActivites.Append(examlActivity[0]);
                    //    // subjectActivites.Append(activities);
                    //    examActivites.Append("-");
                    //}


                    //if (activities.EndsWith("ClassSubject"))
                    //{

                    //    String[] classSubjectlActivity = activities.Split(' ');
                    //    classSubjectActivites.Append(classSubjectlActivity[0]);
                    //    // subjectActivites.Append(activities);
                    //    classSubjectActivites.Append("-");
                    //}

                    //PopulateActivity(theRole, staffActivities, "Staff");
                    //PopulateActivity(theRole, storeActivities, "Store");
                    //PopulateActivity(theRole, studentFeesActivities, "StudentFees");
                    //PopulateActivity(theRole, levelActivities, "Level");
                    //PopulateActivity(theRole, examActivites, "Exam");
                    //PopulateActivity(theRole, classSubjectActivites, "ClassSubject");
                    //PopulateActivity(theRole, studentActivites, "Student");
                    //PopulateActivity(theRole, subjectActivites, "Subject");
                }

          
                model.Upload = Convert.ToString(uploadActivities);
                model.Exam = Convert.ToString(examActivities);
                model.Book = Convert.ToString(bookActivities);
                model.BorrowedItem = Convert.ToString(borrowedItemActivites);
                model.Shelf = Convert.ToString(shelfActivites);
                model.Course = Convert.ToString(courseActivites);

                //model.Store = Convert.ToString(storeActivities);
                //model.Staff = Convert.ToString(staffActivities);
                //model.Subject = Convert.ToString(subjectActivites);
                //model.Student = Convert.ToString(studentActivites);
                //model.OnlineExam = Convert.ToString(onlineexamActivities);
                // TODO: Add update logic here

                work.MyRoleRepository.Update(model);
                work.Save();

                return RedirectToAction("Index", "UserAdministration");
            }
            catch
            {
                return View();
            }
        }

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //
        // GET: /MyRole/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MyRole/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
