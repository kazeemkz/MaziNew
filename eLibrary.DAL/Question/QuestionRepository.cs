using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
  public class QuestionRepository :GenericRepository<Question>, IQuestionRepository<Question>
    {
      public QuestionRepository(eLContext context)
          : base(context)
      {

      }
    }
}
