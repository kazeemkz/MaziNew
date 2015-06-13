using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLibrary.DAL;
using eLibrary.Model;

namespace eLibrary.Controllers
{

    public class UserPhotoController : Controller
    {
        //
        // GET: /UserPhoto/
        UnitOfWork work = new UnitOfWork();

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /UserPhoto/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /UserPhoto/Create
        [HttpPost]
        // public virtual ActionResult Snapshot(int id)
        public virtual ActionResult Snapshot(int id, string image)
        {
            List<UserPhoto> thePersonphotos = work.UserPhotoRepository.Get(a => a.LibraryUserID == id).ToList();



            if (thePersonphotos.Count == 0)
            {


                UserPhoto theUserpoto = new UserPhoto();
                theUserpoto.LibraryUserID = id;
                image = image.Substring("data:image/png;base64,".Length);
                var buffer = Convert.FromBase64String(image);
                // byte[] imageByte = buffer.ToArray();
                theUserpoto.PhotoImage = buffer.ToArray();
                work.UserPhotoRepository.Insert(theUserpoto);

                //
                //  theUserpoto.PhotoImage = imageByte.ToArray();
                //  work.PhotoRepository.Insert(theUserpoto);
                work.Save();
            }
            else
            {
                List<UserPhoto> theUserpoto = work.UserPhotoRepository.Get(a => a.LibraryUserID == id).ToList();
                UserPhoto theRealPhoto = theUserpoto[0];
                int id2 = theRealPhoto.UserPhotoID;
                work.UserPhotoRepository.Delete(id2);
                work.Save();

                image = image.Substring("data:image/png;base64,".Length);
                var buffer = Convert.FromBase64String(image);
                // byte[] imageByte = buffer.ToArray();
                theRealPhoto.PhotoImage = buffer.ToArray();
                work.UserPhotoRepository.Insert(theRealPhoto);
                work.Save();
            }

            return View("");
            // 
        }


        public ActionResult Create(int id = 0)
        {

            UserPhoto thePhoto = new UserPhoto();
            thePhoto.LibraryUserID = id;
            // ViewBag.ID = "Staff";

          
            return View("Create", thePhoto);
        }

        //
        // POST: /UserPhoto/Create

        [HttpPost]
        public ActionResult Create(UserPhoto model)
        {
            try
            {
                // TODO: Add insert logic here
                int UserID = model.LibraryUserID;
                //   List<Photo> theUserPhoto = work.PhotoRepository.Get(a=>a.PhotoID)
                //  return RedirectToAction("Index");
                // return RedirectToAction("Details", "PrimarySchoolStudent", new { id = model.UserID });

                LibraryUser thePerson = work.LibraryUserRepository.GetByID(UserID);


                return RedirectToAction("Index", "UserAdministration");

                //  return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserPhoto/Edit/5

        public ActionResult Edit(int id)
        {
            UserPhoto thePhoto = new UserPhoto();
            List<UserPhoto> thePotos = work.UserPhotoRepository.Get(a => a.LibraryUserID == id).ToList();

            if (thePotos.Count == 0)
            {
                return RedirectToAction("Create", new { id = id });
            }
            else
            {
                thePhoto = thePotos[0];
                return View("Edit", thePhoto);
            }
            // thePhoto.PersonID = id;
            // ViewBag.ID = "Staff";

        }

        //
        // POST: /UserPhoto/Edit/5

        [HttpPost]
        public ActionResult Edit(Photo model)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserPhoto/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UserPhoto/Delete/5

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
