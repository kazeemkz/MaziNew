using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
    public class Column
    {
        public int ColumnID { get; set; }
        [Display(Name = "Column Name")]
        public string ColumnName { get; set; }
        public int RowID { get; set; }
        public virtual Row Row { get; set; }
        public List<Book> Book { get; set; }
    }
}
