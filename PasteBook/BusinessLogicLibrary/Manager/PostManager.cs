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

        public List<FRIEND_POST_USER_JOIN3_Result> PostInTheNewsFeed(int profileID)
        {
            List<FRIEND_POST_USER_JOIN3_Result> list = new List<FRIEND_POST_USER_JOIN3_Result>();

            list = storedProcAccess.PostInTheNewsFeed(profileID);
            return list;
        }

     public List<NEWSFEEDPOST_Result> NewsFeedPost(int profileID)
        {
            List<NEWSFEEDPOST_Result> list = new List<NEWSFEEDPOST_Result>();
            list = storedProcAccess.NewsFeedPost(profileID);
            return list;
        }


        }
    }

