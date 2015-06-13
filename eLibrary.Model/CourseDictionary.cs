using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eLibrary.Model
{
   public class CourseDictionary
    {
        public static SelectList CourseDicoList(string theLevel)
        { 
            return new SelectList(CourseDic, "Value", "Key");

           // ChapterRepository the = new ChapterRepository();
        }

        public static readonly IDictionary<string, string> CourseDic = new Dictionary<string, string> 
       {{"Choose..",""} ,
        {"English","English"},
         {"Mathematics","Mathematics"},
         {"Science","Science"},
         {"Yoruba","Yoruba"},
        {"Egbo","Egbo"},
         {"IRK","IRK"},
      
       };
    }
}
