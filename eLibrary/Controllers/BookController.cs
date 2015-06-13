using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLibrary.DAL;
using eLibrary.Model;
using eLibrary.Models;
using PagedList;
using PagedList.Mvc;

namespace eLibrary.Controllers
{
    public class BookController : Controller
    {
        //
        // GET: /Book/
        UnitOfWork work = new UnitOfWork();


        //public ActionResult Index()
        //{
        //    List<Book> theBooks = work.BookRepository.Get().ToList();
        //    return View(theBooks);
        //}
        [Authorize]
        public ActionResult Index2(string sortOrder, string currentFilter, int? Rodid, int? Colid, int? page)
        {
            eLContext db = new eLContext();

            Column theCol = db.Columns.Include("Book").Where(a => a.ColumnID == Colid && a.RowID == Rodid).First();
            Row theRow = work.RowRepository.GetByID(Rodid);

            Shelf theSelf = work.ShelfRepository.GetByID(theRow.ShelfID);

            ViewBag.Self = theSelf.ShelfName;


            ViewBag.RowName = theRow.RowName;
          //  ViewBag.Self = theRow.Self.ShelfName;
            ViewBag.Column = theCol.ColumnName;
            // return View("Index2", theCol.Book);
            ViewBag.RowID = theRow.RowID;
            ViewBag.ColID = theCol.ColumnID;
            ViewBag.BookCount = theCol.Book.Count();

            int pageSize = 30;
            int pageNumber = (page ?? 1);



            return View("Index2", theCol.Book.ToPagedList(pageNumber, pageSize));
        }
        [Authorize]
        public ViewResult Index(string sortOrder, string Shelf, string currentFilter, string PublicationYear, string ItemType, string searchString, string SubjectArea, string BookTitle, string AuthorName, int? page)
        {
            List<SelectListItem> theSubjectList = new List<SelectListItem>();
            theSubjectList.Add(new SelectListItem() { Text = "Choose...", Value = "" });
            List<SubjectArea> theSubjectA = work.SubjectAreaRepository.Get().ToList();
            foreach (var subjectA in theSubjectA)
            {
                theSubjectList.Add(new SelectListItem() { Text = subjectA.TheSubjectArea, Value = subjectA.TheSubjectArea });
            }
            ViewBag.SubjectArea = theSubjectList;

            List<SelectListItem> theShelfList = new List<SelectListItem>();
            theShelfList.Add(new SelectListItem() { Text = "Choose...", Value = "" });
            List<Shelf> theShelf = work.ShelfRepository.Get().ToList();
            foreach (var sef in theShelf)
            {
                theShelfList.Add(new SelectListItem() { Text = sef.ShelfName, Value = sef.ShelfName });
            }
            ViewBag.Shelf = theShelfList;

            if (sortOrder == null && currentFilter == null && Shelf == null && PublicationYear == null && ItemType == null && searchString == null && SubjectArea == null && BookTitle == null && AuthorName == null)
            {
                List<Book> theBook = new List<Book>();

                int pageSizes = 30;
                int pageNumbers = (page ?? 1);



                return View(theBook.ToPagedList(pageNumbers, pageSizes));
                //  return View(theBook.ToPagedList(1, 1));
                // return View(theBook);
            }

            if (sortOrder == null && currentFilter == null && Shelf == "" && PublicationYear == "" && ItemType == "" && searchString == null && SubjectArea == "" && BookTitle == "" && AuthorName == "")
            {
                List<Book> theBook = new List<Book>();

                int pageSizess = 30;
                int pageNumberss = (page ?? 1);



                return View(theBook.ToPagedList(pageNumberss, pageSizess));
                //  return View(theBook.ToPagedList(1, 1));
                // return View(theBook);
            }
            //ItemType,SubjectArea,BookTitle,AuthorName,PublicationYear
            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            //ViewBag.ClassSortParm = sortOrder == "class" ? "class desc" : "class";
            //ViewBag.GenderSortParm = sortOrder == "gender" ? "gender desc" : "gender";




            if (Request.HttpMethod == "GET")
            {
                // searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var books = from s in work.BookRepository.Get().OrderBy(a => a.ItemTitle)
                        select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    books = books.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
            //                           || s.FirstName.ToUpper().Contains(searchString.ToUpper()) || s.Middle.ToUpper().Contains(searchString.ToUpper()));
            //}




            if (!String.IsNullOrEmpty(Shelf))
            {
                List<Book> theBooks = new List<Book>();
                Shelf sef = work.ShelfRepository.Get(a => a.ShelfName.Equals(Shelf)).First();
                // foreach (var thesef in sef)
                // {
                List<Row> theRows = work.RowRepository.Get(a => a.ShelfID == sef.ShelfID).ToList();
                foreach (var r in theRows)
                {
                    List<Column> theC = work.ColumnRepository.Get(a => a.RowID == r.RowID).ToList();
                    foreach (var c in theC)
                    {
                        List<Book> theBook = c.Book;
                        if (c.Book != null)
                        {
                            foreach (var b in theBook)
                            {
                                theBooks.Add(b);
                            }
                        }


                    }
                }
                // }
                books = theBooks;
                //books = books.Where(s => s.ItemTitle.ToLower().Contains(Shelf));
            }


            if (!String.IsNullOrEmpty(BookTitle))
            {
                string bookt = BookTitle.ToLower().Trim();
                books = books.Where(s => s.ItemTitle.ToLower().Contains(bookt));
            }
            //






            if (!String.IsNullOrEmpty(AuthorName))
            {
                string authorn = AuthorName.ToLower().Trim();
                // int theID = Convert.ToInt32(StudentIDString);
                books = books.Where(s => s.Aurthors.ToLower().Contains(authorn));
            }

            if (!String.IsNullOrEmpty(PublicationYear))
            {
                int Publicationy = Convert.ToInt16(PublicationYear);
                // int theID = Convert.ToInt32(StudentIDString);
                books = books.Where(s => s.YearPublished == (Publicationy));
            }

            if (!String.IsNullOrEmpty(SubjectArea))
            {
                SubjectArea theSubjectArea = work.SubjectAreaRepository.Get(a => a.TheSubjectArea == SubjectArea).First();
                books = books.Where(s => s.SubjectAreaID == (theSubjectArea.SubjectAreaID));
            }

            if (!String.IsNullOrEmpty(ItemType))
            {
                // bool theValue = Convert.ToBoolean(ApprovedString);
                books = books.Where(s => s.ItemType == ItemType);
            }


            int pageSize = 30;
            int pageNumber = (page ?? 1);



            return View(books.ToPagedList(pageNumber, pageSize));

        }


        //
        // GET: /Book/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {




            eLContext db = new eLContext();

            Book theBooks = db.Books.Include("Column").Where(a => a.BookID == id).First();

            SubjectArea theSubArea = work.SubjectAreaRepository.GetByID(theBooks.SubjectAreaID);
            // ViewBag.SubjectArea = theSubArea.TheSubjectArea; 
            //  theBooks.SubjectArea = theSubArea.TheSubjectArea;
            Column theColumn = theBooks.Column;
            ViewBag.Column = theBooks.Column.ColumnName;

            Column theColumns = db.Columns.Include("Row").Where(a => a.ColumnName.Equals(theColumn.ColumnName) && a.ColumnID == theColumn.ColumnID).First();

            ViewBag.Row = theColumns.Row.RowName;

            // return View("Index2", theCol.Book);

            // Row theBookRow  = work.RowRepository.Get(a=>a.)

            Row theRows = db.Rows.Include("Self").Where(a => a.RowName.Equals(theColumns.Row.RowName) && a.RowID == theColumns.Row.RowID).First();

            ViewBag.Shelf = theRows.Self.ShelfName;
            Book theBook = work.BookRepository.GetByID(id);
            ViewBag.RowID = theRows.RowID;
            ViewBag.ColID = theColumns.ColumnID;

            theBook.SubjectArea = theSubArea.TheSubjectArea;

            List<BorrowedItem> theItem = work.BorrowedItemRepository.Get(a => a.BookID == id).ToList();

            if (theItem.Count() > 0)
            {
                ViewBag.AvailableDate = theItem.OrderBy(a => a.TimeToBeReturned).First().TimeToBeReturned;
            }

            // ViewBag
            return View(theBook);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public FileContentResult GetImage(int id)
        {
            //PrimarySchoolStudent theStudent = work.PrimarySchoolStudentRepository.GetByID(id);
            //   Book theBook = work.BookRepository.GetByID(id);
            Photo thePhoto = work.PhotoRepository.Get(a => a.BookID == id).First();
            // Photo myPhoto = thePhoto[0];


            return File(thePhoto.FileData, "image/png");

        }

        //
        // GET: /Book/Create
        [DynamicAuthorize]
        public ActionResult Create(int Rodid, int Colid)
        {
            Row theRow = work.RowRepository.GetByID(Rodid);
            Column theCol = work.ColumnRepository.Get(a => a.ColumnID == Colid && a.RowID == Rodid).First();
            Book theBook = new Book();

            theBook.ColumnID = theCol.ColumnID;
            Shelf theSelf = work.ShelfRepository.GetByID(theRow.ShelfID);
            ViewBag.RowName = theRow.RowName;
            ViewBag.Self = theSelf.ShelfName; //theRow.Self.ShelfName;
            ViewBag.Column = theCol.ColumnName;


            List<SubjectArea> theSubjectss = work.SubjectAreaRepository.Get().ToList();
            List<SelectListItem> theSubjectAreas = new List<SelectListItem>();
            // theSubjectAreas.Add(new SelectListItem() { Text = "Not Applicable", Value = "Not Applicable" });

            foreach (var level in theSubjectss)
            {
                theSubjectAreas.Add(new SelectListItem() { Text = level.TheSubjectArea, Value = level.TheSubjectArea });
            }

            // theItem.Add(new SelectListItem() { Text = "PRIMART 1A", Value = "PRIMART 1A" });
            ViewBag.Subjects = theSubjectAreas;

            return View(theBook);
        }

        //
        // POST: /Book/Create

        [HttpPost]
        // public ActionResult Create(Book model, HttpPostedFileBase file
        public ActionResult Create(Book model)
        {
            model.DateCreated = DateTime.Now;
            try
            {
                // TODO: Add insert logic here
                List<SubjectArea> theSubAreas = work.SubjectAreaRepository.Get(a => a.TheSubjectArea.Equals(model.SubjectArea)).ToList();
                if (theSubAreas.Count > 0)
                {
                    model.SubjectAreaID = theSubAreas[0].SubjectAreaID;
                }

                if (ModelState.IsValid)
                {





                    work.BookRepository.Insert(model);
                    work.Save();

                }
                return RedirectToAction("Create", "Photo", new { id = model.BookID });

                // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]

        public ActionResult Create2(Book model, string ShelfName, string RowName, string ColumnName)
        {
            string columnName = ColumnName;//model.Column.ColumnName;

              eLContext db = new eLContext();
              Shelf she = db.Shelves.Where(a => a.ShelfName == ShelfName).First();
         //  Shelf she =   work.ShelfRepository.Get(a => a.ShelfName == ShelfName).First();

           Row theRows = work.RowRepository.Get(a => a.RowName == RowName && a.ShelfID == she.ShelfID).First();

            //  Row theRows = db.Rows.Where(a => a.ShelfID == she.ShelfID).First();

              Column theC = db.Columns.Where(a => a.RowID == theRows.RowID && a.ColumnName == ColumnName).First();
//Columns
           // string rowName = RowName; //model.Column.Row.RowName;
           // string shelfName = ShelfName; //model.Column.Row.Self.ShelfName;
            //    eLContext db = new eLContext();
          //  Column theColumns = work.ColumnRepository.Get(a => a.ColumnName.Equals(columnName) && a.Row.RowName.Equals(rowName) ).First();
          //  Row theRows = work.RowRepository.Get(a => a.RowName.Equals(rowName) && a.RowID == theColumns.RowID).First();

           // theRows.Self = she;
           // theColumns.Row = theRows;
           // theRows.ShelfID = she.ShelfID;
          //  theColumns.RowID = theRows.RowID;
          //  model.Column = theColumns;
         //   model.ColumnID = theColumns.ColumnID;
            model.ColumnID = theC.ColumnID;
            model.DateCreated = DateTime.Now;
            try
            {
                // TODO: Add insert logic here

                List<SubjectArea> theSubAreas = work.SubjectAreaRepository.Get(a => a.TheSubjectArea.Equals(model.SubjectArea)).ToList();
                if (theSubAreas.Count > 0)
                {
                    model.SubjectAreaID = theSubAreas[0].SubjectAreaID;
                }

                if (ModelState.IsValid)
                {


                    work.BookRepository.Insert(model);
                    work.Save();

                }
                return RedirectToAction("Create", "Photo", new { id = model.BookID });

                // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [DynamicAuthorize]
        public ActionResult Create2()
        {

            List<Shelf> theShelfs = work.ShelfRepository.Get().ToList();
            List<SelectListItem> shelfList = new List<SelectListItem>();


            foreach (var level in theShelfs)
            {
                Shelf theShelf = work.ShelfRepository.Get(a => a.ShelfName.Equals(level.ShelfName)).First();

                List<Row> theRowsinShelf = work.RowRepository.Get(a => a.ShelfID == theShelf.ShelfID).ToList();
                // theRowsinShelf.
                if (theRowsinShelf.Count > 0)
                {
                    List<Column> theC = new List<Column>();
                    foreach (var row in theRowsinShelf)
                    {
                        theC = work.ColumnRepository.Get(a => a.RowID == row.RowID).ToList();

                        if (theC.Count() > 0)
                        {
                            shelfList.Add(new SelectListItem() { Text = level.ShelfName, Value = level.ShelfName });
                            break;
                        }
                    }

                    //if (theC.Count() > 0)
                    //{
                    //    shelfList.Add(new SelectListItem() { Text = level.ShelfName, Value = level.ShelfName });
                    //}


                }
            }

            // theItem.Add(new SelectListItem() { Text = "PRIMART 1A", Value = "PRIMART 1A" });
            ViewBag.Shelf = shelfList;


            List<SubjectArea> theSubjectss = work.SubjectAreaRepository.Get().ToList();
            List<SelectListItem> theSubjectAreas = new List<SelectListItem>();
            // theSubjectAreas.Add(new SelectListItem() { Text = "Not Applicable", Value = "Not Applicable" });

            foreach (var level in theSubjectss)
            {
                theSubjectAreas.Add(new SelectListItem() { Text = level.TheSubjectArea, Value = level.TheSubjectArea });
            }

            // theItem.Add(new SelectListItem() { Text = "PRIMART 1A", Value = "PRIMART 1A" });
            ViewBag.Subjects = theSubjectAreas;

            return View("Create2");
        }

        //
        // GET: /Book/Edit/5
        [DynamicAuthorize]
        public ActionResult Edit(int id, int koko = 0)
        {

            ViewBag.koko = koko;
            Book theBook = work.BookRepository.GetByID(id);

            SubjectArea theSubArea = work.SubjectAreaRepository.GetByID(theBook.SubjectAreaID);

            List<SubjectArea> theSubjectss = work.SubjectAreaRepository.Get().ToList();
            List<SelectListItem> theSubjectAreas = new List<SelectListItem>();
            // theSubjectAreas.Add(new SelectListItem() { Text = "Not Applicable", Value = "Not Applicable" });

            theSubjectAreas.Add(new SelectListItem() { Text = theSubArea.TheSubjectArea, Value = theSubArea.TheSubjectArea });
            foreach (var level in theSubjectss)
            {
                theSubjectAreas.Add(new SelectListItem() { Text = level.TheSubjectArea, Value = level.TheSubjectArea });
            }



            // theItem.Add(new SelectListItem() { Text = "PRIMART 1A", Value = "PRIMART 1A" });
            ViewBag.Subjects = theSubjectAreas;

            //  string shelfName = theBook.Column.Row.Self.ShelfName;
            // Shelf ShelveToBeRemoved = work.ShelfRepository.Get(a => a.ShelfName.Equals(shelfName)).First();
            List<Shelf> theShelfs = work.ShelfRepository.Get().ToList();
            // theShelfs.Remove(ShelveToBeRemoved);
            List<SelectListItem> shelfList = new List<SelectListItem>();
            // theSubjectAreas.Add(new SelectListItem() { Text = "Not Applicable", Value = "Not Applicable" });

            foreach (var level in theShelfs)
            {
                Shelf theShelf = work.ShelfRepository.Get(a => a.ShelfName.Equals(level.ShelfName)).First();

                List<Row> theRowsinShelf = work.RowRepository.Get(a => a.ShelfID == theShelf.ShelfID).ToList();
                // theRowsinShelf.
                if (theRowsinShelf.Count > 0)
                {
                    List<Column> theC = new List<Column>();
                    foreach (var row in theRowsinShelf)
                    {
                        theC = work.ColumnRepository.Get(a => a.RowID == row.RowID).ToList();

                        if (theC.Count() > 0)
                        {
                            shelfList.Add(new SelectListItem() { Text = level.ShelfName, Value = level.ShelfName });
                            break;
                        }
                    }

                    //if (theC.Count() > 0)
                    //{
                    //    shelfList.Add(new SelectListItem() { Text = level.ShelfName, Value = level.ShelfName });
                    //}


                }
            }

            // theItem.Add(new SelectListItem() { Text = "PRIMART 1A", Value = "PRIMART 1A" });
            ViewBag.Shelf = shelfList;

            return View(theBook);

        }

        public ActionResult LoadShelfRowDefault(string shelf)
        {
            int BookID = 0;
            Book theBook = new Book();
            Row theRow = new Row();
            string[] theVale = shelf.Split(':');
            string BookIDstring = theVale[1];
            string shelfName = theVale[0];
            if (BookIDstring != "undefined")
            {
                BookID = Convert.ToInt32(BookIDstring);
            }
            //  List<Level> theLevel = work1.LevelRepository.Get(a => a.Name.Equals(level)).ToList();
            List<Row> shelfRows = work.RowRepository.Get(a => a.Self.ShelfName.Equals(shelfName)).ToList();
            //List<Row> theRowWithColumns = new List<Row>();
            //foreach (var theRow in shelfRows)
            //{

            //    // if(shelfs.)

            //}
            List<SelectListItem> cours = new List<SelectListItem>();
            eLContext db = new eLContext();
            if (BookIDstring != "undefined")
            {
                theBook = db.Books.Include("Column").Where(a => a.BookID == BookID).First();
                Column theColumn = db.Columns.Include("Row").Where(a => a.ColumnID == theBook.Column.ColumnID && a.ColumnName.Equals(theBook.Column.ColumnName)).First();
                theRow = db.Rows.Include("Self").Where(a => a.RowID == theColumn.Row.RowID).First();
                // work.BookRepository.GetByID

                cours.Add(new SelectListItem { Text = theRow.RowName, Value = theRow.RowName });
            }
            //else
            //{
            //    theRow = db.Rows.Include("Self").Where(a => a.Self.ShelfName.Equals(shelfName)).First();
            //}

            foreach (var co in shelfRows)
            {
                List<Column> theCol = work.ColumnRepository.Get(a => a.Row.RowName.Equals(co.RowName)).ToList();
                if (theCol.Count() > 0)
                {
                    //check if no book is selected, that is it in edit mode
                    //   if (BookIDstring != "undefined")
                    //  {
                    if (!(theRow.RowID == co.RowID))
                    {
                        cours.Add(new SelectListItem { Text = co.RowName, Value = co.RowName });
                    }
                    //  }
                }
            }

            //  if()
            return Json(cours, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadShelfRow(string shelf)
        {
            //  List<Level> theLevel = work1.LevelRepository.Get(a => a.Name.Equals(level)).ToList();
            List<Row> shelfRows = work.RowRepository.Get(a => a.Self.ShelfName.Equals(shelf)).ToList();
            List<Row> theRowWithColumns = new List<Row>();
            foreach (var theRow in shelfRows)
            {

                // if(shelfs.)

            }
            List<SelectListItem> cours = new List<SelectListItem>();

            foreach (var co in shelfRows)
            {
                List<Column> theCol = work.ColumnRepository.Get(a => a.Row.RowName.Equals(co.RowName)).ToList();
                if (theCol.Count() > 0)
                {
                    //foreach (var col in theCol)
                    //cours.Add(new SelectListItem { Text = co.RowName, Value = co.RowName });
                    cours.Add(new SelectListItem { Text = co.RowName, Value = co.RowName });
                }
            }

            //  if()
            return Json(cours, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadfRowColumsDefault(string theShelfRows)
        {
            Book theBook = new Book();
            int bookID = 0;
            List<SelectListItem> cours = new List<SelectListItem>();
            string[] shelfRow = theShelfRows.Split(':');
            string row = shelfRow[0];
            string shelfName = shelfRow[1];
            string bookIDString = shelfRow[2];

            if (bookIDString != "undefined")
            {
                bookID = Convert.ToInt32(bookIDString);
            }

            eLContext db = new eLContext();

            if (bookIDString != "undefined")
            {
                theBook = db.Books.Include("Column").Where(a => a.BookID == bookID).First();
                cours.Add(new SelectListItem { Text = theBook.Column.ColumnName, Value = theBook.Column.ColumnName });
            }
            // work.BookRepository.GetByID



            //  List<Level> theLevel = work1.LevelRepository.Get(a => a.Name.Equals(level)).ToList();
            List<Column> colums = work.ColumnRepository.Get(a => a.Row.RowName.Equals(row) && a.Row.Self.ShelfName.Equals(shelfName)).ToList();

            if (bookIDString != "undefined")
            {
                foreach (var co in colums)
                {
                    if (!(theBook.Column.ColumnID == co.ColumnID))
                    {
                        cours.Add(new SelectListItem { Text = co.ColumnName, Value = co.ColumnName });
                    }
                }
            }
            else
            {
                //if we av no book id, we dont need finding the column it belongs, just add it to the list
                foreach (var co in colums)
                {

                    cours.Add(new SelectListItem { Text = co.ColumnName, Value = co.ColumnName });

                }
            }
            return Json(cours, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadfRowColums(string theShelfRows)
        {
            string[] shelfRow = theShelfRows.Split(':');
            string row = shelfRow[0];
            string shelfName = shelfRow[1];
            //  List<Level> theLevel = work1.LevelRepository.Get(a => a.Name.Equals(level)).ToList();
            List<Column> colums = work.ColumnRepository.Get(a => a.Row.RowName.Equals(row) && a.Row.Self.ShelfName.Equals(shelfName)).ToList();
            List<SelectListItem> cours = new List<SelectListItem>();

            foreach (var co in colums)
            {
                cours.Add(new SelectListItem { Text = co.ColumnName, Value = co.ColumnName });
            }
            return Json(cours, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Book/Edit/5

        [HttpPost]
        public ActionResult Edit(Book model, int koko = 0)
        {
            string columnName = model.Column.ColumnName;

            //    model.Column.ColumnName;
            //  string rowName = model.Column.Row.RowName;


            string rowName = model.Column.Row.RowName;
            string shelfName = model.Column.Row.Self.ShelfName;
            Shelf theShelf = work.ShelfRepository.Get(a => a.ShelfName == shelfName).First();
            int theShelfID = theShelf.ShelfID;
            //    eLContext db = new eLContext();
            Row theRows = work.RowRepository.Get(a => a.ShelfID == theShelfID && a.RowName == rowName).First();
            int theRowId = theRows.RowID;
            //   Row theRows = work.RowRepository.Get(a => a.RowName.Equals(rowName) && a.RowID == theColumns.RowID).First();
            Column theColumns = work.ColumnRepository.Get(a => a.ColumnName.Equals(columnName) && a.Row.RowID == theRowId).First();


            theColumns.Row = theRows;
            model.Column = theColumns;
            model.ColumnID = theColumns.ColumnID;

            // DateTime theDate = model.DateCreated;
            //  model.DateCreated = DateTime.Now;
            try
            {

                List<SubjectArea> theSubAreas = work.SubjectAreaRepository.Get(a => a.TheSubjectArea.Equals(model.SubjectArea)).ToList();

                if (theSubAreas.Count > 0)
                {
                    model.SubjectAreaID = theSubAreas[0].SubjectAreaID;
                }
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    //  UnitOfWork work2 = new UnitOfWork();
                    // work2.BookRepository.Update(model);
                    // work.Save();
                    work.BookRepository.Update(model);
                    work.Save();
                }
                if (koko == 1)
                {
                    return RedirectToAction("Index", "Shelf");
                }
                else
                {
                    return RedirectToAction("Index");
                }


            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Book/Delete/5
        [DynamicAuthorize]
        public ActionResult Delete(int id)
        {
            Book theBook = work.BookRepository.GetByID(id);


            Column col = work.ColumnRepository.GetByID(theBook.ColumnID);

            Row r = work.RowRepository.GetByID(col.RowID);
            col.Row = r;
            theBook.Column = col;
            //  work.PhotoRepository.Delete()
            return View(theBook);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost]
        public ActionResult Delete(Book model)
        {
            try
            {
                int roid = model.Column.Row.RowID;
                int col = model.ColumnID;

                Book theBook = work.BookRepository.GetByID(model.BookID);
                //  Index2(string sortOrder, string currentFilter, int? Rodid, int? Colid, int? page)
                // TODO: Add delete logic here

                List<Photo> thePhoto = work.PhotoRepository.Get(a => a.BookID == theBook.BookID).ToList();


                if (thePhoto.Count > 0)
                {
                    Int32 theID = thePhoto[0].PhotoID;
                    work.PhotoRepository.Delete(theID);
                }
                work.BookRepository.Delete(theBook);
                work.Save();

                return RedirectToAction("Index2", new { Rodid = roid, Colid = col });
            }
            catch
            {
                return View();
            }
        }
    }
}
