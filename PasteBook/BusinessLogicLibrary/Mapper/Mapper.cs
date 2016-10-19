using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEntityLibrary;

namespace BusinessLogicLibrary
{
    public static class Mapper
    {
        public static USER_TABLE ToUSER_TABLE(User user)
        {
            USER_TABLE userTable = new USER_TABLE()
            {
                ID = user.UserID,
                USER_NAME = user.UserName,
                PASSWORD = user.Password,
                SALT = user.Salt,
                FIRST_NAME = user.FirstName,
                LAST_NAME = user.Lastname,
                BIRTHDAY = user.Birthday,
                COUNTRY_ID = user.CountryID,
                MOBILE_NO = user.MobileNo,
                GENDER = user.Gender,
                PROFILE_PIC = user.ProfilePic,
                DATE_CREATED = user.DateCreated,
                ABOUT_ME = user.AboutMe,
                EMAIL_ADDRESS = user.EmailAddress
                
            };

            return userTable;

        }

        public static User ToUser(USER_TABLE user)
        {
            User userTable = new User()
            {
                UserID = user.ID,
                UserName = user.USER_NAME,
                Password = user.PASSWORD,
                Salt = user.SALT,
                FirstName = user.FIRST_NAME,
                Lastname = user.LAST_NAME,
                Birthday = user.BIRTHDAY,
                CountryID = user.COUNTRY_ID,
                MobileNo = user.MOBILE_NO,
                Gender = user.GENDER,
                ProfilePic = user.PROFILE_PIC,
                DateCreated = user.DATE_CREATED,
                AboutMe = user.ABOUT_ME,
                EmailAddress = user.EMAIL_ADDRESS

               

            };

            return userTable;

        }

        public static REF_COUNTRY ToREF_COUNTRY(RefCountry refCountry)
        {
            REF_COUNTRY country = new REF_COUNTRY()
            {
                ID = refCountry.CountryID,
                COUNTRY = refCountry.Country
            };

            return country;
        }

        public static RefCountry ToRefCountry(REF_COUNTRY refCountry)
        {
            RefCountry country = new RefCountry()
            {
                CountryID = refCountry.ID,
                Country = refCountry.COUNTRY

            };
            return country;
        }

        public static POST_TABLE ToPOST_TABLE(Post post)
        {
            POST_TABLE postTable = new POST_TABLE()
            {
                ID = post.PostID,
                CREATED_DATE = post.CreatedDate,
                CONTENT = post.Content,
                PROFILE_ID = post.ProfileID,
                POSTER_ID = post.PosterID

            };
            return postTable;
        }

        public static Post ToPost(POST_TABLE postTable)
        {
            Post post = new BusinessLogicLibrary.Post()
            {
                PostID = postTable.ID,
                CreatedDate = postTable.CREATED_DATE,
                Content = postTable.CONTENT,
                ProfileID = postTable.PROFILE_ID,
                PosterID = postTable.POSTER_ID
            };
            return post;
        }

        public static NOTIFICATION_TABLE ToNOTIFICATION_TABLE(Notification notif)
        {
            NOTIFICATION_TABLE notifTable = new NOTIFICATION_TABLE()
            {
                RECEIVER_ID = notif.ReceiverID,
                NOTIF_TYPE = notif.NotifType,
                SENDER_ID = notif.SenderID,
                CREATED_DATE = notif.CreatedDate,
                COMMENT_ID = notif.CommentID,
                POST_ID = notif.PostID,
                ID = notif.ID,
                SEEN = notif.Seen
            };

            return notifTable;
        }

        public static Notification ToNotification(NOTIFICATION_TABLE notifTable)
        {
            Notification notif = new Notification()
            {
                ReceiverID = notifTable.RECEIVER_ID,
                NotifType = notifTable.NOTIF_TYPE,
                SenderID = notifTable.SENDER_ID,
                CreatedDate = notifTable.CREATED_DATE,
                CommentID = notifTable.COMMENT_ID,
                PostID = notifTable.POST_ID,
                ID = notifTable.ID,
                Seen = notifTable.SEEN
            };
            return notif;
        }


        public static LIKES_TABLE ToLIKES_TABLE(Like like)
        {
            LIKES_TABLE likeTable = new LIKES_TABLE()
            {
                ID = like.ID,
                POST_ID = like.PostID,
                LIKED_BY = like.LikedBy

            };
            return likeTable;
        }

        public static Like ToLike(LIKES_TABLE likeTable)
        {
            Like like = new Like()
            {
                ID = likeTable.ID,
                PostID = likeTable.POST_ID,
                LikedBy = likeTable.LIKED_BY
            };
            return like;
        }

        public static FRIENDS_TABLE ToFRIEND_TABLE(Friend friend)
        {
            FRIENDS_TABLE friendTable = new FRIENDS_TABLE()
            {
                ID = friend.ID,
                USER_ID = friend.UserID,
                FRIEND_ID = friend.FriendID,
                REQUEST = friend.Request,
                BLOCKED = friend.Blocked,
                CREATED_DATE = friend.CreatedDate
            };
            return friendTable;
        }

        public static Friend ToFriend(FRIENDS_TABLE friendTable)
        {
            Friend friend = new Friend()
            {
                ID = friendTable.ID,
                UserID = friendTable.USER_ID,
                FriendID = friendTable.FRIEND_ID,
                Request = friendTable.REQUEST,
                Blocked = friendTable.BLOCKED,
                CreatedDate = friendTable.CREATED_DATE
            };
            return friend;
        }

        public static COMMENTS_TABLE ToCOMMENT_TABLE(Comment comment)
        {
            COMMENTS_TABLE commentTable = new COMMENTS_TABLE()
            {
                ID = comment.ID,
                POST_ID = comment.PostID,
                POSTER_ID = comment.PosterID,
                CONTENT = comment.Content,
                DATE_CREATED = comment.DateCreated
            };
            return commentTable;
        }

        public static Comment ToComment(COMMENTS_TABLE commentTable)
        {
            Comment comment = new BusinessLogicLibrary.Comment()
            {
                ID = commentTable.ID,
                PostID = commentTable.POST_ID,
                PosterID = commentTable.POSTER_ID,
                Content = commentTable.CONTENT,
                DateCreated = commentTable.DATE_CREATED

            };
            return comment;
        }

        public static PostUserJoin ToPostUserJoin(POST_USER_JOIN_Result postUserJoin)
        {
            PostUserJoin postUser = new PostUserJoin()
            {
                FirstName = postUserJoin.FIRST_NAME,
                LastName = postUserJoin.LAST_NAME,
                ProfilePic = postUserJoin.PROFILE_PIC,
                Content = postUserJoin.CONTENT,
                CreatedDate = postUserJoin.CREATED_DATE

            };
            return postUser;
        }

    }
}
