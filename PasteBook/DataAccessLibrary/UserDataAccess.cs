using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEntityLibrary;

namespace DataAccessLibrary
{
   public class UserDataAccess
    {
        public bool AddPicture(byte[] profilePic, string email)
        {

            int numberSave = 0;
            using (var context = new PasteBookDBEntities())
            {
                USER_TABLE user = context.USER_TABLE.Where(x => x.EMAIL_ADDRESS == email).SingleOrDefault();
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

        public bool AddAboutMe(string aboutMe, string email)
        {

            int numberSave = 0;
            using (var context = new PasteBookDBEntities())
            {
                USER_TABLE user = context.USER_TABLE.Where(x => x.EMAIL_ADDRESS == email).SingleOrDefault();
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

        public bool EditProfile(USER_TABLE userModel, string email)
        {

            int numberSave = 0;
            using (var context = new PasteBookDBEntities())
            {
                USER_TABLE user = context.USER_TABLE.Where(x => x.EMAIL_ADDRESS == email).SingleOrDefault();
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
            using (var context = new PasteBookDBEntities())
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



    }
}
