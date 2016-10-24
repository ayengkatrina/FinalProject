using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using PasteBookEntityLibrary;

namespace BusinessLogicLibrary
{
   public class FriendManager
    {
        DataAccess<FRIENDS_TABLE> dataAccess = new DataAccess<FRIENDS_TABLE>();
        //public List<FRIENDS_TABLE> GetListOfFriends(int userID)
        //{
        //    List<FRIENDS_TABLE> friendsList = new List<FRIENDS_TABLE>();
        //    using (var context = new PasteBookDBEntities())
        //    {
        //        friendsList = context.
        //    }
        //}
        public List<FRIENDS_TABLE> GetListOfFriends(int userID)
        {
            List<FRIENDS_TABLE> friendslist = new List<FRIENDS_TABLE>();
           
            using (var context = new PasteBookDBEntities())
            {
                var list = context.FRIENDS_TABLE.Where(x => x.FRIEND_ID == userID && x.REQUEST == "Y");
                var secondList = context.FRIENDS_TABLE.Where(x => x.USER_ID == userID && x.REQUEST == "Y");

                foreach(var item in list)
                {
                    friendslist.Add(item);
                }

                foreach(var item in secondList)
                {
                    friendslist.Add(item);
                }

                return friendslist;
            }

             
        }
    }
}
