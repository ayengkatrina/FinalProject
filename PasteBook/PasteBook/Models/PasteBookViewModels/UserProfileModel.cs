using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PasteBookEntityLibrary;

namespace PasteBook
{
    public class UserProfileModel
    {
        public USER_TABLE User { get; set; }
        public POST_TABLE Post { get; set; }
    }
}