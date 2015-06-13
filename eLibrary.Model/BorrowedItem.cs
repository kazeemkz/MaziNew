using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
    public class BorrowedItem
    {
        public int BorrowedItemID { get; set; }
        public int BookID { get; set; }
        public string ItemName { get; set; }
        public string ISBN { get; set; }
        // [Display(Name = "Telephone/GSM Number")]
       // public string Telephone { get; set; }
        public string ItemType { get; set; }
        [Required]
        [Display(Name = "Student/Staff Identification")]
        public string UserID { get; set; }

        public DateTime TimeBorrowed { get; set; }
        [Display(Name = "Date to be Returned")]
        [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy  HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime TimeToBeReturned { get; set; }
    }
}
