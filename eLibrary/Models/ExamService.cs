using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eLibrary.DAL;
using eLibrary.Model;

namespace eLibrary.Models
{
    public class ExamService
    {
       
        public IList<Question> GetQuestions(Exam theMainExam)
        {
             UnitOfWork work = new UnitOfWork();
            List<Question> questions = new List<Question>();
            List<Choice> choices = new List<Choice>();
            Question myQuestion = new Question();
            int examID = theMainExam.ExamID;
            List<Question> theQuestions = work.QuestionRepository.Get(a => a.Exam.ExamID == examID).OrderBy(a => a.OrderNumber).ToList();
            foreach (Question question in theQuestions)
            {
                myQuestion = question;
                int dID = question.QuestionID;
                List<Choice> theChoices = work.ChoiceRepository.Get(a => a.QuestionID == dID).ToList();
                foreach (Choice choice in theChoices)
                {
                    myQuestion.AddChoice(choice);
                    // choices.Add(choice);
                }
                questions.Add(myQuestion);
            }
            //  questions.Add(myQuestion);

            return questions;
        }


        public IList<Question> GetQuestions(Exam theMainExam, ref UnitOfWork work)
        {

            List<Question> questions = new List<Question>();
            List<Choice> choices = new List<Choice>();
            Question myQuestion = new Question();
            int examID = theMainExam.ExamID;
            List<Question> theQuestions = work.QuestionRepository.Get(a => a.Exam.ExamID == examID).OrderBy(a => a.OrderNumber).ToList();
            foreach (Question question in theQuestions)
            {
                myQuestion = question;
                int dID = question.QuestionID;
                List<Choice> theChoices = work.ChoiceRepository.Get(a => a.QuestionID == dID).ToList();
                foreach (Choice choice in theChoices)
                {
                    myQuestion.AddChoice(choice);
                    // choices.Add(choice);
                }
                questions.Add(myQuestion);
            }
            //  questions.Add(myQuestion);

            return questions;
        }

        public Grade Grade(Exam toBeGradedExam)
        {
             UnitOfWork work = new UnitOfWork();
            Question thE = toBeGradedExam.Questions[0];
            List<Question> theQuestion = work.QuestionRepository.Get(a => a.QuestionID == thE.QuestionID).ToList();
            Question theQuestio = theQuestion[0];


            List<Exam> theExam = work.ExamRepository.Get(a => a.ExamID == theQuestio.ExamID).ToList();
            Exam persistedExam = new Exam();
            persistedExam = theExam[0];
            IList<Question> theQs = new ExamService().GetQuestions(theExam[0]);
            persistedExam.AddQuestion(theQs);

            var grade = new Grade() { Exam = persistedExam };

            foreach (var question in toBeGradedExam.Questions)
            {
                var persistedQuestion = (from q in persistedExam.Questions
                                         where q.QuestionID == question.QuestionID
                                         select q).SingleOrDefault();

                if (persistedQuestion != null)
                {
                    foreach (var choice in question.Choices)
                    {
                        var persistedChoice = (from c in persistedQuestion.Choices
                                               where c.ChoiceID == choice.ChoiceID
                                               select c).SingleOrDefault();

                        // sets the user choice in the actual exam fetched from database! 
                        persistedChoice.IsSelected = true;

                        grade.TotalPoints += persistedQuestion.Point;
                        if (persistedChoice.IsAnswer)
                        {
                            grade.Score += persistedQuestion.Point;
                        }
                    }
                }
            }

            return grade;
        }
    }
}
