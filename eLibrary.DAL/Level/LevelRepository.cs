using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
    public class LevelRepository : GenericRepository<Level>, ICourseRepository<Level>
    {
      public LevelRepository(eLContext context)
          : base(context)
      {

      }
    }
}
