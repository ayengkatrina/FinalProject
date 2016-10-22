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
            var list = dataAccess.GetOne(x => x.USER_TABLE1.ID == userID);
            return list;
        }
    }
}
