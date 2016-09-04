using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using Common;

namespace SecurityLayer
{
    /// <summary>
    /// Constants and helpers for use by application, session state, and hashing.
    /// </summary>
    public static class Security
    {

        // salt is used with username to hide it inside session. (may not be used - may instead use human readable UserName)
        public static string LoginSalt
        {
            get { return Convert.ToInt64("94357836487526", 16).ToString(); }
        }

        // Identifies the username section.
        public static string SessionIdentifierLogin
        {
            get { return "activeLogin"; }
        }

        // salt used to generate password hash stored in DB. MUST ONLY permit password hash to be in session state - NO RAW PASSWORDS!
        public static string PasswordSalt
        {
            get { return Convert.ToInt64("823437456342785", 16).ToString(); }
        }

        // Identifies the username section.
        public static string SessionIdentifierSecurityToken
        {
            get { return "h9o3f5y45d23d5u35nyl"; }
        }

        public static string GetPasswordHash(string password)
        {
            string saltedPassword = password + PasswordSalt.ToString();
            byte[] buffer = Encoding.Default.GetBytes(saltedPassword);
            SHA1CryptoServiceProvider cryptoTransformSha1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }

        public static string GetRandomPassword()
        {
            Random random = new Random();
            return new string(Enumerable.Repeat(GeneralConstants.RandomPasswordChars, 16)
                .Select(s => s[random.Next(s.Length)]).ToArray());

        }

        public static bool IsValidLoginToken(string login, string passwordHash, string sessionTokenHash)
        {
            bool IsValid = false;

            // Use GenerateSecurityTokenHash to get expected hash
            string expectedTokenHash = GenerateSecurityTokenHash(login, passwordHash);
            // compare hashes. set IsValid true if matching.
            if (expectedTokenHash.Equals(sessionTokenHash))
            {
                IsValid = true;
            }

            return IsValid;
        }

        public static string GenerateSecurityTokenHash(string login, string passwordHash)
        {
            StringBuilder builder = new StringBuilder();

            // create MD5 hash of login + loginSalt
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.Default.GetBytes(login + LoginSalt));
            foreach (var b in data)
            {
                builder.Append(b.ToString("X2"));
            }
            
            // append passwordHash
            builder.Append(passwordHash);

            // get MD5 hash of this total hash
            data = md5Hash.ComputeHash(Encoding.Default.GetBytes(builder.ToString()));
            builder.Clear();
            foreach (var b in data)
            {
                builder.Append(b.ToString("X2"));
            }

            // cleanup
            md5Hash.Dispose();
            return builder.ToString();
        }
    }
}
