using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
   public class Shelf
    {
       public int ShelfID { get; set; }
       [Display(Name= "Shelf Name")]
     //  [Unique]
       public string ShelfName { get; set; }
       List<Row> Rows { get; set; }
    }
}
