using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
    public class Level
    {
        public int LevelID { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Course { get; set; }
    }
}
