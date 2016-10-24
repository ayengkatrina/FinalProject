using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PasteBookEntityLibrary;

namespace PasteBook
{
    public class SecurityModel
    {
        public string CurrentPassword { get; set; }
        public string NewEmailAddress { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}