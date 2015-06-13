using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
    public class RowRepository : GenericRepository<Row>
    {
       public RowRepository(eLContext context)
            : base(context)
        { }
    }
}
