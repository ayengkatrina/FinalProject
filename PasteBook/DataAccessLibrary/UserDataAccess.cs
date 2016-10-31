using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEFLibrary;

namespace DataAccessLibrary
{
   public class UserDataAccess
    {

        public List<USER_TABLE> Search(string userFullName)
        {
            List<USER_TABLE> userList = new List<USER_TABLE>();
            List<USER_TABLE> firstNameList = new List<USER_TABLE>();
            List<USER_TABLE> lastNameList = new List<USER_TABLE>();
            using (var context = new PASTEBOOK_DBEntities1())
            {
                firstNameList = context.USER_TABLE.Where(x => x.FIRST_NAME.ToLower() == userFullName).ToList();
                lastNameList = context.USER_TABLE.Where(x => x.LAST_NAME.ToLower() == userFullName).ToList();

                foreach(var item in firstNameList)
                {
                    userList.Add(item);
                }
                foreach(var item in lastNameList)
                {
                    userList.Add(item);
                }
                return userList;
            }
        }
        public bool AddPicture(byte[] profilePic, int userID)
        {

            int numberSave = 0;
            using (var context = new PASTEBOOK_DBEntities1())
            {
                USER_TABLE user = context.USER_TABLE.Where(x => x.ID == userID).SingleOrDefault();
                if (user != null)
                {
                    user.PROFILE_PIC = profilePic;
                    numberSave = context.SaveChanges();

                }

            }
            if (numberSave > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddAboutMe(string aboutMe, int userID)
        {

            int numberSave = 0;
            using (var context = new PASTEBOOK_DBEntities1())
            {
                USER_TABLE user = context.USER_TABLE.Where(x => x.ID == userID).SingleOrDefault();
                if (user != null)
                {
                    user.ABOUT_ME = aboutMe;
                    numberSave = context.SaveChanges();

                }

            }
            if (numberSave > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditProfile(USER_TABLE userModel, int userID)
        {

            int numberSave = 0;
            using (var context = new PASTEBOOK_DBEntities1())
            {
                USER_TABLE user = context.USER_TABLE.Where(x => x.ID == userID).SingleOrDefault();
                if (user != null)
                {
                    user.USER_NAME = userModel.USER_NAME;
                    user.BIRTHDAY = userModel.BIRTHDAY;
                    user.COUNTRY_ID = userModel.COUNTRY_ID;
                    user.FIRST_NAME = userModel.FIRST_NAME;
                    user.LAST_NAME = userModel.LAST_NAME;
                    user.MOBILE_NO = user.MOBILE_NO;
                    user.GENDER = user.GENDER;

                    
                    numberSave = context.SaveChanges();

                }

            }
            if (numberSave > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditCredential(int userID, string email, string hash, string salt)
        {

            int numberSave = 0;
            using (var context = new PASTEBOOK_DBEntities1())
            {
                USER_TABLE user = context.USER_TABLE.Where(x => x.ID == userID).SingleOrDefault();
                if (user != null)
                {
                    user.EMAIL_ADDRESS = email;
                    user.PASSWORD = hash;
                    user.SALT = salt;                   

                    numberSave = context.SaveChanges();

                }

            }
            if (numberSave > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditEmailAddress(int userID, string email)
        {

            int numberSave = 0;
            using (var context = new PASTEBOOK_DBEntities1())
            {
                USER_TABLE user = context.USER_TABLE.Where(x => x.ID == userID).SingleOrDefault();
                if (user != null)
                {
                    user.EMAIL_ADDRESS = email;                   

                    numberSave = context.SaveChanges();

                }

            }
            if (numberSave > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EditPassword(int userID, string hash, string salt)
        {

            int numberSave = 0;
            using (var context = new PASTEBOOK_DBEntities1())
            {
                USER_TABLE user = context.USER_TABLE.Where(x => x.ID == userID).SingleOrDefault();
                if (user != null)
                {
                    
                    user.PASSWORD = hash;
                    user.SALT = salt;

                    numberSave = context.SaveChanges();

                }

            }
            if (numberSave > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
