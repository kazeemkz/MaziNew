using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
   public class Choice
    {
       public int ChoiceID { get; set; }
       public string Text { get; set; }
       public bool IsAnswer { get; set; }
       public bool IsSelected { get; set; }
       public virtual Question Question { get; set; }
       public int QuestionID { get; set; }
    }
}
