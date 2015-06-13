using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
  public class ChapterRepository :GenericRepository<Chapter>, ICourseRepository<Chapter>
    {
      public ChapterRepository(eLContext context)
          : base(context)
      {

      }
    }
}
