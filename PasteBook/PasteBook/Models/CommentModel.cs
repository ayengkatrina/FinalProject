using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class CommentModel
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int PosterID { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}