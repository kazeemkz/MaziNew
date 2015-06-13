using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace JobHustler.Models
{
    public class CreateUserViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //  [Required,  DataType(eLibrary.Model.LevelDictionary.LevelDicoList)]
        //[Required, DataType("eLibrary.Model.LevelDictionary.LevelDicoList")]
       // [Required]
        public string Level { get; set; }

        [Display(Name = "Class Type")]
        public string LevelType { get; set; }
        [Display(Name = "Class Teacher Name")]
        public string ClassTeacher { get; set; }
        public string Telephone { get; set; }

        //for class teachers onlt
        [Display(Name = "Class Taught")]
        public string LevelTaught { get; set; }
        [Display(Name = "Class Taught Type")]
        public string LevelTaughtType { get; set; }
        // [Display(E)]
        [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        [Required]
        public string UserType { get; set; }



        [Display(Name = "User Name")]
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Password (Again...)")]
        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email Address")]
        //  [Required, Email]
        [Required]
        public string Email { get; set; }

        //[Display(Name = "Secret Question")]
        //public string PasswordQuestion { get; set; }

        //[StringLength(100)]
        //[Display(Name = "Secret Answer")]
        //public string PasswordAnswer { get; set; }



        [Display(Name = "Initial Roles")]
      //  [Required]
        public IDictionary<string, bool> InitialRoles { get; set; }
    }
}