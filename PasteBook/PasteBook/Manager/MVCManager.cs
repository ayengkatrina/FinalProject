using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLibrary;
using System.Web.Mvc;

namespace PasteBook
{
    public class MVCManager : Controller
    {
        DataAccess dataAccess = new DataAccess();
        Manager BLManager = new Manager();

        public bool RegisterAccount(UserModel user)
        {
            bool result = BLManager.SimulateUserCreation(Mapper.ToUser(user));
            return result;
        }

        public bool Login(string email, string password)
        {
          bool result =  BLManager.SimulateLogin(email, password);
            return result;
        }

        public UserModel GetUserAccount(string email)
        {
            var userList = Mapper.ToUserModel(dataAccess.GetUserAccount(email));

            return userList;

        }

        public bool CheckEmailIfAlreadyExist(string email)
        {
            bool result = dataAccess.CheckEmailIfAlreadyExist(email);
                return result;
        }

        public List<PostModel> GetAllPost()
        {
            List<PostModel> postList = new List<PostModel>();

           var list = dataAccess.GetPost();

            foreach(var item in list)
            {
                postList.Add(Mapper.ToPostModel( item));
            }
            return postList;
            
        }

        public bool CreatePost(PostModel postModel)
        {
            var post = dataAccess.CreatePost(Mapper.ToPost(postModel));

            return post;
        }

        public List<CommentModel> GetComments(int postID)
        {
            List<CommentModel> commentList = new List<CommentModel>();
          var list =  dataAccess.GetComments(postID);
            foreach(var item in list)
            {
                commentList.Add(Mapper.ToCommentModel(item));
            }
            return commentList;
        }

        public bool CreateComment(CommentModel commentModel)
        {
            var comment = dataAccess.CreateComment(Mapper.ToComment(commentModel));
            return comment;
        }

        //public void SendVerificationEmail(UserModel userModel)
        //{
        //    var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
        //    var message = new MailMessage();
        //    message.To.Add(new MailAddress("recipient@gmail.com"));  // replace with valid value 
        //    message.From = new MailAddress("sender@outlook.com");  // replace with valid value
        //    message.Subject = "Your email subject";
        //    message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
        //    message.IsBodyHtml = true;
        //}
    }
}