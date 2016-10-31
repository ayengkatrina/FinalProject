using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PasteBookEFLibrary;

namespace PasteBook
{
    public class HomeViewModel
    {
        public List<POST_TABLE> PostsList { get; set; }
        public COMMENTS_TABLE Comment { get; set; }
        public POST_TABLE Post { get; set; }
    }
}