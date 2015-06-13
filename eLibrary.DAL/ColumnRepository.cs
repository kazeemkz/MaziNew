using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
    public class ColumnRepository : GenericRepository<Column>
    
    {

        public ColumnRepository(eLContext context)
            : base(context)
        { }

    }
}
