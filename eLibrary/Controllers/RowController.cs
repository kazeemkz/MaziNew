using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLibrary.DAL;
using eLibrary.Model;

namespace eLibrary.Controllers
{
    public class RowController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        //
        // GET: /Row/

        public ActionResult Index()
        {
            List<Row> theRows = work.RowRepository.Get().ToList();
            return View(theRows);
        }

        //
        // GET: /Row/Details/5

        public ActionResult Details(int id)
        {
            eLContext db = new eLContext();
            Row theRow = work.RowRepository.GetByID(id);

            Shelf theSelf = work.ShelfRepository.GetByID(theRow.ShelfID);

            ViewBag.SelfName = theSelf.ShelfName;

        //   Row theRow  = db.Rows.Include("Shelf").Where(a => a.RowID == id).First();
            //Shelf 
            List<Column> theColumns = db.Columns.Include("Book").Where(a => a.RowID == id).ToList();

          //  List<Column> theColumn = work.ColumnRepository.Get(a => a.RowID == id).ToList();
            ViewBag.Column = theColumns;

            //foreach (var col in theColumns)
            //{
               
            //}
            return View(theRow);
        }

        //
        // GET: /Row/Create

        public ActionResult Create(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                Row theRow = new Row();
                theRow.ShelfID = id;
                return View(theRow);
            }
        }

        //
        // POST: /Row/Create

        [HttpPost]
        public ActionResult Create(Row theModel)
        {
            try
            {
                string shelfName = theModel.RowName.Trim();
                string theName = shelfName.ToLower();

                theModel.RowName = shelfName;
                if (ModelState.IsValid)
                {
                    UnitOfWork work2 = new UnitOfWork();
                    List<Row> theColums = work2.RowRepository.Get(a => a.RowName.Equals(theName) && a.ShelfID == theModel.ShelfID).ToList();
                    if (theColums.Count > 0)
                    {
                        ModelState.AddModelError("", "A Row of this name has been Created Earlier!");
                        return View("Create", theModel);
                    }
                    else
                    {
                        work.RowRepository.Insert(theModel);
                        work.Save();
                    }
                }

                return RedirectToAction("Details", "Shelf", new {id =theModel.ShelfID });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Row/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Row/Edit/5

        [HttpPost]
        public ActionResult Edit(Row model)
        {
            try
            {
                // TODO: Add update logic here
                // TODO: Add update logic here
                string shelfName = model.RowName.Trim();
                string theName = shelfName.ToLower();

                model.RowName = shelfName;
                UnitOfWork work2 = new UnitOfWork();
                List<Row> theShelves = work2.RowRepository.Get(a => a.RowName.Equals(theName)).ToList();
                if (theShelves.Count > 1)
                {
                    ModelState.AddModelError("", "A Row of this name has been Created Earlier!");
                    return View("Create", model);
                }
                else
                {
                    work.RowRepository.Update(model);
                    work.Save();
                }

                return RedirectToAction("Details", "Shelf");
                // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Row/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Row/Delete/5

        [HttpPost]
        public ActionResult Delete(Row model)
        {
            int ShelfID = model.ShelfID;
            try
            {
                List<Book> theBooks = work.BookRepository.Get(a => a.Column.Row.RowID == model.RowID).ToList();

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



                // TODO: Add delete logic here
                work.RowRepository.Delete(model);
                work.Save();
                return RedirectToAction("Details", "Shelf", new { id = ShelfID });
                // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
