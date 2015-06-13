using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
    public class TextBookRepository : GenericRepository<TextBook>, ITextBookRepository<TextBook>
    {
        public TextBookRepository(eLContext context)
          : base(context)
      {

      }
    }
}
