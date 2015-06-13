using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
   public class BorrowedItemRepository : GenericRepository<BorrowedItem>
    {
       public BorrowedItemRepository(eLContext context)
            : base(context)
        { }

    }
}
