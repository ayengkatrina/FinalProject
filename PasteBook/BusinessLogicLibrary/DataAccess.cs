using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLibrary;
using System.Data.SqlClient;
using PasteBookEntityLibrary;
using System.Data.Entity.Validation;

namespace BusinessLogicLibrary
{
    public class DataAccess
    {
               
       

        public bool CreateAccount(User user)
        {
            bool result;
            try
            {

                using (var content = new PasteBookDBEntities())
                {
                    user.Birthday = user.Birthday.Date;
                    user.DateCreated = DateTime.Now.Date;
                    content.USER_TABLE.Add(Mapper.ToUSER_TABLE(user));

                    int numberCreated = content.SaveChanges();
                    if (numberCreated > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    return result;
                }
            }

            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors .SelectMany(x => x.ValidationErrors) .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public User GetUserAccount(string email)
        {
            User user = new User();
            try
            {
                using (var content = new PasteBookDBEntities())
                {
                    user = Mapper.ToUser(content.USER_TABLE.Where(x => x.EMAIL_ADDRESS == email).SingleOrDefault());

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
            return user;
        }


        public List<RefCountry> GetAllCountries()
        {
            List<RefCountry> countryList = new List<RefCountry>();

            try { 
                  using (var content = new PasteBookDBEntities())
                     {

                         foreach (var item in content.REF_COUNTRY)
                         {
                             countryList.Add(Mapper.ToRefCountry(item));
                         }


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
            return countryList;
        }

        public bool CreatePost(Post post)
        {
            bool result;
            using (var context = new PasteBookDBEntities())
            {
                post.CreatedDate = DateTime.Now;
                context.POST_TABLE.Add(Mapper.ToPOST_TABLE(post));

                int numberCreated = context.SaveChanges();
                if (numberCreated > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }

        }

        public List<Post> GetPost()
        {
           List< Post> postList = new List<Post>();
            List<POST_TABLE> postTableList = new List<POST_TABLE>();

            try
            {
                using (var context = new PasteBookDBEntities())
                {
                    postTableList = (from post in context.POST_TABLE
                                     orderby post.CREATED_DATE descending
                                     select post).ToList();

                    foreach (var item in postTableList)
                    {
                        postList.Add(Mapper.ToPost(item));
                    }

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

            return postList;

            
        }

        public  bool CreateComment(Comment comment)
        {
            bool result;
            int numberCreated = 0;
            using (var context = new PasteBookDBEntities())
            {
                comment.DateCreated = DateTime.Now;
                context.COMMENTS_TABLE.Add(Mapper.ToCOMMENT_TABLE(comment));

                 numberCreated = context.SaveChanges();
                if (numberCreated > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
           
        }

        public List<Comment> GetComments(int postID)
        {
            List<Comment> commentList = new List<Comment>();
            List<COMMENTS_TABLE> commentTableList = new List<COMMENTS_TABLE>();
            
            using (var context = new PasteBookDBEntities())
            {
                commentTableList = (from comment in context.COMMENTS_TABLE
                                 orderby comment.DATE_CREATED descending
                               where comment.POST_ID == postID
                                 select comment).ToList();
                foreach(var item in commentTableList)
                {
                    commentList.Add(Mapper.ToComment(item));
                }

            }
            return commentList;
        }

        public bool LikePost(Like like)
        {
            bool result;
            using(var context = new PasteBookDBEntities())
            {
                context.LIKES_TABLE.Add(Mapper.ToLIKES_TABLE(like));
                int numberCreated = context.SaveChanges();

               if(numberCreated > 0)
                {
                    result = true;
                }else
                {
                    result = false;
                }
                return result;
            }
            
        }

        //public List<Like> GetFriendListWhoLikeAPost(int postID)
        //{

        //}

        public bool CheckEmailIfAlreadyExist(string email)
        {
            bool result;
            using (var context = new PasteBookDBEntities())
            {
                if (context.USER_TABLE.Any(x => x.EMAIL_ADDRESS == email))
                {
                     result = true;
                }else
                {
                     result = false;
                }
                return result;
            }

        }
    }

}

