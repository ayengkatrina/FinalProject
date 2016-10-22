using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class FriendPostUserJoinModel
    {
        public int FriendTableUserID { get; set; }
        public int FriendID { get; set; }
       
        public Nullable<System.DateTime> PostCreatedDate { get; set; }
        public string PostContent { get; set; }
        public int PostProfileID { get; set; }
        public int PostPosterID { get; set; }
        public int UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public byte[] ProfilePic { get; set; }
    }
}