using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using PasteBookEFLibrary;

namespace BusinessLogicLibrary
{

    public class PostManager
    {
        DataAccess<POST_TABLE> dataAccess = new DataAccess<POST_TABLE>();
        PostDataAccess postDataAccess = new PostDataAccess();


        public bool CreatePost(POST_TABLE post)
        {
            post.CREATED_DATE = DateTime.Now;
            bool result = dataAccess.Create(post);

            return result;

        }

        public List<POST_TABLE> PostInTheNewsFeed(int userID)
        {
            FriendManager friendManager = new FriendManager();
            List<POST_TABLE> postList = new List<POST_TABLE>();
          
            List<int> friendsList = friendManager.FriendsIdList(userID);
            friendsList.Add(userID);
            postList = postDataAccess.NewsFeedPost(friendsList);

            return postList;
            
        }



        public List<POST_TABLE> TimelinePost(int profileID)
        {
            List<POST_TABLE> postList = postDataAccess.TimelinePost(profileID);

            return postList;
        }

        public int GetPosterID(int postID)
        {
            int posterID = postDataAccess.GetPosterID(postID);
            return posterID;
        }

        public POST_TABLE Post(int postID)
        {
            return postDataAccess.GetPost(postID);
        }




    }
}

