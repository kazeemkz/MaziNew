using System;
using System.Collections.Generic;
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
     [Authorize]
    public class ExamController : Controller
    {
        //
        // GET: /Exam/
        UnitOfWork work = new UnitOfWork();
        Exam Examme = new Exam();
        // [Authorize(Roles = "Student")] 
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]


      // public ActionResult Index2(string sortOrder, string currentFilter, int? Rodid, int? Colid, int? page)
        [DynamicAuthorize]
        public ActionResult Index(string ExamCode = "kababa")
        {
            //Course theCourse = work.CourseRepository.GetByID(id);
            List<Exam> theExam = work.ExamRepository.Get(a => a.ExamCode.Equals(ExamCode) && a.Visible == "true").ToList();
         //  List<Exam> theExam = work.ExamRepository.Get(a => a.LevelName.Equals(level) && a.Term== term && a.Visible== "true").ToList();// && a.CourseID == courseID

            if (theExam.Count > 0)
            {
                Examme = theExam[0];
                IList<Question> theQs = new ExamService().GetQuestions(theExam[0]);
                Examme.AddQuestion(theQs);
            }
            List<SelectListItem> ExamCodes = new List<SelectListItem>();

            List<Exam> theExams = work.ExamRepository.Get(a=>a.Visible == "true").ToList();

            foreach (var exam in theExams)
            {
                ExamCodes.Add(new SelectListItem() { Text = exam.ExamCode, Value = exam.ExamCode });
            }

            // theItem.Add(new SelectListItem() { Text = "PRIMART 1A", Value = "PRIMART 1A" });
            ViewData["ExamCodes"] = ExamCodes;
            return View(Examme);
        }


        public ActionResult LoadExamDuration(int examID)
        {
            Exam theExam = work.ExamRepository.GetByID(examID);

            return Json(theExam.Duration, JsonRequestBehavior.AllowGet);
        }



        //public ActionResult LoadNextQuestionNumber(string examCode)
        //{
        //    List<Exam> theExams = work.ExamRepository.Get(a => a.ExamCode.Equals(examCode)).ToList();
        //    Exam theRealExam = theExams[0];
        //    List<Question> theQuestions = work.QuestionRepository.Get(a => a.ExamID == theRealExam.ExamID).ToList();
        //    int theCount = theQuestions.Count() + 1;

        //    return Json(theCount, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult LoadExamCodes(string sortOrder, string currentFilter, string ExamCode, string Class, string Visible, int? page)
       // public ActionResult LoadExamCodes()
        {

            var theExamCodes = from s in work.ExamRepository.Get()
                           select s;
            if (!String.IsNullOrEmpty(ExamCode))
            {
                string theCode = ExamCode.ToLower();
                theExamCodes = theExamCodes.Where(s => s.ExamCode.ToLower().Contains(theCode));
                                      
            }

            if (!String.IsNullOrEmpty(Class))
            {
              //  string theCode = ExamCode.ToLower();
                theExamCodes = theExamCodes.Where(s => s.LevelName.Equals(Class));

            }

            if (!String.IsNullOrEmpty(Visible))
            {
               // bool theVal = Convert.ToBoolean(Visible);
                //  string theCode = ExamCode.ToLower();
                theExamCodes = theExamCodes.Where(s => s.Visible.Equals(Visible));

            }

           // List<Exam> theExamCodes;
          //  theExamCodes = work.ExamRepository.Get().ToList();// && a.CourseID == courseID


            //  if (examCode == "kazoo")
            // {
        
            //// }
            // else
            // {
            //   theExamCodes = work.ExamRepository.Get(a => a.ExamCode.Equals(examCode)).ToList();// && a.CourseID == courseID
            // }
            //    //   @Html.DropDownListFor(model => model.LevelType, new SelectList((System.Collections.IEnumerable)ViewData["Level"], "Value", "Text"))

            // List<SelectListItem> theItem = new List<SelectListItem>();

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            foreach (var examCodes in theExamCodes)
            {
                IList<Question> theQs = new ExamService().GetQuestions(examCodes);
                examCodes.AddQuestion(theQs);
            }
            // ViewData["examCode"] = theItem;
          //  return View("Index2", theExamCodes);
            return View("Index2", theExamCodes.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LoadNextQuestionNumber(string examCode)
        {
            List<Exam> theExams = work.ExamRepository.Get(a => a.ExamCode.Equals(examCode)).ToList();
            Exam theRealExam = theExams[0];
            List<Question> theQuestions = work.QuestionRepository.Get(a => a.ExamID == theRealExam.ExamID).ToList();
            int theCount = theQuestions.Count() + 1;

            return Json(theCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreatePartial(string levelValue)
        {

            return PartialView("_Create");
        }

        public ActionResult LoadExam(string level)
        {
            // List<Level> theLevel = work1.LevelRepository.Get(a => a.Name.Equals(level)).ToList();
            //List<Exam> theExams = work.ExamRepository.Get().ToList();
            //List<SelectListItem> exam = new List<SelectListItem>();

            //foreach (var co in theExams)
            //{
            //    //Course theCourse = work.CourseRepository.GetByID(co.);
            //    exam.Add(new SelectListItem { Text = co.ExamCode, Value = co.ExamCode });
            //}
            //return Json(exam, JsonRequestBehavior.AllowGet);

            List<Exam> theExams = work.ExamRepository.Get().ToList();
            List<SelectListItem> exam = new List<SelectListItem>();
            if (string.IsNullOrEmpty(level))
            {

            }
            else
            {
                theExams = work.ExamRepository.Get(a => a.ExamCode != level).ToList();

                exam.Add(new SelectListItem { Text = level, Value = level, Selected = true });
            }
            //  List<Level> theLevel = work1.LevelRepository.Get(a => a.Name.Equals(level)).ToList();



            foreach (var co in theExams)
            {
                //Course theCourse = work.CourseRepository.GetByID(co.);
                exam.Add(new SelectListItem { Text = co.ExamCode, Value = co.ExamCode });
            }
            return Json(exam, JsonRequestBehavior.AllowGet);
        }
       // [Authorize(Roles = "Student")] 
        [HttpPost]
        public ActionResult Grade(Exam exam)
        {

            var grade = new ExamService().Grade(exam);
            return View(grade);
        }

        //
        // GET: /Exam/Details/5

        public ActionResult Details(int id)
        {
            Exam theExam = work.ExamRepository.GetByID(id);
            return View(theExam);
        }

        //
        // GET: /Exam/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Exam/Create

        [HttpPost]
        [DynamicAuthorize]
        public ActionResult Create(Exam theExam)
        {
            try
            {
                // TODO: Add insert logic here

                string theExamCode = theExam.ExamCode.Trim().ToLower();
                List<Exam> theExams = work.ExamRepository.Get(a => a.ExamCode.Equals(theExamCode)).ToList();
                if (theExams.Count > 0)
                {
                    ModelState.AddModelError("", "This Exam Code Already Exist!");
                    return View("_Create", theExam);
                }
                else
                {
                    work.ExamRepository.Insert(theExam);
                    work.Save();
                    return RedirectToAction("Create", "Question");
                }

                // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Exam/Edit/5
        [DynamicAuthorize]

        public ActionResult Edit(int id)
        {
            Exam theExam = work.ExamRepository.GetByID(id);
            return View(theExam);
        }

        //
        // POST: /Exam/Edit/5

        [HttpPost]
        public ActionResult Edit(Exam theExam)
        {
            try
            {
                // TODO: Add update logic here

                work.ExamRepository.Update(theExam);
                work.Save();

                return RedirectToAction("LoadExamCodes");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Exam/Delete/5
        [DynamicAuthorize]

        public ActionResult Delete(int id)
        {
            Exam theExam = work.ExamRepository.GetByID(id);

            return View(theExam);
        }

        //
        // POST: /Exam/Delete/5

        [HttpPost]
        public ActionResult Delete(Exam theExam)
        {
            try
            {

                //PrimarySchoolStudent theStudent = work.PrimarySchoolStudentRepository.GetByID(model.PersonID);
                //work.PrimarySchoolStudentRepository.Delete(theStudent);

                Exam theMainExam = work.ExamRepository.GetByID(theExam.ExamID);

              //  GetQuestions(Exam theMainExam, ref UnitOfWork work)

                IList<Question> theQs = new ExamService().GetQuestions(theMainExam, ref work);

                foreach (var theQ in theQs)
                {
                   // foreach (var choice in theQ.Choices)
                   // {
                      //  work.ChoiceRepository.Delete(choice);
                   // }
                    work.QuestionRepository.Delete(theQ);
                }
               // foreach (var theQ in theQs)
               // {
                   // theMainExam.Questions.Add(theQ);
               // }
             //   examCodes.AddQuestion(theQs);



               // IList<Question> theQs = new ExamService().GetQuestions(theMainExa);
              //  examCodes.AddQuestion(theQs);
                work.ExamRepository.Delete(theMainExam);
                work.Save();

              //  work.ExamRepository.Delete(theExam);
               // work.Save();
                // TODO: Add delete logic here

                return RedirectToAction("LoadExamCodes");
            }
            catch
            {
                return View();
            }
        }
    }
}
