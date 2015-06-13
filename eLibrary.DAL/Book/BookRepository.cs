using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
    public class BookRepository : GenericRepository<eLibrary.Model.Book>, IBookRepository<eLibrary.Model.Book>
    {
        public BookRepository(eLContext context)
            : base(context)
        {

        }
    }
}
