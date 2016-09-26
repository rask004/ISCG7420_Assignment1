using System;
using System.Net.Mail;
using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace Common
{
    /// <summary>
    ///     Common constants
    /// 
    ///     Changelog:
    ///     22-08-16        19:01   AskewR04    created static class
    /// </summary>
    public static class GeneralConstants
    {
        public static readonly string ReleaseMachineName = "DOCHYPER";

        public static readonly string CustomerUserType = "C";
        public static readonly string AdminUserType = "A";

        public static readonly string ButtonTextChangePassword = "Change Password";
        public static readonly string ButtonTextSavePassword = "Save Password";
        public static readonly string ButtonTextCancelChangePassword = "Cancel Change";

        public static readonly string CapPriceNew = "00.00";

        public static readonly string CapImageDefaultFileName = "~/Images/Cap_NoImage.png";

        public static readonly string CapImageDefaultListName = "Default";

        public static readonly string ImagesUploadFolder = "~/UploadFiles";

        public static readonly string[] PermittedContentTypes = new string[] {"image/jpeg", "image/jpg", "image/png"};

        public static readonly string[] PermittedOrderStatuses = new string[] {"Waiting", "Shipped"};

        public static readonly Double MoneyGstRate = 0.15;

        public static readonly string EmailErrorSubject = "Error, Quality Caps Website.";

        public static readonly string EmailErrorBody = "Error Report:\n\n{0}\n\nSource Page:\n{1}.";

        public static readonly string EmailPasswordChangeSubject = "Quality Caps, change in customer details";

        public static readonly string EmailRegisteredCustomerSubject = "Quality Caps, new customer details";

        public static readonly string EmailPasswordChangeBody =
            "Dear {0} {1},\n\nYour login details have changed to:\n\nLogin:\t\t{2}\nPassword:\t{3}\n\n\nRegards,\n\nThe Quality Caps Team\n";

        public static readonly string EmailRegisteredCustomerBody =
            "Dear {0} {1},\n\nYou are now registered as a customer for the Quality Caps Website.\n\nYour Details:\n\nLogin:\t\t{2}\nPassword:\t{3}\n\nHome Number:   {4}\nWork Number:   {5}\nMobile Number: {6}\n\nAddress: {7}\n         {8}, {9}\n\nYou may change these details by logging in and navigating to your profile page.\n\nIf you did not register on our side, reply to this email ASAP to resolve the matter.\n\n\nRegards,\n\nThe Quality Caps Team\n";

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

        public static readonly string QueryStringGeneralMessageSuccessfulProfileUpdate = "456o8wbn7564";

        public static readonly string QueryStringGeneralMessageSuccessfulPlacedNewOrder = "346w98w4wva00d";

        public static readonly string SessionCartItems = "cartItems";

        /// <summary>
        ///     Determines the connection string to use depending on which machine is running the application.
        /// </summary>
        public static string DefaultConnectionString
        {
            get
            {
                
                if (HttpContext.Current.Server.MachineName.Equals(ReleaseMachineName))
                {
                    return ConfigurationManager.ConnectionStrings["ReleaseConnection"].ConnectionString;
                }
                else
                {
                    return ConfigurationManager.ConnectionStrings["DeveloperExpressConnection"].ConnectionString;
                }
                
            }
        }
    }

    /// <summary>
    ///     Support functions shared across the application.
    /// 
    ///     Changelog:
    ///     24-08-16        19:01   AskewR04    created static class
    /// </summary>
    public static class GeneralFunctions
    {
        /// <summary>
        ///     manage sending an email.
        /// </summary>
        /// <param name="destinationEmail">To Address</param>
        /// <param name="subject">Subject line</param>
        /// <param name="messageBody">Body paragraph</param>
        /// <param name="replyToEmail">From and ReplyTo Address.</param>
        public static void SendEmail(string destinationEmail, string subject, string messageBody, string replyToEmail)
        {
            MailMessage message = new MailMessage();
            try
            {
                message.To.Add(destinationEmail);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is ArgumentException)
                {
                    throw new SmtpException("No destination email provided.");
                }    
                else if (ex is FormatException)
                {
                    throw new SmtpException("Destination email is not in correct email format.");
                }
            }

            if (replyToEmail.Equals(String.Empty))
            {
                throw new SmtpException("ReplyTo email is Required.");
            }
            message.From = new MailAddress(replyToEmail);
                
            
            message.Subject = subject;
            message.Body = messageBody;

            SmtpClient client = new SmtpClient();

            try
            {
                client.Host = "mail.unitec.ac.nz";
                client.Send(message);
            }
            catch (Exception ex)
            {
                if (ex is SmtpException)
                {
                    SmtpException smtpEx = ex as SmtpException;
                    throw smtpEx;
                }

                throw ex;
            }
        }
    }
}