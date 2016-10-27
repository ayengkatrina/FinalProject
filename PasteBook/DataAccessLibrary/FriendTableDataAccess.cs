using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEntityLibrary;
using System.Data.Entity;

namespace DataAccessLibrary
{
   public class FriendTableDataAccess
    {
        public bool ConfirmFriendRequest(int friendID, int userID )
        {
            int numberSave = 0;
            using (var context = new PasteBookDBEntities())
            {
                FRIENDS_TABLE friendTable = context.FRIENDS_TABLE.Where(x => x.FRIEND_ID == userID && x.USER_ID == friendID).SingleOrDefault();
                if (friendTable != null)
                {
                    //user.ABOUT_ME = aboutMe;
                    friendTable.REQUEST = "Y";
                    friendTable.CREATED_DATE = DateTime.Now;
                    numberSave = context.SaveChanges();

                }

            }
            if (numberSave > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RejectFriendRequest(int friendID, int userID)
        {
            int numberSave = 0;
            using (var context = new PasteBookDBEntities())
            {
                FRIENDS_TABLE friendTable = context.FRIENDS_TABLE.Where(x => x.FRIEND_ID == userID && x.USER_ID == friendID).SingleOrDefault();
                if (friendTable != null)
                {
                    context.FRIENDS_TABLE.Remove(friendTable);
                   numberSave = context.SaveChanges();

                }

            }
            if (numberSave > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
