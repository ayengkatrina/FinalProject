//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PasteBook_EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class NOTIFICATION_TABLE
    {
        public int RECEIVER_ID { get; set; }
        public string NOTIF_TYPE { get; set; }
        public int SENDER_ID { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public int COMMENT_ID { get; set; }
        public int POST_ID { get; set; }
        public int ID { get; set; }
        public string SEEN { get; set; }
    
        public virtual COMMENTS_TABLE COMMENTS_TABLE { get; set; }
        public virtual POST_TABLE POST_TABLE { get; set; }
        public virtual USER_TABLE USER_TABLE { get; set; }
        public virtual USER_TABLE USER_TABLE1 { get; set; }
    }
}
