using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using eLibrary.DAL;
using eLibrary.Model;
using eLibrary.Models;
//using Spire.Doc;

namespace eLibrary.Controllers
{

    [DynamicAuthorize]
    public class UploadLessonNoteController : AsyncController
    {
        UnitOfWork work = new UnitOfWork();
        UnitOfWork work1 = new UnitOfWork();
        string ParentNameC = "";
        string SaveLocationPdf = null;
        // private PDFCreator.clsPDFCreator m_PDFCreator = new PDFCreator.clsPDFCreator();

        // GET: /UploadLessonNote/

        //public static SelectList FetchCoursesByLevel(string level)

        public string FindDuplicateChapterNumber(string chapterLevel)
        {
            string theCount = "0";
            int chapterNo = 0;
           // string isOneOrZero = "0";
            string theChapterNumber = "0";
            string[] theCoursLevel = chapterLevel.Split(':');
            string theLevel = theCoursLevel[0];
            string theCourse = theCoursLevel[1];
            //  theChapterNumber = theCoursLevel[2];
            if (theChapterNumber != "")
            {
                chapterNo = Convert.ToInt32(theChapterNumber);
            }
            List<Chapter> theChapters = work.ChapterRepository.Get(a => a.Course.Name.Equals(theCourse) && a.Course.Level.Name.Equals(theLevel)).ToList();

            theCount = Convert.ToString(theChapters.Count() + 1);
            //foreach (var chap in theChapters)
            //{
            //    int intValue = Convert.ToInt32(chap.Number);
            //    if (intValue == chapterNo)
            //    {
            //        isOneOrZero = "1";
            //        break;
            //    }
            //}
            //  Chapter
            return theCount;
            // return isOneOrZero;
        }


        public ActionResult LoadCourse(string level)
        {
            //  List<Level> theLevel = work1.LevelRepository.Get(a => a.Name.Equals(level)).ToList();
            List<Course> theCourses = work1.CourseRepository.Get(a => a.Level.Name.Equals(level)).ToList();
            List<SelectListItem> cours = new List<SelectListItem>();

            foreach (var co in theCourses)
            {
                cours.Add(new SelectListItem { Text = co.Name, Value = co.Name });
            }
            return Json(cours, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadCourse2(string level)
        {
            //  List<Level> theLevel = work1.LevelRepository.Get(a => a.Name.Equals(level)).ToList();
            List<Course> theCourses = work1.CourseRepository.Get(a => a.Level.Name.Equals(level)).ToList();
            List<SelectListItem> cours = new List<SelectListItem>();

            foreach (var co in theCourses)
            {
                cours.Add(new SelectListItem { Text = co.Name, Value = co.LevelID.ToString() });
            }
            return Json(cours, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(int id = 0)
        {
            // List<Exam> theExam = work.ExamRepository.Get(a => a.ExamID == id).ToList();
            //int thExamid =  theExam[0].ExamID;
            //List<Question> theQuestions = work.QuestionRepository.Get(a => a.Exam.ExamID == thExamid).ToList();
            return View();
        }

        //
        // GET: /UploadLessonNote/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /UploadLessonNote/Create

        public ActionResult Create(int id = 0)
        {
            ViewBag.Success = id.ToString();

            return View();
        }

        //
        // POST: /UploadLessonNote/Create

        [HttpPost]
        public ActionResult Create(UploadLessonNoteViewModel model)
        {

            //  model.Level.
            try
            {
                if (Request.Files[0] == null)
                {
                    new ModelError("No Uploaded Document!");
                    return View(model);
                }
                string TheFileName = System.IO.Path.GetFileName(Request.Files[0].FileName);

                if (!(TheFileName.EndsWith(".pdf")))
                {
                    ModelState.AddModelError("", "The Input File is not a .pdf file!");
                    return View(model);
                }
                if (ModelState.IsValid)
                {

                    Level SubjectLevel = work.LevelRepository.Get(a => a.Name.Equals(model.Level)).First();

                    // //create byte array of size equal to file input stream
                    // byte[] fileData = new byte[Request.Files[0].InputStream.Length];
                    //// fileData
                    // //add file input stream into byte array
                    // Request.Files[0].InputStream.Read(fileData, 0, Convert.ToInt32(Request.Files[0].InputStream.Length));
                    // fileData.ToArray();






                    ////create system.data.linq object using byte array
                    //System.Data.Linq.Binary binaryFile = new System.Data.Linq.Binary(fileData);
                    ////initialise object of FileDump LINQ to sql class passing values to be inserted
                    //FileDump record = new FileDump { FileData = binaryFile, FileName = System.IO.Path.GetFileName(Request.Files[0].FileName) };
                    ////call InsertOnsubmit method to pass new object to entity
                    //dataContext.FileDumps.InsertOnSubmit(record);
                    ////call submitChanges method to execute implement changes into database
                    //dataContext.SubmitChanges();




                    //  string physicalPath = HttpContext.Server.MapPath("../") + "UploadImages";// +"\\";
                    // string physicalPathPdf = HttpContext.Server.MapPath("../") + "UploadPdf";// +"\\";

                    ParentNameC = model.Level + "-" + model.Subject + "-" + "Chapter" + model.Chapter + "-" + model.TopicTitle + ".pdf";

                    string path = @"c:\upload";

                    string SaveLocationPdf = string.Format("{0}\\{1}", path, model.Level + "-" + model.Subject + "-" + "Chapter" + model.Chapter + "-" + model.TopicTitle + ".pdf");


                    // string SaveLocation = string.Format("{0}\\{1}", physicalPath, model.Level + "-" + model.Subject + "-" + "Chapter" + model.Chapter + "-" + model.TopicTitle + ".doc");
                    // SaveLocationPdf = string.Format("{0}\\{1}", physicalPathPdf, model.Level + "-" + model.Subject + "-" + "Chapter" + model.Chapter + "-" + model.TopicTitle + ".pdf");
                    // Request.Files[0].SaveAs(SaveLocation);//.SaveAs(SaveLocation);
                    // Utility2.Word2PDF(SaveLocation, SaveLocationPdf);

                    // Request.Files[0].SaveAs(SaveLocationPdf);

                    //now save file path to databse

                    eLContext db = new eLContext();

                    List<Course> theCourse = work.CourseRepository.Get(a => a.Name.Equals(model.Subject) && a.LevelID == SubjectLevel.LevelID).ToList();
                    Course C = theCourse[0];
                    List<Chapter> theChaps = work.ChapterRepository.Get(a => a.CourseID == C.CourseID).ToList();
                    //find if we have duplicate copy
                    foreach (var chap in theChaps)
                    {
                        string thePPArt = ParentNameC.Trim().ToLower();
                        string chap2 = chap.ParentName.Trim().ToLower();
                        if (chap2.Equals(thePPArt))
                        {
                            ModelState.AddModelError("", "A Chapter of this Name Already Exist!");
                            UploadLessonNoteViewModel themodel = new UploadLessonNoteViewModel();
                            return View(themodel);
                        }
                    }
                    //  Course theCourse = db.Courses.Include("Chapter").Where(a => a.Name.Equals(model.Subject) && a.LevelID == SubjectLevel.LevelID).First();
                    Chapter theChapter = new Chapter { Name = model.TopicTitle, Number = model.Chapter, ParentName = ParentNameC, Path = SaveLocationPdf, Course = theCourse[0], CourseID = theCourse[0].CourseID };
                    // Chapter theChapter = new Chapter { Name = model.TopicTitle, Number = model.Chapter, ParentName = ParentNameC, Path = SaveLocationPdf, Course = theCourse, CourseID = theCourse.CourseID };


                    Request.Files[0].SaveAs(SaveLocationPdf);//.SaveAs(SaveLocation);
                    //  , FileData = fileData.ToArray()
                    work.ChapterRepository.Insert(theChapter);
                    work.Save();
                    return RedirectToAction("Create", new { id = 1 });
                }

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UploadLessonNote/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /UploadLessonNote/Edit/5

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
        // GET: /UploadLessonNote/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UploadLessonNote/Delete/5

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
