using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using PasteBookEntityLibrary;

namespace BusinessLogicLibrary
{
   
    public class PostManager
    {
        DataAccess<POST_TABLE> dataAccess = new DataAccess<POST_TABLE>();
        StoredProcDataAccess storedProcAccess = new StoredProcDataAccess();
        

        public bool CreatePost(POST_TABLE post)
        {
            post.CREATED_DATE = DateTime.Now;
           bool result = dataAccess.Create(post);
            return result;           

        }

        //public List<POST_TABLE> PostInTheNewsFeed(int userID)
        //{
        //    FriendManager friendManager = new FriendManager();
        //    List<POST_TABLE> postList = new List<POST_TABLE>();

        //    var listOfFriends = friendManager.GetListOfFriends(userID);

        //    using (var context = new PasteBookDBEntities())
        //    {
        //        var list = context.POST_TABLE.Include("USER_TABLE").Where(x=>x.POSTER_ID == listOfFriends.)
        //    }
        //}

     public List<NEWSFEEDPOST_Result> NewsFeedPost(int profileID)
        {
            List<NEWSFEEDPOST_Result> list = new List<NEWSFEEDPOST_Result>();
            list = storedProcAccess.NewsFeedPost(profileID);
            return list;
        }

        public List<POST_TABLE> TimelinePost(int profileID)
        {
            List<POST_TABLE> list = new List<POST_TABLE>();
            //list = dataAccess.GetOne(x => x.PROFILE_ID == profileID);

            using (var context = new PasteBookDBEntities())
            {
                list = context.POST_TABLE.Include("USER_TABLE").Where(x => x.PROFILE_ID == profileID).ToList();
            }

                return list;
        }




        }
    }

