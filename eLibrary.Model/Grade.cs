using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eLibrary.Model
{
    public class Grade
    {
        public double TotalPoints { get; set; }
        public double Score { get; set; }
        public Exam Exam { get; set; }
    }
}
