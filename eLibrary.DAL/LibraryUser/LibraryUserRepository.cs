using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
    public class LibraryUserRepository : GenericRepository<LibraryUser>, ILibraryUserRepository<LibraryUser>
    {
        public LibraryUserRepository(eLContext context)
          : base(context)
      {

      }
    }
}
