using System;
using System.Collections.Generic;
using System.Globalization;
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
    //http://www.csharp-examples.net/string-format-datetime/

    public class BorrowedItemController : Controller
    {
        UnitOfWork work = new UnitOfWork();
      //  [DynamicAuthorize]
        [Authorize]
        public ViewResult Index(string sortOrder, string currentFilter, string StudentStaffID, string PublicationYear, string ItemType, string searchString, string SubjectArea, string BookTitle, string AuthorName, int? page)
        {
            List<SelectListItem> theSubjectList = new List<SelectListItem>();
            theSubjectList.Add(new SelectListItem() { Text = "Choose...", Value = "" });
            List<SubjectArea> theSubjectA = work.SubjectAreaRepository.Get().ToList();
            foreach (var subjectA in theSubjectA)
            {
                theSubjectList.Add(new SelectListItem() { Text = subjectA.TheSubjectArea, Value = subjectA.TheSubjectArea });
            }
            ViewBag.SubjectArea = theSubjectList;

            if (Request.HttpMethod == "GET")
            {
                // searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

           //  List<BorrowedItem> books = work.BorrowedItemRepository.Get().OrderByDescending(a => a.TimeBorrowed).ToList();

            var books = from s in work.BorrowedItemRepository.Get().OrderByDescending(a => a.TimeBorrowed)
                        select s;

         //   books.OrderByDescending();
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    books = books.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
            //                           || s.FirstName.ToUpper().Contains(searchString.ToUpper()) || s.Middle.ToUpper().Contains(searchString.ToUpper()));
            //}



            if (!String.IsNullOrEmpty(BookTitle))
            {
                string bookt = BookTitle.ToLower().Trim();
                books = books.Where(s => s.ItemName.ToLower().Contains(bookt));
            }

            //if (!String.IsNullOrEmpty(AuthorName))
            //{
            //    string authorn = AuthorName.ToLower().Trim();
            //    // int theID = Convert.ToInt32(StudentIDString);
            //    books = books.Where(s => s.Aurthors.ToLower().Contains(authorn));
            //}

            //if (!String.IsNullOrEmpty(PublicationYear))
            //{
            //    string Publicationy = PublicationYear.ToLower().Trim();
            //    // int theID = Convert.ToInt32(StudentIDString);
            //    books = books.Where(s => s.Aurthors.ToLower().Contains(Publicationy));
            //}

            //if (!String.IsNullOrEmpty(SubjectArea))
            //{
            //    books = books.Where(s => s..Equals(SubjectArea));
            //}

            if (!String.IsNullOrEmpty(StudentStaffID))
            {
                string Publicationy = StudentStaffID.ToLower().Trim();
                // int theID = Convert.ToInt32(StudentIDString);
                books = books.Where(s => s.UserID.ToLower().Contains(Publicationy));
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



        [Authorize]
        public ViewResult Index2(string sortOrder, string currentFilter, string StudentStaffID, string PublicationYear, string ItemType, string searchString, string SubjectArea, string BookTitle, string AuthorName, int? page)
        {

            string UserID = User.Identity.Name;
            //List<SelectListItem> theSubjectList = new List<SelectListItem>();
            //theSubjectList.Add(new SelectListItem() { Text = "Choose...", Value = "" });
            //List<SubjectArea> theSubjectA = work.SubjectAreaRepository.Get().ToList();
            //foreach (var subjectA in theSubjectA)
            //{
            //    theSubjectList.Add(new SelectListItem() { Text = subjectA.TheSubjectArea, Value = subjectA.TheSubjectArea });
            //}
            //ViewBag.SubjectArea = theSubjectList;

            if (Request.HttpMethod == "GET")
            {
                // searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            // List<BorrowedItem> theBorrowedItems = work.BorrowedItemRepository.Get().OrderBy(a => a.TimeBorrowed).ToList();

            var books = from s in work.BorrowedItemRepository.Get(a => a.UserID.Equals(UserID)).OrderBy(a => a.TimeBorrowed)
                        select s;

            //if (!String.IsNullOrEmpty(BookTitle))
            //{
            //    string bookt = BookTitle.ToLower().Trim();
            //    books = books.Where(s => s.ItemName.ToLower().Contains(bookt));
            //}

            //if (!String.IsNullOrEmpty(StudentStaffID))
            //{
            //    string Publicationy = StudentStaffID.ToLower().Trim();
            //    // int theID = Convert.ToInt32(StudentIDString);
            //    books = books.Where(s => s.UserID.ToLower().Contains(Publicationy));
            //}
            //if (!String.IsNullOrEmpty(ItemType))
            //{
            //    // bool theValue = Convert.ToBoolean(ApprovedString);
            //    books = books.Where(s => s.ItemType == ItemType);
            //}


            int pageSize = 30;
            int pageNumber = (page ?? 1);



            return View("Index2", books.ToPagedList(pageNumber, pageSize));

        }


        //
        // GET: /BorrowedItem/Details/5
      //  [DynamicAuthorize]
        [Authorize]
        public ActionResult Details(int id)
        {
        BorrowedItem theBookLoggedAsBorrowed =    work.BorrowedItemRepository.GetByID(id);
            double days = (DateTime.Now - theBookLoggedAsBorrowed.TimeToBeReturned).TotalDays;
            ViewBag.Amount = 0;
            if (days > 0)
            {
                int theDays = Convert.ToInt16(days);
                ViewBag.Amount = theDays * 200;
            }
            return View(work.BorrowedItemRepository.GetByID(id));
        }

        //
        // GET: /BorrowedItem/Create
        [DynamicAuthorize]
        public ActionResult Create(int id)
        {
            Book theBook = work.BookRepository.GetByID(id);

            int theSubAreaID = theBook.SubjectAreaID;
            SubjectArea theSub = work.SubjectAreaRepository.GetByID(theSubAreaID);
            ViewBag.Tittle = theBook.ItemTitle;
            ViewBag.Authors = theBook.Aurthors;
            ViewBag.SubjectArea = theSub.TheSubjectArea;

            BorrowedItem theBorrowedBook = new BorrowedItem();
            theBorrowedBook.BookID = id;

            return View(theBorrowedBook);
        }

        //
        // POST: /BorrowedItem/Create

        [HttpPost]
        public ActionResult Create(BorrowedItem model)
        {
            model.TimeBorrowed = DateTime.Now;


            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    Book theBook = work.BookRepository.GetByID(model.BookID);
                    model.ItemName = theBook.ItemTitle;
                    model.ItemType = theBook.ItemType;
                    model.ISBN = theBook.ISBN + " | " + theBook.ISBN13;
                    int theQuantity = theBook.BookQuantity + 1;
                    theBook.BookQuantity = theQuantity;
                    work.BookRepository.Update(theBook);
                    work.BorrowedItemRepository.Insert(model);
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
        // GET: /BorrowedItem/Edit/5
        [DynamicAuthorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BorrowedItem/ReturnBook/5

        [HttpPost]
        public ActionResult ReturnBook(int id, int id2)
        {
            try
            {
                // TODO: Add update logic here
                Book theBorrowedBook = new Book();
                theBorrowedBook = work.BookRepository.GetByID(id);
                if (theBorrowedBook  == null)
                {

                }
                else
                {
                    
                    theBorrowedBook.BookQuantity = theBorrowedBook.BookQuantity - 1;

                    work.BookRepository.Update(theBorrowedBook);
                }
               

                BorrowedItem theBookLoggedAsBorrowed = work.BorrowedItemRepository.GetByID(id2);
      
              //  theBookLoggedAsBorrowed.TimeToBeReturned.da
                 //  theBookLoggedAsBorrowed.TimeToBeReturned

                double days = (DateTime.Now - theBookLoggedAsBorrowed.TimeToBeReturned).TotalDays;
                int theDays = Convert.ToInt16(days);
                if(days > 0)
                {
                    Finance theFi = new Finance();

                    theFi.AmountPaid =  Convert.ToInt32( days) * 200;
                    theFi.DatePaid = DateTime.Now;
                    theFi.PaidBy = theBookLoggedAsBorrowed.UserID;
                  //  theFi.

                    work.FinanceRepository.Insert(theFi);
                    work.Save();

                }
              
              
                work.BorrowedItemRepository.Delete(theBookLoggedAsBorrowed);
                work.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

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
        // GET: /BorrowedItem/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BorrowedItem/Delete/5

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
