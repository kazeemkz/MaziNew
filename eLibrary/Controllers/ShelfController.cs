using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLibrary.DAL;
using eLibrary.Model;
using eLibrary.Models;

namespace eLibrary.Controllers
{
    [DynamicAuthorize]
    public class ShelfController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        //
        // GET: /Shelf/

        public ActionResult Index()
        {
            List<Shelf> theShelves = work.ShelfRepository.Get().ToList();
            return View(theShelves);
        }

        //
        // GET: /Shelf/Details/5

        public ActionResult Details(int id)
        {
            Shelf theShelf = work.ShelfRepository.GetByID(id);

            List<Row> theRows = work.RowRepository.Get(a => a.ShelfID == id).ToList();

            ViewBag.Rows = theRows;
            return View(theShelf);
        }

        //
        // GET: /Shelf/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Shelf/Create

        [HttpPost]
        public ActionResult Create(Shelf theModel)
        {
            try
            {
                // TODO: Add insert logic here

                string shelfName = theModel.ShelfName.Trim();
                string theName = shelfName.ToLower();

                theModel.ShelfName = shelfName;
                if (ModelState.IsValid)
                {
                    UnitOfWork work2 = new UnitOfWork();
                    List<Shelf> theShelves = work2.ShelfRepository.Get(a => a.ShelfName.Equals(theName)).ToList();
                    if (theShelves.Count > 0)
                    {
                        ModelState.AddModelError("", "A Shelf Name of this name has been Created Earlier!");
                        return View("Create", theModel);
                    }
                    else
                    {
                        work.ShelfRepository.Insert(theModel);
                        work.Save();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Shelf/Edit/5

        public ActionResult Edit(int id)
        {
            Shelf theShelf = work.ShelfRepository.GetByID(id);
            return View(theShelf);
        }

        //
        // POST: /Shelf/Edit/5

        [HttpPost]
        public ActionResult Edit(Shelf model)
        {
            try
            {
                // TODO: Add update logic here
                string shelfName = model.ShelfName.Trim();
                string theName = shelfName.ToLower();

                model.ShelfName = shelfName;
                UnitOfWork work2 = new UnitOfWork();
                List<Shelf> theShelves = work2.ShelfRepository.Get(a => a.ShelfName.Equals(theName)).ToList();
                if (theShelves.Count > 1)
                {
                    ModelState.AddModelError("", "A Shelf Name of this name has been Created Earlier!");
                    return View("Create", model);
                }
                else
                {
                    work.ShelfRepository.Update(model);
                    work.Save();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Shelf/Delete/5

        public ActionResult Delete(int id)
        {
            Shelf theShelf = work.ShelfRepository.GetByID(id);
            return View(theShelf);
        }

        //
        // POST: /Shelf/Delete/5

        [HttpPost]
        public ActionResult Delete(Shelf model)
        {
            try
            {
                // TODO: Add delete logic here

                work.ShelfRepository.Delete(model);
                List<Book> theBooks = work.BookRepository.Get(a => a.Column.Row.ShelfID == model.ShelfID).ToList();

                if (theBooks.Count() > 0)
                {
                    Int32 id = theBooks[0].BookID;
                    List<Photo> thePhotos = work.PhotoRepository.Get(a => a.BookID == id).ToList();
                    if (thePhotos.Count() > 0)
                    {
                        Int32 theID = thePhotos[0].PhotoID;
                        UnitOfWork work2 = new UnitOfWork();
                        work2.PhotoRepository.Delete(theID);
                        work2.Save();
                    }


                }
                work.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
