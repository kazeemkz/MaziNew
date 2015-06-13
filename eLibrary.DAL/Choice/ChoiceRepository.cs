using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
  public class ChoiceRepository :GenericRepository<Choice>, IChoiceRepository<Choice>
    {
      public ChoiceRepository(eLContext context)
          : base(context)
      {

      }
    }
}
