using eLibrary.DAL;
using eLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eLibrary.Controllers
{
    public class FinanceController : Controller
    {
        //
        // GET: /Finance/
        UnitOfWork work = new UnitOfWork();
        public ActionResult Index(string date, string dateto, string studentid)
        {
            //List<Level> theLevels = work.LevelRepository.Get().ToList();

            List<Finance> theAtten = new List<Finance>();
            theAtten = work.FinanceRepository.Get().OrderByDescending(a => a.DatePaid).ToList();

            if (!String.IsNullOrEmpty(date) && !(String.IsNullOrEmpty(dateto)))
            {
                DateTime from = Convert.ToDateTime(date);
                DateTime to = Convert.ToDateTime(dateto);
                theAtten = theAtten.Where(a => a.DatePaid.Date >= from.Date && a.DatePaid.Date <= to.Date).ToList();
            }

            if (!String.IsNullOrEmpty(date) && (String.IsNullOrEmpty(dateto)))
            {
                DateTime from = Convert.ToDateTime(date);
                // DateTime to = Convert.ToDateTime(dateto);
                theAtten = theAtten.Where(a => a.DatePaid.Date == from.Date).ToList();
            }

            if (!String.IsNullOrEmpty(studentid))
            {
                theAtten = theAtten.Where(a => a.PaidBy == studentid).ToList();
            }


            //if (!String.IsNullOrEmpty(arm))
            //{
            //    theAtten = theAtten.Where(a => a.arm == arm).ToList();
            //}

           
            //if ((String.IsNullOrEmpty(date)) && (String.IsNullOrEmpty(dateto)) && (String.IsNullOrEmpty(studentid)))
            //{
            //    //theAtten = theAtten.Where(s => s.arm == arm).ToList();
            //    //DateTime d = Convert.ToDateTime(date);
            //    theAtten = null;// theAtten.Where(s => s.DateTaken.Date == d.Date).ToList();
            //    ViewBag.Count = 0; //; theAtten.Count();

            //    return View(theAtten);

            //}



            //if (!String.IsNullOrEmpty(SexString))
            //{
            //    students = students.Where(s => s.Sex.Equals(SexString));
            //}


            if (!String.IsNullOrEmpty(date))
            {
                //  DateTime d = Convert.ToDateTime(date);
                //  theAtten = theAtten.Where(s => s.DateTaken.Date == d.Date).ToList();
            }


            ViewBag.Count = theAtten.Count();
            double total = 0;
            if (theAtten.Count > 0)
            {
                foreach (Finance f in theAtten)
                {
                    total = total + f.AmountPaid;
                }
            }
            ViewBag.Amount = total;
            return View(theAtten);
        }

        //
        // GET: /Finance/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Finance/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Finance/Create

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
        // GET: /Finance/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Finance/Edit/5

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
        // GET: /Finance/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Finance/Delete/5

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
