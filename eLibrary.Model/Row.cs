using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
    public  class Row
    {
        public int RowID { get; set; }
        public int ShelfID { get; set; }
        public virtual Shelf Self { get; set; }

        [Required]
        [Display(Name="Row Name")]
        public string RowName { get; set; }
        List<Column> Columns { get; set; }
    }
}
