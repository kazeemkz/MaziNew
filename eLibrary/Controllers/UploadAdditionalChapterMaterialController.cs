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
    public class UploadAdditionalChapterMaterialController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        string ParentNameC = "";
        //
        // GET: /UploadAdditionalChapterMaterial/

        public ActionResult LoadCourseChapters(string courseLevel)
        {
            string[] theCoursLevel = courseLevel.Split(':');
            string theLevel = theCoursLevel[0];
            string theCourse = theCoursLevel[1];
            // List<Level> theLevels = work.LevelRepository.Get(a => a.Name.Equals(theLevel)).ToList();
            // List<Chapter> theChapters = work.ChapterRepository.Get(a => a.Course.Name.Equals(theCourse)).ToList();
            List<Chapter> theChapters = work.ChapterRepository.Get(a => a.Course.Name.Equals(theCourse) && a.Course.Level.Name.Equals(theLevel)).ToList();

            //|| a.Course.Name.Equals(theCourse)
            List<SelectListItem> chap = new List<SelectListItem>();

            foreach (var cha in theChapters)
            {
                chap.Add(new SelectListItem { Text = cha.Name, Value = cha.Number });
            }
            return Json(chap, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadCourseChapters2(string courseLevel)
        {
            string[] theCoursLevel = courseLevel.Split(':');
            string theLevel = theCoursLevel[0];
            int theCourseID =Convert.ToInt32(theCoursLevel[1]);
            // List<Level> theLevels = work.LevelRepository.Get(a => a.Name.Equals(theLevel)).ToList();
            // List<Chapter> theChapters = work.ChapterRepository.Get(a => a.Course.Name.Equals(theCourse)).ToList();
            List<Chapter> theChapters = work.ChapterRepository.Get(a => a.Course.CourseID == theCourseID && a.Course.Level.Name.Equals(theLevel)).ToList();

            //|| a.Course.Name.Equals(theCourse)
            List<SelectListItem> chap = new List<SelectListItem>();

            foreach (var cha in theChapters)
            {
                chap.Add(new SelectListItem { Text = cha.Name, Value = cha.Number });
            }
            return Json(chap, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /UploadAdditionalChapterMaterial/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /UploadAdditionalChapterMaterial/Create

        public ActionResult Create(int id=0)
        {
            ViewBag.Success = id.ToString();
            return View();
        }

        //
        // POST: /UploadAdditionalChapterMaterial/Create

        [HttpPost]
        public ActionResult Create(UploadAdditionalChapterMaterial model, IEnumerable<HttpPostedFileBase> file)
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
                    //create byte array of size equal to file input stream
                    //byte[] fileData = new byte[Request.Files[0].InputStream.Length];
                    //// fileData
                    ////add file input stream into byte array
                    //Request.Files[0].InputStream.Read(fileData, 0, Convert.ToInt32(Request.Files[0].InputStream.Length));
                    //fileData.ToArray();

                   // model.
                    Level SubjectLevel = work.LevelRepository.Get(a => a.Name.Equals(model.Level)).First();

                    //string physicalPath = HttpContext.Server.MapPath("../") + "UploadImages";// +"\\";
                    //string physicalPathPdf = HttpContext.Server.MapPath("../") + "UploadPdf";// +"\\";


                    //string SaveLocation = string.Format("{0}\\{1}", physicalPath, model.Level + "-" + model.Subject + "-" + "Chapter" + model.Chapter + "-" + model.MaterialTitle + "-" + "Additional-Text" + ".doc");
                    //string SaveLocationPdf = string.Format("{0}\\{1}", physicalPathPdf, model.Level + "-" + model.Subject + "-" + "Chapter" + model.Chapter + "-" + model.MaterialTitle + "-" + "Additional-Text" + ".pdf");

                    ParentNameC = model.Level + "-" + model.Subject + "-" + "Chapter" + model.Chapter + "-" + model.MaterialTitle + "-" + "Additional-Text" + ".pdf";
                   //// System.IO.Path.GetFileName(Request.Files[i].FileName);

                   // Request.Files[0].SaveAs(SaveLocation);//.SaveAs(SaveLocation);

                   // Request.Files[0].SaveAs(SaveLocationPdf);

                   // Utility2.Word2PDF(SaveLocation, SaveLocationPdf);


                    string path = @"c:\upload";

                    string SaveLocationPdf = string.Format("{0}\\{1}", path, model.Level + "-" + model.Subject + "-" + "Chapter" + model.Chapter + "-" + model.MaterialTitle + "-" + "Additional-Text" + ".pdf");
                 

                    Course theAdittionalCahpterCourse = work.CourseRepository.Get(a => a.Name.Equals(model.Subject) && a.LevelID == SubjectLevel.LevelID).First();
                    List<Chapter> theChap = work.ChapterRepository.Get(a => a.Number.Equals(model.Chapter) && a.CourseID == theAdittionalCahpterCourse.CourseID).ToList();
                    AdditionalChapterText theText = new AdditionalChapterText { ChapterID = theChap[0].ChapterID, Chapter = theChap[0], Name = model.MaterialTitle, ParentName = ParentNameC};
                 //   , FileData = fileData.ToArray() 

                    Chapter C = theChap[0];
                    List<AdditionalChapterText> theChaps = work.AdditionalChapterTextRepository.Get(a => a.ChapterID == C.ChapterID).ToList();
                    //find if we have duplicate copy
                    foreach (var chap in theChaps)
                    {
                        string thePPArt = ParentNameC.Trim().ToLower();
                        string chap2 = chap.ParentName.Trim().ToLower();
                        if (chap2.Equals(thePPArt))
                        {
                            ModelState.AddModelError("", "An Additional Note of this Name Already Exist!");
                            // UploadLessonNoteViewModel themodel = new UploadLessonNoteViewModel();
                            return View(model);
                        }
                    }






                    Request.Files[0].SaveAs(SaveLocationPdf);//.SaveAs(SaveLocation);
                    work.AdditionalChapterTextRepository.Insert(theText);
                    work.Save();
                    // }
                }
                // TODO: Add insert logic here
                return RedirectToAction("Create", new { id = 1 });
                // return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /UploadAdditionalChapterMaterial/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /UploadAdditionalChapterMaterial/Edit/5

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
        // GET: /UploadAdditionalChapterMaterial/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UploadAdditionalChapterMaterial/Delete/5

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
