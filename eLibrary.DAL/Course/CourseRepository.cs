using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
  public class CourseRepository :GenericRepository<Course>, ICourseRepository<Course>
    {
      public CourseRepository(eLContext context)
          : base(context)
      {

      }
    }
}
