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
    public class CourseController : Controller
    {
        //
        // GET: /Course/
        UnitOfWork work = new UnitOfWork();
       //  [DynamicAuthorize]
        public ActionResult Index(string level)
        {
            Level theL = new Level();
            List<Course> theCourses = new List<Course>();
            List<Level> theLevel = work.LevelRepository.Get(a => a.Name.Equals(level)).ToList();
            if (theLevel.Count() > 0)
            {
                theL = theLevel[0];
                theCourses = work.CourseRepository.Get(a => a.LevelID == theL.LevelID).ToList();
                theL.Course = theCourses;
            }
            return View(theCourses);
        }

        public void Select(int id = 0)
        {

            // Course theCourse = new Course() { Name = "English" };
            // ViewBag.Name = theCourse.Name;
            RedirectToAction("Create", "UploadLessonNote", new { id = 4 });
            // return View();
        }

        public ActionResult IndexPartial()
        {
            List<Course> theCourses = new List<Course>() { new Course { Name = "English" }, new Course { Name = "English" }, new Course { Name = "English" },
                new Course { Name = "English" },new Course { Name = "English" },new Course { Name = "English" },new Course { Name = "English" },new Course { Name = "English" },
                new Course { Name = "English" },new Course { Name = "English" },new Course { Name = "English" },new Course { Name = "English" },new Course { Name = "English" },
                new Course { Name = "English" },new Course { Name = "English" },new Course { Name = "English" },
                new Course { Name = "Mathematics" }, new Course { Name = "Science" } };

            Course course = new Course();

            return View("CoursPartial", theCourses);
        }


        //
        // GET: /Course/Details/5

        public ActionResult Details(int id, int levelID)
        {
            return View();
        }

        public ActionResult CreatePartial(string levelValue)
        {

            return PartialView("_CreatePartial");
        }
        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Course/Create
     
        [HttpPost]
        public ActionResult Create(Course model, string levelValue)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string CourseName = model.Name.Trim().ToLower();



                    // int intVal = Convert.ToInt32(model.LevelStringID);
                    //  Level theLevel = db.Level.Include("Course").Where(a => a.Name.Equals(model.LevelStringID)).First();
                    Level theLevel = work.LevelRepository.Get(a => a.Name.Equals(model.LevelStringID)).First();
                    List<Course> theCourses = work.CourseRepository.Get(a => a.LevelID == theLevel.LevelID).ToList();
                    //  theLevel[0].Course.
                    foreach (var theC in theCourses)
                    {
                        string theCourse = theC.Name.Trim().ToLower();
                        if (theCourse.Equals(CourseName))
                        {
                            ModelState.AddModelError("", "This Subject Already Exist for Chosen Class");
                            return View(model);
                        }
                    }
                    model.Level = theLevel;
                    work.CourseRepository.Insert(model);
                    work.Save();
                }
                // string levelVal = ViewBag.Level as string;
                // TODO: Add insert logic here
                //   UploadLessonNote/Create
                return RedirectToAction("Create", "UploadLessonNote");

                // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // POST: /Course/Create
       [DynamicAuthorize]
        [HttpPost]
        public ActionResult Create2(Course model, string levelValue)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string CourseName = model.Name.Trim().ToLower();


                    //  eLContext db = new eLContext();

                    //  int intVal = Convert.ToInt32(model.LevelStringID);
                    //  Level theLevel = db.Level.Include("Course").Where(a => a.Name.Equals(model.LevelStringID)).First();
                    Level theLevel = work.LevelRepository.Get(a => a.Name.Equals(model.LevelStringID)).First();
                    List<Course> theCourses = work.CourseRepository.Get(a => a.LevelID == theLevel.LevelID).ToList();
                    //  theLevel[0].Course.
                    foreach (var theC in theCourses)
                    {
                        string theCourse = theC.Name.Trim().ToLower();
                        if (theCourse.Equals(CourseName))
                        {
                            ModelState.AddModelError("", "This Subject Already Exist for Chosen Class");
                            return View("Create",model);
                        }
                    }
                    model.Level = theLevel;
                    work.CourseRepository.Insert(model);
                    work.Save();
                }
                // string levelVal = ViewBag.Level as string;
                // TODO: Add insert logic here
                //   UploadLessonNote/Create
                // return RedirectToAction("Create", "UploadLessonNote");

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        //
        // GET: /Course/Edit/5
           [DynamicAuthorize]
        public ActionResult Edit(int id, int levelID)
        {
            Course theCourse = work.CourseRepository.GetByID(id);

            string theC = theCourse.Level.Name;

            // Level theLEvel = work.LevelRepository.Get(a=>a.LevelID == theC).First();

            theCourse.LevelStringID = theC;

            //  Level theLevel = work.LevelRepository.GetByID(levelID);
            // theCourse.Level = theLevel;
            return View(theCourse);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        public ActionResult Edit(Course model)
        {
            try
            {
                // TODO: Add update logic here
                // eLContext db = new eLContext();
                string CourseName = model.Name.Trim().ToLower();
                //  Level theLevel = db.Level.Include("Course").Where(a => a.Name.Equals(model.LevelStringID)).First();
                // Level theLevel = work.LevelRepository.Get(a => a.Name.Equals(model.LevelStringID)).First();
                //  theLevel[0].Course.

                // int intVal = Convert.ToInt32(model.LevelStringID);
                //  Level theLevel = db.Level.Include("Course").Where(a => a.Name.Equals(model.LevelStringID)).First();
                Level theLevel = work.LevelRepository.Get(a => a.Name.Equals(model.LevelStringID)).First();
                List<Course> theCourses = work.CourseRepository.Get(a => a.LevelID == theLevel.LevelID).ToList();
                foreach (var theC in theCourses)
                {
                    string theCourse = theC.Name.Trim().ToLower();
                    if (theCourse.Equals(CourseName))
                    {
                        ModelState.AddModelError("", "This Subject Already Exist for Chosen Class");
                        return View(model);
                    }
                }


                UnitOfWork work2 = new UnitOfWork();
                work2.CourseRepository.Update(model);
                work2.Save();
                // work.CourseRepository.Update(model);
                // work.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Course/Delete/5
        [DynamicAuthorize]
        public ActionResult Delete(int id)
        {
            Course theCourse = work.CourseRepository.GetByID(id);
            return View(theCourse);
            // return View();
        }

        //
        // POST: /Course/Delete/5

        [HttpPost]
        public ActionResult Delete(Course model)
        {
            try
            {
                // TODO: Add delete logic here
                string physicalPathPdf = @"c:\upload"; 

                List<Chapter> theChapters = work.ChapterRepository.Get(a => a.CourseID == model.CourseID).ToList();
                List<TextBook> theText = work.TextBookRepository.Get(a => a.CourseID == model.CourseID).ToList();

                foreach (var eachTextBook in theText)
                {
                  //  List<TextBook> theAddChapterText = work.TextBookRepository.Get(a => a.CourseID == thechap.ChapterID).ToList();
                   // foreach (var ad in theAddChapterText)
                   // {
                        string rootFolderPath = physicalPathPdf;
                        string filesToDelete = eachTextBook.ParentName;   // Only delete DOC files containing "DeleteMe" in their filenames
                        string[] fileList = System.IO.Directory.GetFiles(rootFolderPath, filesToDelete);
                        foreach (string file in fileList)
                        {
                            System.IO.File.Delete(file);
                            //  AdditionalChapterText theAddCapText = work.AdditionalChapterTextRepository.Get(a => a.ParentName.Equals(stringName)).Single();
                            work.TextBookRepository.Delete(eachTextBook);
                            work.Save();
                        }
                  //  }
                }

                foreach (var thechap in theChapters)
                {
                    List<AdditionalChapterText> theAddChapterText = work.AdditionalChapterTextRepository.Get(a => a.ChapterID == thechap.ChapterID).ToList();
                    foreach (var ad in theAddChapterText)
                    {
                        string rootFolderPath = physicalPathPdf;
                        string filesToDelete = ad.ParentName;   // Only delete DOC files containing "DeleteMe" in their filenames
                        string[] fileList = System.IO.Directory.GetFiles(rootFolderPath, filesToDelete);
                        foreach (string file in fileList)
                        {
                            System.IO.File.Delete(file);
                          //  AdditionalChapterText theAddCapText = work.AdditionalChapterTextRepository.Get(a => a.ParentName.Equals(stringName)).Single();
                            work.AdditionalChapterTextRepository.Delete(ad);
                            work.Save();
                        }
                    }
                     string rootFolderPath1 = physicalPathPdf;
                        string filesToDelete1 = thechap.ParentName;   // Only delete DOC files containing "DeleteMe" in their filenames
                        string[] fileList1 = System.IO.Directory.GetFiles(rootFolderPath1, filesToDelete1);
                        foreach (string file in fileList1)
                        {
                            System.IO.File.Delete(file);
                            //  AdditionalChapterText theAddCapText = work.AdditionalChapterTextRepository.Get(a => a.ParentName.Equals(stringName)).Single();
                            work.ChapterRepository.Delete(thechap);
                            work.Save();
                        }
                   
                }

                work.CourseRepository.Delete(model);
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
