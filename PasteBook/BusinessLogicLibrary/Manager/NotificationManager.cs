using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEFLibrary;
using DataAccessLibrary;

namespace BusinessLogicLibrary
{
   public class NotificationManager
    {
        DataAccess<NOTIFICATION_TABLE> dataAccess = new DataAccess<NOTIFICATION_TABLE>();
        NotificationDataAccess notifDataAccess = new NotificationDataAccess();
        public bool NotificationLike (int receiverID, int senderID, int postID)
        {
            NOTIFICATION_TABLE notification = new NOTIFICATION_TABLE();
            notification.RECEIVER_ID = receiverID;
            notification.SENDER_ID = senderID;
            notification.POST_ID = postID;
            //notification.COMMENT_ID = 
            notification.NOTIF_TYPE = "L";
            notification.SEEN = "N";
            notification.CREATED_DATE = DateTime.Now;

            bool result = dataAccess.Create(notification);
            return result;          

        }

        public bool NotificationComment(int receiverID, int senderID, int postID, int commentID)
        {
            NOTIFICATION_TABLE notification = new NOTIFICATION_TABLE();
            notification.RECEIVER_ID = receiverID;
            notification.SENDER_ID = senderID;
            notification.POST_ID = postID;
            notification.COMMENT_ID = commentID;
            notification.NOTIF_TYPE = "C";
            notification.SEEN = "N";
            notification.CREATED_DATE = DateTime.Now;
            bool result = dataAccess.Create(notification);
            return result;
        }

        public bool NotificationFriendRequest(int receiverID, int senderID)
        {
            NOTIFICATION_TABLE notification = new NOTIFICATION_TABLE();
            notification.RECEIVER_ID = receiverID;
            notification.SENDER_ID = senderID;
            notification.NOTIF_TYPE = "F";
            notification.SEEN = "N";
            notification.COMMENT_ID = null;
            notification.CREATED_DATE = DateTime.Now;
            bool result = dataAccess.Create(notification);
            return result;
        }

        public bool NotificationBadgeClick(NOTIFICATION_TABLE notificationData)
        {
            bool result = notifDataAccess.notificationisAlreadyClick(notificationData);
            return result;
        }

        public List<NOTIFICATION_TABLE> NotifList(int userID) 
        {
            List<NOTIFICATION_TABLE> notifList = new List<NOTIFICATION_TABLE>();

            notifList = notifDataAccess.NotificationList(userID);

            return notifList;
        }

    }
}
