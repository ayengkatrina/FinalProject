using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEntityLibrary;
using System.Data.Entity.Validation;

namespace DataAccessLibrary
{
  public  class StoredProcDataAccess
    {
        public List<FRIEND_POST_USER_JOIN3_Result> PostInTheNewsFeed(int profileID)
        {

            List<FRIEND_POST_USER_JOIN3_Result> postTableList = new List<FRIEND_POST_USER_JOIN3_Result>();

            try
            {
                using (var context = new PasteBookDBEntities())
                {
                    postTableList = (from post in context.FRIEND_POST_USER_JOIN3(profileID)
                                     orderby post.CREATED_DATE descending
                                     select post).ToList();



                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return postTableList;


        }

        public List<NEWSFEEDPOST_Result> NewsFeedPost(int profileID)
        {
            using (var context = new PasteBookDBEntities())
            {
              var  postTableList = (from post in context.NEWSFEEDPOST(profileID)
                                 orderby post.CREATED_DATE descending
                                 select post).ToList();

                return postTableList;

            }
        }


        public List<COMMENTS_TABLE> GetComments(int postID)
        {
            List<COMMENTS_TABLE> commentList = new List<COMMENTS_TABLE>();
            using (var context = new PasteBookDBEntities())
            {
                commentList = context.COMMENTS_TABLE.Include("POST_TABLE").Include("USER_TABLE").Where(x => x.POST_ID == postID).ToList();
                return commentList;
            }
        }


       

    }
}
