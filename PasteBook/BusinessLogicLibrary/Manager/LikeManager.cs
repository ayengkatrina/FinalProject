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

        public bool LikeAPost(LIKES_TABLE like)
        {
            
           bool result = dataAccess.Create(like);
            return result;
        }


    }
}
