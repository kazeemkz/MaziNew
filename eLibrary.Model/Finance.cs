using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
  public  class Finance
    {

        public int FinanceID { get; set; }
        [Display(Name = "Paid By")]
        public string PaidBy { get; set; }
       [Display(Name = "Date paid")]
        public DateTime DatePaid { get; set; }
       [Display(Name = "Amount Paid")]
        public int AmountPaid { get; set; }
        
    }
}
