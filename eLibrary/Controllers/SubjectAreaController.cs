using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLibrary.DAL;
using eLibrary.Model;

namespace eLibrary.Controllers
{
    public class SubjectAreaController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        //
        // GET: /SubjectArea/

        public ActionResult Index()
        {

            return View(work.SubjectAreaRepository.Get().ToList());
        }

        //
        // GET: /SubjectArea/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SubjectArea/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SubjectArea/Create

        [HttpPost]
        public ActionResult Create(SubjectArea theModel)
        {
            try
            {
                // TODO: Add insert logic here

                string shelfName = theModel.TheSubjectArea.Trim();
                string theName = shelfName.ToLower();

                theModel.TheSubjectArea = shelfName;
                if (ModelState.IsValid)
                {
                    UnitOfWork work2 = new UnitOfWork();
                    List<SubjectArea> theShelves = work2.SubjectAreaRepository.Get(a => a.TheSubjectArea.Equals(theName)).ToList();
                    if (theShelves.Count > 0)
                    {
                        ModelState.AddModelError("", "A Subject Area of this name has been Created Earlier!");
                        return View("Create", theModel);
                    }
                    else
                    {
                        work.SubjectAreaRepository.Insert(theModel);
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
        // GET: /SubjectArea/Edit/5

        public ActionResult Edit(int id)
        {
            SubjectArea theArea = work.SubjectAreaRepository.GetByID(id);

            return View(theArea);
        }

        //
        // POST: /SubjectArea/Edit/5

        [HttpPost]
        public ActionResult Edit(SubjectArea model)
        {
            try
            {
                // TODO: Add update logic here
                string shelfName = model.TheSubjectArea.Trim();
                string theName = shelfName.ToLower();

                model.TheSubjectArea = shelfName;
                UnitOfWork work2 = new UnitOfWork();
                List<SubjectArea> theShelves = work2.SubjectAreaRepository.Get(a => a.TheSubjectArea.Equals(theName)).ToList();
                if (theShelves.Count > 1)
                {
                    ModelState.AddModelError("", "A Subject Area of this name has been Created Earlier!");
                    return View("Create", model);
                }
                else
                {
                    work.SubjectAreaRepository.Update(model);
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
        // GET: /SubjectArea/Delete/5

        public ActionResult Delete(int id)
        {
            SubjectArea theSub = work.SubjectAreaRepository.GetByID(id);
            return View(theSub);
        }

        //
        // POST: /SubjectArea/Delete/5

        [HttpPost]
        public ActionResult Delete(SubjectArea model)
        {
            try
            {
                SubjectArea theSubArea = work.SubjectAreaRepository.GetByID(model.SubjectAreaID);
              //  SubjectArea theRealSubject = theSubArea[0];
                List<Book> theBooks = work.BookRepository.Get(a => a.SubjectAreaID == theSubArea.SubjectAreaID).ToList();

                if (theBooks.Count() > 0)
                {


                    ModelState.AddModelError("", "Delete Books Belonging to this Subject Area First!");
                    model.TheSubjectArea = theSubArea.TheSubjectArea;
                    return View(model);

                }
                else
                {
                    // TODO: Add delete logic here
                    UnitOfWork work2 = new UnitOfWork();
                    work2.SubjectAreaRepository.Delete(model);
                    work2.Save();
                    return RedirectToAction("Index");
                }


            }
            catch
            {
                return View();
            }
        }
    }
}
