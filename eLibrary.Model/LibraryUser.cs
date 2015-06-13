using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
    public class LibraryUser
    {
        public int LibraryUserID { get; set; }


        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string TelePhoneNumber { get; set; }
        ////for students
        //public string Level { get; set; }
        //public string LevelType { get; set; }
        //public string ClassTeacher { get; set; }

        ////for class teachers onlt
        //public string LevelTaught { get; set; }
        //public string LevelTaughtType { get; set; }

        //[Required, DataType(DataType.Date)]
        //public DateTime DOB { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }

         [Display(Name = "First Name")]
        // [Required]
        public string FirstName { get; set; }
        // [Required]
         [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //  [Required,  DataType(eLibrary.Model.LevelDictionary.LevelDicoList)]
        //[Required, DataType("eLibrary.Model.LevelDictionary.LevelDicoList")]
        //  [Required]
        public string Level { get; set; }
        //[Required]
        [Display(Name = "Class Type")]
        public string LevelType { get; set; }
        // [Display(Name = "Class Teacher Name")]
        public string ClassTeacher { get; set; }
        // public string Telephone { get; set; }

        //for class teachers onlt
        //  [Display(Name = "Class Taught")]
        public string LevelTaught { get; set; }
        // [Display(Name = "Class Taught Type")]
        public string LevelTaughtType { get; set; }
        // [Display(E)]
        //  [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Display(Name = "User Type")]
        public string UserType { get; set; }

        // [Display(Name = "User Name")]
        // [Required]
        //  public string Username { get; set; }
    }
}
