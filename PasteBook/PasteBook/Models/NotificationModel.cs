using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class NotificationModel
    {
        public int ReceiverID { get; set; }
        public string NotifType { get; set; }
        public int SenderID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int ID { get; set; }
        public string Seen { get; set; }
    }
}