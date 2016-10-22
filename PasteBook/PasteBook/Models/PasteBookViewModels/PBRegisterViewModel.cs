using PasteBookEntityLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class PBRegisterViewModel
    {
        public USER_TABLE userTable { get; set; }

        [Required(ErrorMessage = "Please enter a confirmation password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }       
    }
}