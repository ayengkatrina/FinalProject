using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLibrary;
using System.Data.SqlClient;
using PasteBook_EntityFramework;

namespace BusinessLogicLibrary
{
    public class DataAccess
    {
        
        public int CreateAccount(User user)
        {
            using (var content = new PasteBookDBEntities())
            {
                content.USER_TABLE.Add(Mapper.ToUSER_TABLE(user));
                
               int numberCreated = content.SaveChanges();
                return numberCreated;
                
            }
        }
       
    }
}
