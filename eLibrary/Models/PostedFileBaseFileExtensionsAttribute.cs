using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;

namespace eLibrary.Models
{
    public class PostedFileBaseFileExtensionsAttribute : DataAnnotationsExtensions.FileExtensionsAttribute
{

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as HttpPostedFileBase; 
        if (file == null)
        {
            return new ValidationResult("No File Specified");
        }

        return base.IsValid(file.FileName, validationContext);
    }
}

}