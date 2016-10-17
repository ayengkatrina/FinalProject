using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class LikeModel
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int LikedBy { get; set; }
    }
}