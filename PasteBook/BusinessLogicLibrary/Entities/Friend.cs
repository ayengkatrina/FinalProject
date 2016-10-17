using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLibrary
{
    class Friend
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int FriendID { get; set; }
        public string Request { get; set; }
        public string Blocked { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
