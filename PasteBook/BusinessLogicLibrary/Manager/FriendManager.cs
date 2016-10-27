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
        FriendTableDataAccess friendTableDataAccess = new FriendTableDataAccess();
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

        public bool ConfirmFriendRequest(int friendID, int userID)
        {
            var result = friendTableDataAccess.ConfirmFriendRequest(friendID, userID);
            return result;
        }

        public bool SendFriendRequest(int friendID, int userID)
        {
            FRIENDS_TABLE friendTable = new FRIENDS_TABLE();
            friendTable.USER_ID = userID;
            friendTable.FRIEND_ID = friendID;
            friendTable.CREATED_DATE = DateTime.Now;
            friendTable.REQUEST = "N";
            friendTable.BLOCKED = "N";
            var resulr = dataAccess.Create(friendTable);
            return resulr;
        }

        public List<int> GetListOfFriendsIDThatYouInviteAsFriend(int userID)
        {
            List<FRIENDS_TABLE> listOfPendingRequest = new List<FRIENDS_TABLE>();

            using (var context = new PasteBookDBEntities())
            {

                var list = context.FRIENDS_TABLE.Where(x => x.USER_ID == userID && x.REQUEST == "N");

                foreach (var item in list)
                {
                    listOfPendingRequest.Add(item);
                }
                List<int> friendsUserID = new List<int>();


                friendsUserID = listOfPendingRequest.Select(x => x.FRIEND_ID).ToList();

                return friendsUserID;
            }

        }

        public bool CheckIfRejectionSuccess(int friendID, int userID)
        {
            var result = friendTableDataAccess.RejectFriendRequest(friendID, userID);
            return result;
        }

        public bool CheckIfYouAlreadySendAnInvite(int friendID, int userID)
        {
            List<int> friendsIDThatYouInvited = new List<int>();
            friendsIDThatYouInvited = GetListOfFriendsIDThatYouInviteAsFriend(userID);

            if (friendsIDThatYouInvited.Any(x => x.Equals(friendID)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<int> GetListOfFriendsIDThatSentYouARequest(int userID)
        {
            List<FRIENDS_TABLE> listOfPendingRequest = new List<FRIENDS_TABLE>();

            using (var context = new PasteBookDBEntities())
            {
               
                var list = context.FRIENDS_TABLE.Where(x => x.FRIEND_ID == userID && x.REQUEST == "N");

                foreach (var item in list)
                {
                    listOfPendingRequest.Add(item);
                }
                List<int> friendsUserID = new List<int>();

               
                friendsUserID = listOfPendingRequest.Select(x => x.USER_ID).ToList();

                return friendsUserID;
            }

        }

        public bool CheckIfFriendAlreadySentRequest(int friendID, int userID)
        {
            List<int> friendsIDThatSendRequest = new List<int>();
            friendsIDThatSendRequest = GetListOfFriendsIDThatSentYouARequest(userID);

            if (friendsIDThatSendRequest.Any(x => x.Equals(friendID)))
            {
                return true;
            }else
            {
                return false;
            }
        }

        

        
        public List<USER_TABLE> FriendsList(int userID)
        {
            List<USER_TABLE> friendsUserList = new List<USER_TABLE>();
            List<FRIENDS_TABLE> friend = new List<FRIENDS_TABLE>();
            friend = GetListOfFriends(userID).ToList();
            using (var context = new PasteBookDBEntities())
            {
                var friendsID = friend.Distinct().Select(x => x.USER_ID == userID ? x.FRIEND_ID : x.USER_ID).ToList();

                foreach (var item in friendsID)
                {
                    friendsUserList.Add(context.USER_TABLE.Where(x => x.ID == item).SingleOrDefault());
                }

                return friendsUserList;

            }
        //    public List<int> GetFriendsIDThatSentFriendRequestToYou(int userID)
        //{
        //    List<int> friend
        //}

        //    public bool CheckIfFriendAlreadySendFriendRequest(int friendID, int userID)
        //{
        //    List<FRIENDS_TABLE> friendslist = new List<FRIENDS_TABLE>();

        //    friendslist = GetListOfFriends(userID);

        //    if

        //}

        }

        public List<int> FriendsIdList(int userID)
        {
            List<FRIENDS_TABLE> friend = new List<FRIENDS_TABLE>();
            friend = GetListOfFriends(userID).ToList();
            using (var context = new PasteBookDBEntities())
            {
                var friendsID = friend.Distinct().Select(x => x.USER_ID == userID ? x.FRIEND_ID : x.USER_ID).ToList();

                return friendsID;
            }

            }

        public bool CheckIfFriend(int friendID, int userID)
        {
            List<int> friendsList = new List<int>();
            friendsList = FriendsIdList(userID);

            if (friendsList.Contains(friendID))
            {
                return true;
            }else
            {
                return false;
            }
        }


        public bool AddFriend(FRIENDS_TABLE friend)
        {
            var result = dataAccess.Create(friend);
            return result;
        }
    }
}
