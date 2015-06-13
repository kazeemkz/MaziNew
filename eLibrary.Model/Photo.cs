using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLibrary.Model
{
  public  class Photo
    {
      public int PhotoID {get; set;}
      public int BookID { get; set; }
      public byte[] FileData { get; set; }
    }
}
