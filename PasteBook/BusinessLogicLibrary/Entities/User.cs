using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLibrary
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public System.DateTime Birthday { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public byte[] ProfilePic { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string AboutMe { get; set; }
        public string EmailAddress { get; set; }
    }
}
