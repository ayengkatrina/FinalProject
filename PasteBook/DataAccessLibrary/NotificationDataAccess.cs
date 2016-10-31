using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEFLibrary;

namespace DataAccessLibrary
{
    public class NotificationDataAccess
    {
        public bool notificationisAlreadyClick(NOTIFICATION_TABLE notificationData)
        {
            int numberChanges=0;
            NOTIFICATION_TABLE notification = new NOTIFICATION_TABLE();
            using (var context = new PASTEBOOK_DBEntities1())
            {
                var list = context.NOTIFICATION_TABLE.Where(x => x.SEEN == "N");

                foreach(var item in list)
                {
                    item.SEEN = "Y";
                    numberChanges = context.SaveChanges();
                }

                if(numberChanges > 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
            
        }

        public List<NOTIFICATION_TABLE> NotificationList(int userID)
        {
            List<NOTIFICATION_TABLE> notifList = new List<NOTIFICATION_TABLE>();
            List<int> notifListThatTheUserIsNotTheSender = new List<int>();

            using (var context = new PASTEBOOK_DBEntities1())
            {


                notifList = context.NOTIFICATION_TABLE.Include("USER_TABLE1").Where(x => x.RECEIVER_ID == userID && x.SENDER_ID != userID).ToList();
                return notifList;
            }
        }


    }
}
