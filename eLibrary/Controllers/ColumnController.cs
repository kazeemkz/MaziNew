using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLibrary.DAL;
using eLibrary.Model;

namespace eLibrary.Controllers
{
    public class ColumnController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        //
        // GET: /Column/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Column/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Column/Create

        public ActionResult Create(int id=0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                Column theColumn = new Column();
                theColumn.RowID = id;
                return View(theColumn);
            }
        }

        //
        // POST: /Column/Create

        [HttpPost]
        public ActionResult Create(Column model)
        {
            try
            {
                // TODO: Add insert logic here
                string shelfName = model.ColumnName.Trim();
                string theName = shelfName.ToLower();

                model.ColumnName = shelfName;
                if (ModelState.IsValid)
                {
                    UnitOfWork work2 = new UnitOfWork();
                    List<Column> theColums = work2.ColumnRepository.Get(a => a.ColumnName.Equals(theName) && a.RowID == model.RowID).ToList();
                    if (theColums.Count > 0)
                    {
                        ModelState.AddModelError("", "A Column of this name has been Created Earlier!");
                        return View("Create", model);
                    }
                    else
                    {
                        work.ColumnRepository.Insert(model);
                        work.Save();
                    }
                }

                return RedirectToAction("Details", "Row", new { id= model.RowID});
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Column/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Column/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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
        // GET: /Column/Delete/5

        public ActionResult Delete(int id)
        {
         Column theColumn =   work.ColumnRepository.GetByID(id);

         Row theRow = work.RowRepository.GetByID(theColumn.RowID);
         Shelf theShelf = work.ShelfRepository.GetByID(theRow.ShelfID);

         theRow.Self = theShelf;

         theColumn.Row = theRow;

         return View(theColumn);
        }

        //
        // POST: /Column/Delete/5

        [HttpPost]
        public ActionResult Delete(Column model)
        {
            try
            {
                int rowID = model.RowID;
                // TODO: Add delete logic here


                List<Book> theBooks = work.BookRepository.Get(a => a.Column.ColumnID == model.ColumnID).ToList();

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
                work.ColumnRepository.Delete(model);
                work.Save();

                return RedirectToAction("Details", "Row", new {id=rowID });
            }
            catch
            {
                return View();
            }
        }
    }
}
