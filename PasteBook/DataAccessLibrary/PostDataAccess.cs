using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEFLibrary;

namespace DataAccessLibrary
{
    public class PostDataAccess
    {

        public List<POST_TABLE> TimelinePost(int profileID)
        {
            List<POST_TABLE> list = new List<POST_TABLE>();

            using (var context = new PASTEBOOK_DBEntities1())
            {
                list = context.POST_TABLE.Include("USER_TABLE").Include("LIKES_TABLE").Where(x => x.PROFILE_ID == profileID).OrderByDescending(x => x.CREATED_DATE).Take(100).ToList();
            }

            return list;
        }

        public List<POST_TABLE> NewsFeedPost( List<int> friendsIDList)
        {
            List<POST_TABLE> postList = new List<POST_TABLE>();
            List<POST_TABLE> newsFeedList = new List<POST_TABLE>();
            
            using (var context = new PASTEBOOK_DBEntities1())
            {
                foreach (var item in friendsIDList)
                {

                    postList = context.POST_TABLE.Include("USER_TABLE").Include("LIKES_TABLE").Include("COMMENTS_TABLE").Where(x => x.POSTER_ID == item).OrderByDescending(x => x.CREATED_DATE).Take(100).ToList();

                    foreach(var postItems in postList)
                    {
                        newsFeedList.Add(postItems);
                    }
                }

                return newsFeedList;
            }
        }

        public int GetPosterID(int postID)
        {
            int posterID = 0;
            using (var context = new PASTEBOOK_DBEntities1())
            {
                var id = context.POST_TABLE.Where(x => x.ID == postID).Select(x => x.POSTER_ID).SingleOrDefault();
                return posterID = (int)id;
            }
        }

        public POST_TABLE GetPost(int postID)
        {
            POST_TABLE post = new POST_TABLE();
            using (var context = new PASTEBOOK_DBEntities1())
            {
                post = context.POST_TABLE.Include("USER_TABLE").Include("LIKES_TABLE").Where(x => x.ID == postID).SingleOrDefault();
                return post;
            }
        }
    }
}
