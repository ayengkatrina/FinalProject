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

        public static UserModel ToUserModel(User user)
        {
            UserModel userModel = new UserModel()
            {
                UserID = user.UserID,
                UserName = user.UserName,
                Password = user.Password,
                Salt = user.Salt,
                FirstName = user.FirstName,
                Lastname = user.Lastname,
                Birthday = user.Birthday,
                CountryID = user.CountryID,
                MobileNo = user.MobileNo,
                Gender = user.Gender,
                ProfilePic = user.ProfilePic,
                DateCreated = user.DateCreated,
                AboutMe = user.AboutMe,
                EmailAddress = user.EmailAddress
            };
            return userModel;
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


        public static PostModel ToPostModel(Post post)
        {
            PostModel postModel = new PostModel()
            {
                PostID = post.PostID,
                CreatedDate = post.CreatedDate,
                Content = post.Content,
                ProfileID = post.ProfileID,
                PosterID = post.PosterID

            };
            return postModel;
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

        public static NotificationModel ToNotificationModel(Notification notif)
        {
            NotificationModel notifModel = new PasteBook.NotificationModel()
            {
                ReceiverID = notif.ReceiverID,
                NotifType = notif.NotifType,
                SenderID = notif.SenderID,
                CreatedDate = notif.CreatedDate,
                CommentID = notif.CommentID,
                PostID = notif.PostID,
                ID = notif.ID,
                Seen = notif.Seen
            };
            return notifModel;
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

        public static LikeModel ToLikeModel(Like like)
        {
            LikeModel likeModel = new LikeModel()
            {
                ID = like.ID,
                PostID = like.PostID,
                LikedBy = like.LikedBy
            };
            return likeModel;
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

        public static FriendModel ToFriendModel(Friend friend)
        {
            FriendModel friendModel = new FriendModel()
            {
                ID = friend.ID,
                UserID = friend.UserID,
                FriendID = friend.FriendID,
                Request = friend.Request,
                Blocked = friend.Blocked,
                CreatedDate = friend.CreatedDate

            };
            return friendModel;
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

        public static CountryModel ToCountryModel(RefCountry refCountry)
        {
            CountryModel countryModel = new CountryModel()
            {
                CountryID = refCountry.CountryID,
                Country = refCountry.Country
            };
            return countryModel;
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

        public static CommentModel commentModel(Comment comment)
        {
            CommentModel commentModel = new CommentModel()
            {
                ID = comment.ID,
                PostID = comment.PostID,
                PosterID = comment.PosterID,
                Content = comment.Content,
                DateCreated = comment.DateCreated

            };
            return commentModel;
        }
    }
}