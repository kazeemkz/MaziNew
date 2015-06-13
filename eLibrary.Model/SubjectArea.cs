using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
   public class SubjectArea
    {
       public int SubjectAreaID { get; set; }
       [Display(Name = "Subject Area")]
       public string TheSubjectArea { get; set; }
       public List<Book> Books { get; set; }
    }
}
