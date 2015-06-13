using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DesktopDragAndDrop.Models;
using eLibrary.DAL;
using eLibrary.Model;

namespace eLibrary.Controllers
{
    public class PhotoController : Controller
    {

        UnitOfWork work = new UnitOfWork();
        //
        // GET: /Photo/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Photo/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }


        
        [HttpPost]
       public string Upload(int id, HttpPostedFileBase file)
       // public string Upload(int id)
        {

            //var length = Request.ContentLength;
            //var bytes = new byte[length];
            //Request.InputStream.Read(bytes, 0, length);



            //var files = Request.Files[0];
            //byte[] fileContents = new byte[files.ContentLength];
            //file.InputStream.Read(fileContents, 0, file.ContentLength);
            


        //  byte[]  fileContents = new byte[Request.ContentLength];
           // fileContents.to
          //  Request.InputStream.Read(fileContents, 0, Request.ContentLength);

         //   var file = Request.Files[0];

       
              Photo thePhoto = new Photo();
              thePhoto.BookID = id;
          

          //  UploadedFile file = RetrieveFileFromRequest();
           // string savePath = string.Empty;
           // string virtualPath = SaveFile(file);



            using (var stream = new MemoryStream())
            {
                Request.InputStream.CopyTo(stream);
                thePhoto.FileData = stream.ToArray();
               // var domainModel = new MyDomainModel
               // {
                //    AdImage = image,
                  //  Description = model.Description
                //};

                // TODO: persist the domain model by passing it to a method
                // on your DAL layer
            }









        
            List<Photo> thePhotos = work.PhotoRepository.Get(a => a.BookID == id).ToList();
            if (thePhotos.Count() > 0)
            {
                UnitOfWork work2 = new UnitOfWork();
                Photo theOldPhoto = thePhotos[0];
                work2.PhotoRepository.Delete(theOldPhoto);
                work2.Save();

            }
         
                work.PhotoRepository.Insert(thePhoto);
                work.Save();
           

            return null;         
        }        

        private UploadedFile RetrieveFileFromRequest()
        {
            string filename = null;
            string fileType = null;
            byte[] fileContents = null;

            if (Request.Files.Count > 0)
            { //we are uploading the old way
                var file = Request.Files[0];
                fileContents = new byte[file.ContentLength];
                file.InputStream.Read(fileContents, 0, file.ContentLength);
                fileType = file.ContentType;
                filename = file.FileName;
            }
            else if (Request.ContentLength > 0)
            {
                // Using FileAPI the content is in Request.InputStream!!!!
                fileContents = new byte[Request.ContentLength];
                Request.InputStream.Read(fileContents, 0, Request.ContentLength);
                filename = Request.Headers["X-File-Name"];
                fileType = Request.Headers["X-File-Type"];
            }

            return new UploadedFile()
            {
                Filename = filename,
                ContentType = fileType,
                FileSize = fileContents != null ? fileContents.Length : 0,
                Contents = fileContents
            };
        }

        /// <summary>
        /// Saves the image
        /// </summary>
        /// <param name="logoUpload"></param>
        private string SaveFile(UploadedFile file)
        {
            System.IO.FileStream stream = null;
            var virtualPath = string.Empty;
            try
            {
                var physicalPath = Server.MapPath("~/Content");
                virtualPath = "/Content/";                
                var fileName = System.IO.Path.GetFileNameWithoutExtension(file.Filename);
                fileName = fileName + DateTime.Now.Ticks + System.IO.Path.GetExtension(file.Filename);
                var path = System.IO.Path.Combine(physicalPath, fileName);
                virtualPath = virtualPath + fileName;
                stream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                if (stream.CanWrite)
                {
                    stream.Write(file.Contents, 0, file.Contents.Length);
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return virtualPath;
        }













        [HttpPost]
        public virtual string UploadFiles(object obj)
        {
            var length = Request.ContentLength;
            var bytes = new byte[length];
            Request.InputStream.Read(bytes, 0, length);
            // bytes has byte content here. what do do next?

            var fileName = Request.Headers["X-File-Name"];
            var fileSize = Request.Headers["X-File-Size"];
            var fileType = Request.Headers["X-File-Type"];

            var saveToFileLoc = string.Format("{0}\\{1}",
                                           Server.MapPath("/Files"),
                                           fileName);

            // save the file.
            var fileStream = new FileStream(saveToFileLoc, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(bytes, 0, length);
            fileStream.Close();

            return string.Format("{0} bytes uploaded", bytes.Length);
        }

      
        //[HttpPost]
        //public ActionResult UploadFiles( int id,IEnumerable<HttpPostedFileBase> files)
        //{
           
        //    foreach (HttpPostedFileBase file in files)
        //    {
        //    }

        //    //// byte[] data;
        //    //Photo thePhoto = new Photo();
        //    //thePhoto.BookID = id;

        //    //using (Stream inputStream = file.InputStream)
        //    //{
        //    //    MemoryStream memoryStream = inputStream as MemoryStream;
        //    //    if (memoryStream == null)
        //    //    {
        //    //        memoryStream = new MemoryStream();
        //    //        inputStream.CopyTo(memoryStream);
        //    //    }
        //    //    thePhoto.FileData = memoryStream.ToArray();
        //    //}

        //    //// var buffer = Convert.ToBase64CharArray(file);
        //    //// model.FileData = file.ToArray();
        //    //work.PhotoRepository.Insert(thePhoto);
        //    //work.Save();
        //    //return RedirectToAction("Details", "Book", new { id = id });
        //    return RedirectToAction("Details", "Book");
        //}

        //
        // GET: /Photo/Create

        public ActionResult Create(int id)
        {
            Photo thePhoto = new Photo();
            thePhoto.BookID = id;
           // return View();
            return View(thePhoto);
        }

        //
        // POST: /Photo/Create

        [HttpPost]
        public ActionResult Create(Photo model)
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
        // GET: /Photo/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Photo/Edit/5

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
        // GET: /Photo/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Photo/Delete/5

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
