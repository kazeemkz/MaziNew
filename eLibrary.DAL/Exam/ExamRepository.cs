using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository<Exam>
    {
        public ExamRepository(eLContext context)
          : base(context)
      {

      }
    }
}
