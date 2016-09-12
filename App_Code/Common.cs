using System;
using System.Net.Mail;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class GeneralConstants
    {
        public static readonly string CustomerUserType = "C";
        public static readonly string AdminUserType = "A";

        public static readonly string ButtonTextChangePassword = "Change Password";
        public static readonly string ButtonTextSavePassword = "Save Password";
        public static readonly string ButtonTextCancelChangePassword = "Cancel Change";

        public static readonly string CapPriceNew = "00.00";

        public static readonly string CapImageDefaultFileName = "~/Images/Cap_NoImage.png";

        public static readonly string CapImageDefaultListName = "Default";

        public static readonly string ImagesUploadFolder = "~/UploadFiles";

        public static readonly string[] PermittedContentTypes = new string[] {"image/jpeg", "image/png"};

        public static readonly string[] PermittedOrderStatuses = new string[] {"Waiting", "Shipped"};

        public static readonly Double MoneyGstRate = 0.15;

        public static readonly string UserNewPasswordEmailSubject = "New Password Notice";

        public static readonly string AdminReplyToEmailDefault = "AskewR04@myunitec.ac.nz";

        public static readonly int SidebarItemTextMaxLength = 19;

        public static readonly int EmailMaxLength = 100;

        public static readonly int LoginMaxLength = 64;

        public static readonly int PasswordMaxLength = 64;

        public static readonly int HomeNumberMaxLength = 11;

        public static readonly int WorkNumberMaxLength = 11;

        public static readonly int MobileNumberMaxLength = 13;

        public static readonly int StreetAddressMaxLength = 64;

        public static readonly int SuburbMaxLength = 24;

        public static readonly int CityMaxLength = 16;

        public static readonly int FirstNameMaxLength = 32;

        public static readonly int LastNameMaxLength = 32;

        public static readonly int CategoryNameMaxLength = 40;

        public static readonly int ColourNameMaxLength = 24;

        public static readonly int SupplierNameMaxLength = 32;

        public static readonly int SupplierEmailMaxLength = 64;

        public static readonly int CapNameMaxLength = 40;

        public static readonly int CapDescriptionMaxLength = 512;

        public static readonly string RandomPasswordChars = "abcdefghijklmnoABCDEFGHIJKLMNO123456789";

        public static readonly string Numerals = "0123456789";

        public static readonly string LogFileDefaultLocation = "~/Logs/Site.Log";

        public static readonly string LoggerApplicationStateKey = "GlobalLogger";

        public static readonly string QueryStringGeneralMessageKey = "message";

        public static readonly string QueryStringGeneralMessageSuccessfulRegistration = "e5vt8n57";

        public static readonly string SessionCartItems = "cartItems";

        public static readonly string SessionCustomerIdentifier = "SessionCustomerId";
    }

    /// <summary>
    /// 
    /// </summary>
    public static class GeneralFunctions
    {
        public static void SendEmail(string destinationEmail, string subject, string messageBody, string replyToEmail)
        {
            var toAddress = new MailAddress(destinationEmail);
            var replyAddress = new MailAddress(replyToEmail);
            MailMessage message = new MailMessage(replyAddress, toAddress);
            message.Subject = subject;
            message.Body = messageBody;

            SmtpClient client = new SmtpClient();

            try
            {
                client.Host = "localhost";
                client.Send(message);
            }
            catch (SmtpException smtpEx)
            {
                throw smtpEx;
            }
        }
    }
}