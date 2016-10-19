using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLibrary
{
   public class Manager
    {
        DataAccess dataAccess = new DataAccess();

        public bool SimulateUserCreation(User user)
        {
            bool result;
            string salt = null;

            user.Password = GeneratePasswordHash(user.Password, out salt);
            user.Salt = salt;

            result = dataAccess.CreateAccount(user);
            return result;
        }

        public bool SimulateLogin(string email, string password)
        {

            User user2 = dataAccess.GetUserAccount(email);

            bool result = IsPasswordMatch(password, user2.Salt, user2.Password);

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
