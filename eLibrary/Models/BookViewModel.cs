using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eLibrary.Models
{
    public class BookViewModel
    {
       // public int BookID { get; set; }
        [Required]
        [Display(Name = "Item Title")]
        public string ItemTitle { get; set; }

        [Display(Name = "Subject Area")]
        public string SubjectArea { get; set; }



        [Required]
        [Display(Name = "Author(s)")]
        public string Aurthors { get; set; }

        [Display(Name = "Publisher(s)")]
        public string Publisher { get; set; }
        [Display(Name = "Year Published")]
        // [DataType(DataType.Date), DisplayFormat(DataFormatString = "{yyyy  HH:mm:ss}", ApplyFormatInEditMode = true)]
        public int YearPublished { get; set; }

        [Required]
        [Display(Name = "Item Type")]
        public string ItemType { get; set; }


        public int BookQuantity { get; set; }
        [Required]
        [Display(Name = "Book Quantity")]
        public int QuantityInStoreIngnoreBorrow { get; set; }
        [Display(Name = "Page Number")]
        public int PageNumber { get; set; }
        //[Display(Name = "ISBN")]
        public string ISBN { get; set; }
        // [Display(Name = "Number of  Disk(s) if Required")]

        public string ISBN13 { get; set; }

        [Display(Name = "Number of  Disk(s) if Required")]
        public int DiskNumbers { get; set; }
       // public virtual Column Column { get; set; }
       // public int ColumnID { get; set; }
       // public byte[] FileData { get; set; }

        public string Edition { get; set; }

        [Display(Name = "Item Description")]
        public string ItemSummary { get; set; }

        public DateTime DateCreated { get; set; }
      //  public int BorrowedTimes { get; set; }
    }
}