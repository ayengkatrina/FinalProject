using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using PasteBookEFLibrary;

namespace BusinessLogicLibrary
{
    public class CommentManager
    {
        DataAccess<COMMENTS_TABLE> dataAccess = new DataAccess<COMMENTS_TABLE>();
        StoredProcDataAccess procDataAccess = new StoredProcDataAccess();


        public bool CommentOnAPost(COMMENTS_TABLE comment)
        {
            bool result;
            comment.DATE_CREATED = DateTime.Now;
            result = dataAccess.Create(comment);
            return result;

        }

        public List<COMMENTS_TABLE> GetAllCommentsOfAPost(int postID)
        {
            List<COMMENTS_TABLE> commentList = new List<COMMENTS_TABLE>();

            commentList = dataAccess.GetOne(x => x.POST_ID == postID);


            return commentList;

        }

       public List<COMMENTS_TABLE> GetComments(int postID)
        {
            var commentList = procDataAccess.GetComments(postID).ToList();

            return commentList;
        }

       
    }
}
