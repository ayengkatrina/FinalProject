using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLibrary
{
    class Post
    {
        public int PostID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Content { get; set; }
        public int ProfileID { get; set; }
        public int PosterID { get; set; }
    }
}
