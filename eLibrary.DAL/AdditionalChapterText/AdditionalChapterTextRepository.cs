using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
    public class AdditionalChapterTextRepository : GenericRepository<AdditionalChapterText>, ICourseRepository<AdditionalChapterText>
    {
      public AdditionalChapterTextRepository(eLContext context)
          : base(context)
      {

      }
    }
}
