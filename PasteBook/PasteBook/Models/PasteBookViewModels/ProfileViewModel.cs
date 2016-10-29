using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PasteBookEFLibrary;

namespace PasteBook
{
    public class ProfileViewModel
    {
        public USER_TABLE userTable { get; set; }
        public string ConfirmPassword { get; set; }

        private string myVar;

        public string FullName
        {
            get { return userTable.FIRST_NAME + " " + userTable.LAST_NAME; }
            set { myVar = value; }
        }
    }
}