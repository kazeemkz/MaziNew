using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
    //http://stackoverflow.com/questions/4335636/amazon-book-search-api-using-asp-net
    //http://www.geekscrapbook.com/2012/06/05/getting-started-with-amazon-aws-via-c/
    //http://www.tmhshop.com/9780070667266
    //http://www.textbookw.com/search.asp?p=&filteryr=&genre=&alpha=&page=&source=search&searchstr=9780321415066&pid=1115&pauth=Ramez+Elmasri%2C+Shamkant+B%2E+Navathe&pname=Fundamentals+of+Database+Systems
    public class Book
    {
        public int BookID { get; set; }
        [Required]
        [Display(Name = "Item Title")]
        public string ItemTitle { get; set; }


        [NotMapped]
      
       [Display(Name = "Subject Area")]
        public string SubjectArea { get; set; }

        
        public virtual SubjectArea SubjectAreaNavigate { get; set; }

     
      //   public string SubjectArea { get; set; }
         public int SubjectAreaID { get; set; }

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
        public string PageNumber { get; set; }
        //[Display(Name = "ISBN")]
        public string ISBN { get; set; }
       // [Display(Name = "Number of  Disk(s) if Required")]

        public string ISBN13 { get; set; }

        [Display(Name = "Number of  Disk(s) if Required")]
        public int DiskNumbers { get; set; }
        public virtual Column Column { get; set; }
        public int ColumnID { get; set; }
        public byte[] FileData { get; set; }

        public string Edition { get; set; }

        [Display(Name = "Item Description")]
        public string ItemSummary { get; set; }

        public DateTime DateCreated { get; set; }
        public int BorrowedTimes { get; set; }

    }
}
