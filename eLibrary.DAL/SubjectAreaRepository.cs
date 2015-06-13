using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Model;

namespace eLibrary.DAL
{
    public class SubjectAreaRepository : GenericRepository<SubjectArea>
    {
        public SubjectAreaRepository(eLContext context)
            : base(context)
        { }
    }
}
