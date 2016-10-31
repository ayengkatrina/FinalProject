using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEFLibrary;
using System.Data.Entity.Validation;

namespace DataAccessLibrary
{
  public  class StoredProcDataAccess
    {
        

        public List<COMMENTS_TABLE> GetComments(int postID)
        {
            List<COMMENTS_TABLE> commentList = new List<COMMENTS_TABLE>();
            using (var context = new PASTEBOOK_DBEntities1())
            {
                commentList = context.COMMENTS_TABLE.Include("POST_TABLE").Include("USER_TABLE").Where(x => x.POST_ID == postID).ToList();
                return commentList;
            }
        }

       

    }
}
