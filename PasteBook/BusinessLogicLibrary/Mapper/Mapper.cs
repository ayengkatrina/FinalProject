using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBook_EntityFramework;

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
                ABOUT_ME = user.AboutMe
            };

            return userTable;

        }

        public static REF_COUNTRY RefCountryEntityMapper(RefCountry refCountry)
        {
            REF_COUNTRY country = new REF_COUNTRY()
            {
                ID = refCountry.CountryID,
                COUNTRY = refCountry.Country
            };

            return country;
        }

        public static POST_TABLE PostEntityMapper(Post post)
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

        public static NOTIFICATION_TABLE NotificationEntityMapper(Notification notif)
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


        public static LIKES_TABLE LikeEntityMapper(Like like)
        {
            LIKES_TABLE likeTable = new LIKES_TABLE()
            {
                ID = like.ID,
                POST_ID = like.PostID,
                LIKED_BY = like.LikedBy

            };
            return likeTable;
        }

        public static FRIENDS_TABLE FriendEntityMapper(Friend friend)
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

        public static COMMENTS_TABLE CommentEntityMapper(Comment comment)
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

    }
}
