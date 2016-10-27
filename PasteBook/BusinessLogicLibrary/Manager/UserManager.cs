using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using PasteBookEntityLibrary;

namespace BusinessLogicLibrary
{
   public class UserManager
    {
        //UserTableAccess userTableAccess = new UserTableAccess();
        DataAccess<USER_TABLE> dataAccess = new DataAccess<USER_TABLE>();
        UserDataAccess userDataAccess = new UserDataAccess();
        LikeDataAccess likeDataAccess = new LikeDataAccess();

        //public USER_TABLE GetUserAccount(string email)
        //{
        //   List<USER_TABLE> user = ;
        //    return user;
        //}
        public List<USER_TABLE> ListOfUserWhoLikeAPost(int postID)
        {
            List<USER_TABLE> usersWhoLikeList = new List<USER_TABLE>();
            List<LIKES_TABLE> listLikeTable = likeDataAccess.GetListOfLikeRecordOnPost(postID);

            using (var context = new PasteBookDBEntities())
            {
                
                List<int> userIDList = listLikeTable.Select(x => x.ID).ToList();
                foreach(var item in userIDList)
                {
                    usersWhoLikeList.Add(context.USER_TABLE.Where(x => x.ID == item).SingleOrDefault());
                }
                return usersWhoLikeList;
            }

        }

        public List<USER_TABLE> Search(string inputString)
        {
            var userList = userDataAccess.Search(inputString);
            return userList;
        }

            public bool EditProfile(USER_TABLE user, int userID)
        {
            var result = userDataAccess.EditProfile(user, userID);
            return result;
        }

        public bool EditCredential(int userID, string email, string hash, string salt)
        {
            var result = userDataAccess.EditCredential(userID, email, hash, salt);
            return result;
        }
        public bool EditEmailAddress(int userID, string email)
        {
            var result = userDataAccess.EditEmailAddress(userID, email);
            return result;
        }
        public bool EditPassword(int userID, string hash, string salt)
        {
            var result = userDataAccess.EditPassword(userID, hash, salt);
            return result;
        }

        public USER_TABLE GetUserDetails(string email)
        {
            var list = dataAccess.GetOne(x=>x.EMAIL_ADDRESS == email).SingleOrDefault();

            return list;
        }

        public USER_TABLE GetUserDetailsByID(int userID)
        {
            var list = dataAccess.GetOne(x => x.ID == userID).SingleOrDefault();

            return list;
        }
        
        public bool AddProfilePicture(byte[] profilePic, int userID)
        {
            var result = userDataAccess.AddPicture(profilePic, userID);
            return result;
        }

        public bool AddAboutMe(string aboutMe, int userID)
        {
            var result = userDataAccess.AddAboutMe(aboutMe, userID);
            return result;
        }

        public bool CheckIfEmailAlreadyExist(string email)
        {
            bool result;
            
            var list = dataAccess.GetOne(x => x.EMAIL_ADDRESS == email);

            if (list.Count() > 0)
            {
                result = true;
            }else
            {
                result = false;
            }

            return result;
        }

        public bool CheckIfUserNameAlreadyExist(string userName)
        {
            bool result;
            var list = dataAccess.GetOne(x => x.USER_NAME == userName);
            if(list.Count() > 0)
            {
                result = true;
            }else
            {
                result = false;
            }
            return result;
        }


        //www.codeproject.com/Articles/608860/Understanding-and-Implementing-Password-Hashing

        public bool SimulateUserCreation(USER_TABLE user )
        {
            bool result;
            string salt = null;

            user.PASSWORD = GeneratePasswordHash(user.PASSWORD, out salt);
            user.SALT = salt;

            
            user.DATE_CREATED = DateTime.Now;
            result = dataAccess.Create(user);
            return result;
        }

        public bool SimulateLogin(string email, string password)
        {
            bool result;
           

            USER_TABLE user2 = dataAccess.GetAll().Where(x=> x.EMAIL_ADDRESS == email).FirstOrDefault();
            if(user2 != null)
            {
                 result = IsPasswordMatch(password, user2.SALT, user2.PASSWORD);
            }
            else
            {
                result = false;
            }
           

            return result;
        }

        //HashComputer

        public string GetPasswordHashAndSalt(string message)
        {
            // Let us use SHA256 algorithm to 
            // generate the hash from this salted password
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);

            // return the hash string to the caller
            return GetString(resultBytes);
        }



        public string GeneratePasswordHash(string plainTextPassword, out string salt)
        {
            salt = GetSaltString();

            string finalString = plainTextPassword + salt;

            return GetPasswordHashAndSalt(finalString);
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            string finalString = password + salt;
            return hash == GetPasswordHashAndSalt(finalString);
        }

       
            private static RNGCryptoServiceProvider m_cryptoServiceProvider = new RNGCryptoServiceProvider();
            private const int SALT_SIZE = 24;

                //static m_cryptoServiceProvider = new RNGCryptoServiceProvider();



        public static string GetSaltString()
            {
                // Lets create a byte array to store the salt bytes
                byte[] saltBytes = new byte[SALT_SIZE];

                // lets generate the salt in the byte array
                m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);

                // Let us get some string representation for this salt
                string saltString = GetString(saltBytes);

                // Now we have our salt string ready lets return it to the caller
                return saltString;
            }

            public static string GetString(byte[] resultBytes)
            {

            return Encoding.ASCII.GetString(resultBytes);

            //string s = System.Text.Encoding.UTF8.GetString(input, 0, input.Length);
            //return s;
        }

            public static byte[] GetBytes(string message)
            {
            return Encoding.ASCII.GetBytes(message);

            //byte[] bytes = new byte[input.Length * sizeof(char)];
            //System.Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);
            //return bytes;
        }
        
    }
}
