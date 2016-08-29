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
        public static string UserNameSessionSalt
        {
            get { return Convert.ToInt64("94357836487526", 16).ToString(); }
        }

        // Identifies the username section.
        public static string UserNameSessionIdentifier
        {
            get { return "loginId"; }
        }

        // salt used to generate password hash stored in DB. MUST ONLY permit password hash to be in session state - NO RAW PASSWORDS!
        public static string PassWordSessionSalt
        {
            get { return Convert.ToInt64("823437456342785", 16).ToString(); }
        }

        // Identifies the username section.
        public static string AuthenticationSessionIdentifier
        {
            get { return "isAuthenticated"; }
        }

        public static string GetPasswordHash(string password)
        {
            string saltedPassword = password + PassWordSessionSalt.ToString();
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
    }
}
