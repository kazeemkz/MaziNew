using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
    public class Question
    {
        public int QuestionID { get; set; }
        public ICollection<Choice> Choice { get; set; }
        [Required]
        public int OrderNumber { get; set; }
        // [Required]
        //public int OptionNumber { get; set; }
        public int Point { get; set; }
        [Required]
        public string Text { get; set; }
        public virtual Exam Exam { get; set; }
        public int ExamID { get; set; }

     

        [NotMapped]
        public bool Delete { get; set; }

        [NotMapped]
        public string Choice1 { get; set; }
        [NotMapped]
        public string Choice2 { get; set; }
        [NotMapped]
        public string Choice3 { get; set; }
        [NotMapped]
        public string Choice4 { get; set; }
        [NotMapped]
        public string Choice5 { get; set; }
        [NotMapped]
        public string Answer { get; set; }

        [NotMapped]
        private IList<Choice> _choices = new List<Choice>();
        [NotMapped]
        public IList<Choice> Choices
        {
            get { return _choices; }
            set { _choices = value; }
        }

        public void AddChoice(Choice choice)
        {
            _choices.Add(choice);
            choice.Question = this;

        }

    }
}
