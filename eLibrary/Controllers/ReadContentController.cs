using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLibrary.DAL;
using eLibrary.Model;

namespace eLibrary.Controllers
{
    [Authorize]
    public class ReadContentController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        Dictionary<string, Dictionary<string, string>> ChapterSubChap = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, string> theInner = new Dictionary<string, string>();
        List<SelectListItem> cours = new List<SelectListItem>();

        public ActionResult LoadCourse(string level)
        {
            //  List<Level> theLevel = work1.LevelRepository.Get(a => a.Name.Equals(level)).ToList();
            List<Course> theCourses = work.CourseRepository.Get(a => a.Level.Name.Equals(level)).ToList();
            List<SelectListItem> cours = new List<SelectListItem>();



            foreach (var co in theCourses)
            {
                string physicalPathPdf = HttpContext.Server.MapPath("../") + "UploadPdf" + "\\";
                List<Chapter> theChapters = work.ChapterRepository.Get(a => a.Course.CourseID == co.CourseID).OrderBy(a => a.Number).ToList();
                //List<TextBook> theTextBooks =  work.TextBookRepository.Get(a => a.CourseID == co.CourseID).ToList();

                foreach (var chap in theChapters)
                {
                    theInner.Add(co.Name + ":" + chap.Name + " Chapter " + chap.Number, chap.ParentName);
                    List<AdditionalChapterText> theAdditionalChapters = work.AdditionalChapterTextRepository.Get(a => a.ChapterID == chap.ChapterID).ToList();
                    foreach (var addChap in theAdditionalChapters)
                    {
                        theInner.Add(co.Name + ":" + addChap.Name + " Additional Material", addChap.ParentName);
                    }

                }
                List<TextBook> theTextBooks = work.TextBookRepository.Get(a => a.CourseID == co.CourseID).ToList();
                foreach (var textBook in theTextBooks)
                {
                    theInner.Add(co.Name + ":" + textBook.Name + " TextBook", textBook.ParentName);
                }
                // ChapterSubChap.Add(co.Name, theInner);
                // cours.Add(new SelectListItem { Text = co.Name, Value = co.Name });

            }
            foreach (var t in theInner)
            {

                cours.Add(new SelectListItem { Text = t.Key, Value = t.Value });
            }
            // return Json(ChapterSubChap, JsonRequestBehavior.AllowGet);
            return Json(cours, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report(string stringName)
        {
       //  List<Chapter> theChapter =  work.ChapterRepository.Get(a => a.ParentName.Equals(stringName)).ToList();
       //  List<TextBook> theTextBook = work.TextBookRepository.Get(a => a.ParentName.Equals(stringName)).ToList();
        // List<AdditionalChapterText> addtionalChapter = work.AdditionalChapterTextRepository.Get(a => a.ParentName.Equals(stringName)).ToList();
           byte[] _Buffer = null;
         byte[] fileData = null;
     
      
           // string physicalPathPdf = HttpContext.Server.MapPath("../") + "UploadPdf/";// +"\\";physicalPathPdf + stringName

         string physicalPathPdf = @"c:\upload";

       //  string SaveLocationPdf = string.Format("{0}\\{1}", path, model.Level + "-" + model.Subject + "-" + "Chapter" + model.Chapter + "-" + model.TopicTitle + ".pdf");
        // Request.Files[0].SaveAs(SaveLocationPdf);//.SaveAs(SaveLocation);

            try
            {
                //if (theChapter.Count > 0)
                //{
                //    fileData = (byte[])theChapter.First().FileData.ToArray();
                //}

                //if (theTextBook.Count > 0)
                //{
                //    fileData = (byte[])theTextBook.First().FileData.ToArray();
                //}

                //if (addtionalChapter.Count > 0)
                //{
                //    fileData = (byte[])addtionalChapter.First().FileData.ToArray();
                //}



                ////declare byte array to get file content from database and string to store file name
                //byte[] fileData;
                //string fileName;
                ////create object of LINQ to SQL class
                //DBContext dataContext = new DBContext();
                ////using LINQ expression to get record from database for given id value
                //var record = from p in dataContext.FileDumps
                //             where p.ID == id
                //             select p;
                ////only one record will be returned from database as expression uses condtion on primary field
                ////so get first record from returned values and retrive file content (binary) and filename 
               
                //fileName = record.First().;
                ////return file and provide byte file content and file name

              
                //return File(fileData, "text", null);





                // Open file for reading
            //    string physicalPathPdf = @"c:\upload";

                System.IO.FileStream _FileStream = new System.IO.FileStream(physicalPathPdf +"/"+ stringName, System.IO.FileMode.Open, System.IO.FileAccess.Read);



                // attach filestream to binary reader

                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);



                // get total byte length of the file

                long _TotalBytes = new System.IO.FileInfo(physicalPathPdf + "/" + stringName).Length;



                // read entire file into buffer

                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);



                // close file reader

                _FileStream.Close();

                _FileStream.Dispose();

                _BinaryReader.Close();

            }

            catch (Exception _Exception)
            {

                // Error

                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());

            }


            
          //  string physicalPathPdf = HttpContext.Server.MapPath("../") + "UploadPdf/";// +"\\";
          //  Response.AppendHeader("Content-Disposition", String.Format("inline; filename={0}", stringName));
           // return File(physicalPathPdf + stringName, "application/pdf", Server.UrlEncode(physicalPathPdf + stringName));
            return File(_Buffer, "application/pdf");
           // return File(fileData, "application/pdf");
        }
        //
        // GET: /ReadContent/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ReadContent/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ReadContent/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReadContent/Create

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
        // GET: /ReadContent/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ReadContent/Edit/5

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
        // GET: /ReadContent/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ReadContent/Delete/5

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
