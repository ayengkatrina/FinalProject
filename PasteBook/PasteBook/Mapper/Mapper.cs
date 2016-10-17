using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLibrary;

namespace PasteBook
{
    public static class Mapper
    {
        public static User ToUser(UserModel userModel)
        {
            User user = new User()
            {
                UserID = userModel.UserID,
                UserName = userModel.UserName,
                Password = userModel.Password,
                Salt = userModel.Salt,
                FirstName = userModel.FirstName,
                Lastname = userModel.Lastname,
                Birthday = userModel.Birthday,
                CountryID = userModel.CountryID,
                MobileNo = userModel.MobileNo,
                Gender = userModel.Gender,
                ProfilePic = userModel.ProfilePic,
                DateCreated = userModel.DateCreated,
                AboutMe = userModel.AboutMe,
                EmailAddress = userModel.EmailAddress


            };
            return user;
        }

        public static Post ToPost (PostModel postModel)
        {
            Post post = new Post()
            {
                PostID = postModel.PostID,
                CreatedDate = postModel.CreatedDate,
                Content = postModel.Content,
                ProfileID = postModel.ProfileID,
                PosterID = postModel.PosterID
            };
            return post;
        }

        public static Notification ToNotification(NotificationModel notifModel)
        {
            Notification notif = new Notification()
            {
                ReceiverID = notifModel.ReceiverID,
                NotifType = notifModel.NotifType,
                SenderID = notifModel.SenderID,
                CreatedDate = notifModel.CreatedDate,
                CommentID = notifModel.CommentID,
                PostID = notifModel.PostID,
                ID = notifModel.ID,
                Seen = notifModel.Seen,

            };
            return notif;

        }

        public static Like ToLike(LikeModel likeModel)
        {
            Like like = new Like()
            {
                ID = likeModel.ID,
                PostID = likeModel.PostID,
                LikedBy = likeModel.LikedBy
            };
            return like;
        }

        public static Friend ToFriend(FriendModel friendModel)
        {
            Friend friend = new Friend()
            {
                ID = friendModel.ID,
                UserID = friendModel.UserID,
                FriendID = friendModel.FriendID,
                Request = friendModel.Request,
                Blocked = friendModel.Blocked,
                CreatedDate = friendModel.CreatedDate
            };
            return friend;
        }

        public static RefCountry ToRefCountry(CountryModel country)
        {
            RefCountry refCountry = new RefCountry()
            {
                CountryID = country.CountryID,
                Country = country.Country
            };
            return refCountry;
        }

        public static Comment ToComment(CommentModel commentModel)
        {
            Comment comment = new Comment()
            {
                ID = commentModel.ID,
                PostID = commentModel.PostID,
                PosterID = commentModel.PosterID,
                Content = commentModel.Content,
                DateCreated = commentModel.DateCreated
            };
            return comment;
        }
    }
}