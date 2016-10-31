using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PasteBookEFLibrary;
using System.ComponentModel.DataAnnotations;

namespace PasteBook
{
    public class CreateAccountViewModel
    {
        public USER_TABLE User { get; set; }

        public List<REF_COUNTRY> CountryList { get; set; }

        [StringLength(50, ErrorMessage = "Confirmation Password must not be longer than 50 characters")]
        [Required(ErrorMessage = "Please enter a confirmation password.")]

        public string CONFIRM_PASSWORD { get; set; }


    }
}