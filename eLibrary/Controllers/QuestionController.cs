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
    [Authorize]
    public class QuestionController : Controller
    {
        UnitOfWork work = new UnitOfWork();

        //
        // GET: /Question/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Question/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Question/Create

        public ActionResult Create(string examCode = null)
        {
            ViewBag.ExamCode = examCode;
            return View("Create2");
        }

        //
        // POST: /Question/Create

        [HttpPost]
        public ActionResult Create(Question model)
        {
            try
            {
                ICollection<Choice> theChoices = new List<Choice>();
                //  if (ModelState.IsValid)
                // {
                //List<Level>  theLevel =  work.LevelRepository.Get(a => a.Name.Equals(model.Exam.LevelName)).ToList();

                //  string theExamCode = model.Exam.ExamCode;


                //  List<Course> theCourse = work.CourseRepository.Get(a => a.CourseID == model.Exam.CourseID).ToList();
                List<Exam> theExama = work.ExamRepository.Get(a => a.ExamCode == model.Exam.ExamCode).ToList();
                Exam theMainExam = theExama[0];
                model.Exam = theMainExam;
                model.ExamID = theMainExam.ExamID;


                //model.Exam.LevelID = theLevel[0].LevelID;
                // model.Exam.CourseID = theCourse[0].CourseID;

                // Exam theMainExam = new Exam();

                // List<Choice> theChoices = new List<Choice>();
                Choice theChoice1 = new Choice();
                if (model.Answer.Equals("A"))
                    theChoice1.IsAnswer = true;
                else
                    theChoice1.IsAnswer = false;
                theChoice1.Question = model;
                theChoice1.Text = model.Choice1;

                Choice theChoice2 = new Choice();
                if (model.Answer.Equals("B"))
                    theChoice2.IsAnswer = true;
                else
                    theChoice2.IsAnswer = false;
                theChoice2.Question = model;
                theChoice2.Text = model.Choice2;

                Choice theChoice3 = new Choice();
                if (model.Answer.Equals("C"))
                    theChoice3.IsAnswer = true;
                else
                    theChoice3.IsAnswer = false;
                theChoice3.Question = model;
                theChoice3.Text = model.Choice3;

                Choice theChoice4 = new Choice();
                if (model.Answer.Equals("D"))
                    theChoice4.IsAnswer = true;
                else
                    theChoice4.IsAnswer = false;
                theChoice4.Question = model;
                theChoice4.Text = model.Choice4;

                theChoices.Add(theChoice1);
                theChoices.Add(theChoice2);
                theChoices.Add(theChoice3);
                theChoices.Add(theChoice4);


                if (model.Choice5 != null)
                {
                    Choice theChoice5 = new Choice();
                    if (model.Answer.Equals("E"))
                        theChoice5.IsAnswer = true;
                    else
                        theChoice5.IsAnswer = false;
                    theChoice5.Question = model;
                    theChoice5.Text = model.Choice5;
                    theChoices.Add(theChoice5);
                }


                // }
                // TODO: Add insert logic here
                model.Choice = theChoices;
                //  work.ExamRepository.Insert(model.Exam);
                work.QuestionRepository.Insert(model);
                work.Save();
                // work.ChoiceRepository.Insert();
                //  return RedirectToAction("Create");
                return RedirectToAction("Create", new { examCode = model.Exam.ExamCode });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Question/Edit/5
        //slot in the exam id
        public ActionResult Edit(int id)
        {
            Exam theExam = work.ExamRepository.GetByID(id);
            IList<Question> theQs = new ExamService().GetQuestions(theExam);
            QuestionViewModel theListofQs = new QuestionViewModel();

            foreach (var q in theQs)
            {
                theListofQs.TheQuestions.Add(q);
            }

            //for (int k = 0; k < theQs.Count(); k++)

            //    foreach (Choice q in theQs[k])
            //    {
            //        theListofQs.TheChoices.Add(q);
            //    }
            //{

            //}
            //  examCodes.AddQuestion(theQs);

            return View(theListofQs);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        public ActionResult Edit(QuestionViewModel theModel)
        {
            UnitOfWork work2 = new UnitOfWork();
            try
            {
                int counter = 0;
                foreach (var question in theModel.TheQuestions)
                {

                    if (question.Delete == true)
                    {
                        work2.QuestionRepository.Delete(question);
                        work2.Save();
                    }
                    else if (question.Delete == false)
                    {
                        counter = counter + 1;
                        question.OrderNumber = counter;
                        foreach (var chioice in question.Choices)
                        {
                            // counter = counter + 1;
                            work2.ChoiceRepository.Update(chioice);
                            work2.Save();
                        }
                        work2.QuestionRepository.Update(question);
                        work2.Save();
                    }

                }

                //  return RedirectToAction("Index", "Exam");
                return RedirectToAction("LoadExamCodes", "Exam");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Question/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Question/Delete/5

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
