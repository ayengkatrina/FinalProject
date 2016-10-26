using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEntityLibrary;

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

        public bool SendFriendRequest(int friendID, int userID)
        {
            int numberSave = 0;
            using (var context = new PasteBookDBEntities())
            {
                FRIENDS_TABLE friendTable = context.FRIENDS_TABLE.Where(x => x.FRIEND_ID == friendID && x.USER_ID == userID).SingleOrDefault();
                if (friendTable != null)
                {
                    //user.ABOUT_ME = aboutMe;
                    friendTable.REQUEST = "N";
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
    }
}
