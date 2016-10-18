using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLibrary;

namespace PasteBook
{
    public class Manager
    {
        DataAccess dataAccess = new DataAccess();

        public List<PostModel> GetAllPost()
        {
            List<PostModel> postList = new List<PostModel>();

           var list = dataAccess.GetPost();

            foreach(var item in list)
            {
                postList.Add(Mapper.ToPostModel( item));
            }
            return postList;
            
        }
    }
}