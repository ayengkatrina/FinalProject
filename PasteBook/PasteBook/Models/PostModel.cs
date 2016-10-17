using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class PostModel
    {
        public int PostID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Content { get; set; }
        public int ProfileID { get; set; }
        public int PosterID { get; set; }
    }
}