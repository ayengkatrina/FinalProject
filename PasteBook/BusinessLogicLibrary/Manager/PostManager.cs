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
        StoredProcDataAccess storedProcAccess = new StoredProcDataAccess();
        

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

            List<FRIENDS_TABLE> friendList = friendManager.GetListOfFriends(userID);

            using (var context = new PASTEBOOK_DBEntities())
            {
                foreach(var item in friendList)
                {
                  var list = context.POST_TABLE.Include("USER_TABLE").Include("LIKES_TABLE").Where(x => x.POSTER_ID == item.USER_ID || x.POSTER_ID == item.FRIEND_ID).OrderByDescending(x=>x.CREATED_DATE).Take(100);
                    foreach(var itemInList in list)
                    {
                        postList.Add(itemInList);
                    }
                }

                return postList;
            }
        }



        public List<POST_TABLE> TimelinePost(int profileID)
        {
            List<POST_TABLE> list = new List<POST_TABLE>();
            //list = dataAccess.GetOne(x => x.PROFILE_ID == profileID);

            using (var context = new PASTEBOOK_DBEntities())
            {
                list = context.POST_TABLE.Include("USER_TABLE").Include("LIKES_TABLE").Where(x => x.PROFILE_ID == profileID).OrderByDescending(x => x.CREATED_DATE).Take(100).ToList();
            }

                return list;
        }




        }
    }

