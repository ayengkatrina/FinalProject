using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class PostUserJoinModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePic { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}