using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLibrary;
using System.Data.SqlClient;
using PasteBookEntityLibrary;
using System.Data.Entity.Validation;

namespace BusinessLogicLibrary
{
    public class DataAccess
    {
        PasswordManager passwordManager = new PasswordManager();



        public int SimulateUserCreation(User user)
        {
            int result;
            string salt = null;

            user.Password = passwordManager.GeneratePasswordHash(user.Password, out salt);
            user.Salt = salt;

            result = CreateAccount(user);
            return result;


        }

        public bool SimulateLogin(string email, string password)
        {

            User user2 = GetUserAccount(email);

            bool result = passwordManager.IsPasswordMatch(password, user2.Salt, user2.Password);

            return result;
        }


        public int CreateAccount(User user)
        {
            //using (var content = new PasteBookDBEntities())
            //{
            //    content.USER_TABLE.Add(Mapper.ToUSER_TABLE(user));

            //   int numberCreated = content.SaveChanges();
            //    return numberCreated;

            //}
            try
            {

                using (var content = new PasteBookDBEntities())
                {
                    user.Birthday = user.Birthday.Date;
                    user.DateCreated = DateTime.Now.Date;
                    content.USER_TABLE.Add(Mapper.ToUSER_TABLE(user));

                    int numberCreated = content.SaveChanges();
                    return numberCreated;
                }
            }

            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
 .SelectMany(x => x.ValidationErrors)
 .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public User GetUserAccount(string email)
        {
            User user = new User();
            try
            {
                using (var content = new PasteBookDBEntities())
                {
                    user = Mapper.ToUser(content.USER_TABLE.Where(x => x.EMAIL_ADDRESS == email).SingleOrDefault());

                }
               
            }

            catch (DbEntityValidationException ex)
            {
            }
            return user;
        }
    }
}
