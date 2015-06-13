using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
    public class UserPhoto
    {
        public int UserPhotoID { get; set; }
        public byte[] PhotoImage { get; set; }
        //public virtual PrimarySchoolStudent PrimarySchoolStudent { get; set; }
        public LibraryUser Person { get; set; }
        // public int UserID { get; set; }
        public int LibraryUserID { get; set; }

    }
}
