using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLibrary
{
    class Comment
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int PosterID { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}
