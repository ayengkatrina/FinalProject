using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class ContainerModel
    {
        public UserModel User { get; set; }
        public PostModel Post { get; set; }
        public NotificationModel Notification { get; set; }
        public LikeModel Like { get; set; }
        public FriendModel Friend { get; set; }
        public CountryModel Country { get; set; }
        public CommentModel Comment { get; set; }
    }
}