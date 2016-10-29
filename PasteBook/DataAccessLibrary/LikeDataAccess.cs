using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEFLibrary;
using DataAccessLibrary;

namespace DataAccessLibrary
{
    public class LikeDataAccess
    {
        DataAccess<LIKES_TABLE> dataAccess = new DataAccess<LIKES_TABLE>();

        public bool LikeAPost(LIKES_TABLE like)
        {
            bool result = false;
            int user = like.LIKED_BY;
            using (var context = new PASTEBOOK_DBEntities())
            {
                var likeRecord = context.LIKES_TABLE.Where(x => x.LIKED_BY == user && x.POST_ID == like.POST_ID);

                if (likeRecord.Count() == 0)
                {
                    result = dataAccess.Create(like);
                }
            }

            return result;
        }

        public List<LIKES_TABLE> GetListOfLikeRecordOnPost(int postID)
        {
            
            using (var context = new PASTEBOOK_DBEntities())
            {
                var list = context.LIKES_TABLE.Where(x => x.POST_ID == postID).ToList();

                return list;
            }
        }
    }
}
