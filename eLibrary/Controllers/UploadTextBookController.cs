using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLibrary.DAL;
using eLibrary.Model;
using eLibrary.Models;
//using Spire.Doc;

namespace eLibrary.Controllers
{
    [DynamicAuthorize]
    public class UploadTextBookController : Controller
    {
        //
        UnitOfWork work = new UnitOfWork();
        string SaveLocationPdf = null;
        // GET: /UploadTextBook/
        public string ParentNameC = "";


        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /UploadTextBook/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /UploadTextBook/Create

        public ActionResult Create(int id=0)
        {
            ViewBag.Success = id.ToString();
            return View();
        }

        //
        // POST: /UploadTextBook/Create
        [HttpPost]
        public ActionResult Create(UploadTextBookViewModel model, IEnumerable<HttpPostedFileBase> file)
        {
            try
            {
                string TheFileName = System.IO.Path.GetFileName(Request.Files[0].FileName);

                if (!(TheFileName.EndsWith(".pdf")))
                {
                    ModelState.AddModelError("", "The Input File is not a .pdf file!");
                    return View(model);
                }
                if (ModelState.IsValid)
                {
                    Level SubjectLevel = work.LevelRepository.Get(a => a.Name.Equals(model.Level)).First();
                    ////create byte array of size equal to file input stream
                    //byte[] fileData = new byte[Request.Files[0].InputStream.Length];
                    //// fileData
                    ////add file input stream into byte array
                    //Request.Files[0].InputStream.Read(fileData, 0, Convert.ToInt32(Request.Files[0].InputStream.Length));
                    //fileData.ToArray();


                    //string physicalPath = HttpContext.Server.MapPath("../") + "UploadImages";// +"\\";
                    //string physicalPathPdf = HttpContext.Server.MapPath("../") + "UploadPdf";// +"\\";
                    //ParentNameC = model.Level + "-" + model.Subject + "-" + model.TextBookTitle + "-" + "TextBook" + ".pdf";
                    ////for (int i = 0; i < Request.Files.Count; i++)
                    ////{

                    //string SaveLocation = string.Format("{0}\\{1}", physicalPath, model.Level + "-" + model.Subject + "-" + model.TextBookTitle + "-" + "TextBook" + ".pdf");
                    //SaveLocationPdf = string.Format("{0}\\{1}", physicalPathPdf, model.Level + "-" + model.Subject + "-" + model.TextBookTitle + "-" + "TextBook" + ".pdf");
                    ParentNameC = model.Level + "-" + model.Subject + "-" + model.TextBookTitle + "-" + "TextBook" + ".pdf";

                    string path = @"c:\upload";

                    string SaveLocationPdf = string.Format("{0}\\{1}", path, model.Level + "-" + model.Subject + "-" + model.TextBookTitle + "-" + "TextBook" + ".pdf");
               

                    ////   System.IO.Path.GetFileName(Request.Files[i].FileName);
                    //Request.Files[0].SaveAs(SaveLocation);//.SaveAs(SaveLocation);
                    //Request.Files[0].SaveAs(SaveLocationPdf);

                    List<Course> theTextBookCourse = work.CourseRepository.Get(a => a.Name.Equals(model.Subject) && a.LevelID == SubjectLevel.LevelID).ToList();
                  //  List<Course> theTextBookCourse = work.CourseRepository.Get(a => a.Name.Equals(model.Subject) && a.LevelID == SubjectLevel.LevelID).ToList();

                    Course C = theTextBookCourse[0];
                    List<TextBook> theChaps = work.TextBookRepository.Get(a => a.CourseID == C.CourseID).ToList();
                    //find if we have duplicate copy
                    foreach (var chap in theChaps)
                    {
                        string thePPArt = ParentNameC.Trim().ToLower();
                        string chap2 = chap.ParentName.Trim().ToLower();
                        if (chap2.Equals(thePPArt))
                        {
                            ModelState.AddModelError("", "A Texbook of this Name Already Exist!");
                            UploadTextBookViewModel themodel = new UploadTextBookViewModel();
                            themodel.Level = "JSS1";
                          //  themodel.Subject = model.Subject;
                            return View("Create");
                        }
                    }
                    TextBook theTextBook = new TextBook { Name = model.TextBookTitle, ParentName = ParentNameC, path = SaveLocationPdf, CourseID = theTextBookCourse[0].CourseID };
                    //, FileData = fileData.ToArray()

                    Request.Files[0].SaveAs(SaveLocationPdf);//.SaveAs(SaveLocation);
                    work.TextBookRepository.Insert(theTextBook);
                    work.Save();
                    // }

                   
                }
                return RedirectToAction("Create", new { id = 1 });

                // TODO: Add insert logic here

                //   return RedirectToAction("Index");


            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UploadTextBook/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /UploadTextBook/Edit/5

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
        // GET: /UploadTextBook/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UploadTextBook/Delete/5

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
