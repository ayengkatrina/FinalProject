using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using PasteBookEntityLibrary;

namespace BusinessLogicLibrary
{
   public class LikeManager
    {
        DataAccess<LIKES_TABLE> dataAccess = new DataAccess<LIKES_TABLE>();
        LikeDataAccess likeDataccess = new LikeDataAccess();

        public bool LikeAPost(LIKES_TABLE like)
        {
            var resultOfLikePost = likeDataccess.LikeAPost(like);
            return resultOfLikePost;
        }


    }
}
