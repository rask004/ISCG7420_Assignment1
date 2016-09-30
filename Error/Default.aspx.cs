using System;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using SecurityLayer;

public partial class Error_Default : Page
{
    /// <summary>
    ///     Load the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var ex = Session["lastError"] as Exception;
            if (ex == null)
            {
                KnownErrorSection.Visible = false;
                return;
            }

            UnknownErrorSection.Visible = false;

            lblErrorName.InnerText = ex.GetType().ToString();
            lblErrorHResult.InnerText = ex.HResult.ToString();
            lblErrorMessage.InnerText = ex.Message;
        }
    }

    /// <summary>
    ///     click handler, send email
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSendEmail_OnClick(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var ex = Session["lastError"] as Exception;
            var message = "Name: " + lblErrorName.InnerText + "\nHResult: " + lblErrorHResult.InnerText + "\n\n";
            message += "StackTrace: " + ex.StackTrace + "\n\n\n";
            if (ex.InnerException != null)
            {
                message += "InnerException: " + ex.InnerException.GetType() + "\n";
                message += "HResult: " + ex.InnerException.HResult + "\n";
                message += "StackTrace: " + ex.InnerException.StackTrace + "\n\n";
            }

            if (txtSenderName.Text != string.Empty)
            {
                message += "Sent By: " + txtSenderName.Text;
            }

            var emailMessage = string.Format(GeneralConstants.EmailErrorBody, message, Session["pageOfLastError"]);

            try
            {
                GeneralFunctions.SendEmail(GeneralConstants.AdminReplyToEmailDefault,
                    GeneralConstants.EmailErrorSubject, emailMessage,
                    txtSenderEmail.Text);
                litEmailResponse.Text = "The Email Has Been Sent.";
            }
            catch (SmtpException)
            {
                litEmailResponse.Text =
                    "There was an error sending the email. You can manually email the Administrator at " +
                    GeneralConstants.AdminReplyToEmailDefault + ".";
            }
        }
    }

    /// <summary>
    ///     Validation handler, validate the email.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void ValidateEmailInput(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateEmailInput(ref args);

        if (!args.IsValid)
        {
            litEmailResponse.Text = "ERROR: email is not valid. Please Correct.";
        }
    }
}