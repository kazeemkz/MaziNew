using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLibrary.DAL;
using eLibrary.Model;
using PagedList;
using PagedList.Mvc;

namespace eLibrary.Controllers
{
    public class LibraryUserController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        //
        // GET: /LibraryUser/
        //,   public ViewResult Index(string sortOrder, string Shelf, string currentFilter, string PublicationYear, string ItemType, string searchString, string SubjectArea, string BookTitle, string AuthorName, int? page)
        public ActionResult Index(string sortOrder, string currentFilter, string Type, string UserName, string Level, string Arm, int? page)
        {
            var LibraryUser = from s in work.LibraryUserRepository.Get()
                              select s;

            if (!String.IsNullOrEmpty(UserName))
            {

                // LibraryUser = LibraryUser.Where(s => s.UserType.Equals(Type) && s.UserType.Equals("Student"));
                LibraryUser = LibraryUser.Where(s => s.UserName.Contains(UserName));

            }

            if (!String.IsNullOrEmpty(Type))
            {
               
                LibraryUser = LibraryUser.Where(s => s.UserType.Equals(Type));
            }

          

            if (!String.IsNullOrEmpty(Level))
            {
                if (!String.IsNullOrEmpty(Type))
                {
                    if (Type.Equals("Student"))
                    {
                        LibraryUser = LibraryUser.Where(s => s.UserType.Equals(Type));
                        LibraryUser = LibraryUser.Where(s => s.Level.Equals(Level));
                    }
                    if (Type.Equals("Staff"))
                    {
                        LibraryUser = LibraryUser.Where(s => s.UserType.Equals(Type));
                        LibraryUser = LibraryUser.Where(s => s.LevelTaught.Equals(Level));
                    }
                    if (Type.Equals("NonTeaching-Staff"))
                    {
                        LibraryUser = LibraryUser.Where(s => s.UserType.Equals(Type));
                    }

                    // LibraryUser = LibraryUser.Where(s => s.);
                }
                //else
                //{
                //    LibraryUser = LibraryUser.Where(s => s.UserType.Equals(Type));
                //}
            }

            if (!String.IsNullOrEmpty(Arm))
            {
                if (Type.Equals("Student"))
                {
                    // LibraryUser = LibraryUser.Where(s => s.UserType.Equals(Type) && s.UserType.Equals("Student"));
                    LibraryUser = LibraryUser.Where(s => s.LevelType.Equals(Arm));
                }
                if (Type.Equals("Staff"))
                {
                    // LibraryUser = LibraryUser.Where(s => s.UserType.Equals(Type) && s.UserType.Equals("Staff"));
                    LibraryUser = LibraryUser.Where(s => s.LevelTaughtType.Equals(Level));
                }
                //if (Type.Equals("NonTeaching-Staff"))
                //{
                //    LibraryUser = LibraryUser.Where(s => s.UserType.Equals(Type) && s.UserType.Equals("NonTeaching-Staff"));
                //}
            }


            int pageSize = 30;
            int pageNumber = (page ?? 1);



            return View(LibraryUser.ToPagedList(pageNumber, pageSize));
            // return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public FileContentResult GetImage(int id)
        {
            //PrimarySchoolStudent theStudent = work.PrimarySchoolStudentRepository.GetByID(id);
            List<UserPhoto> thePhoto = work.UserPhotoRepository.Get(a => a.LibraryUserID == id).ToList();
            UserPhoto myPhoto = thePhoto[0];


            return File(myPhoto.PhotoImage, "image/png");

        }

        //
        // GET: /LibraryUser/Details/5

        public ActionResult Details(string id)
        {
            LibraryUser theUser = work.LibraryUserRepository.Get(a => a.UserName.Equals(id)).First();
            return View(theUser);
            // return View();
        }

        //
        // GET: /LibraryUser/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LibraryUser/Create

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
        // GET: /LibraryUser/Edit/5

        public ActionResult Edit(string id)
        {
            LibraryUser theUser = work.LibraryUserRepository.Get(a => a.UserName.Equals(id)).First();
            return View(theUser);
        }

        //
        // POST: /LibraryUser/Edit/5

        [HttpPost]
        public ActionResult Edit(LibraryUser model)
        {
            try
            {
                // TODO: Add update logic here

                work.LibraryUserRepository.Update(model);
                work.Save();

                return RedirectToAction("Index", "UserAdministration");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /LibraryUser/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /LibraryUser/Delete/5

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
