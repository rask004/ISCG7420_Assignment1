using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using SecurityLayer;

/// <summary>
/// 
/// </summary>
public partial class Error_Default : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Exception ex = Session["lastError"] as Exception;
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
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSendEmail_OnClick(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Exception ex = Session["lastError"] as Exception;
            string Message = "Name: " + lblErrorName.InnerText + "\nHResult: " + lblErrorHResult.InnerText + "\n\n";
            Message += "StackTrace: " + ex.StackTrace + "\n\n\n";
            if (ex.InnerException != null)
            {
                Message += "InnerException: " + ex.InnerException.GetType().ToString() + "\n";
                Message += "HResult: " + ex.InnerException.HResult + "\n";
                Message += "StackTrace: " + ex.InnerException.StackTrace + "\n\n";
            }

            if (txtSenderName.Text != String.Empty)
            {
                Message += "Sent By: " + txtSenderName.Text;
            }

            string EmailMessage = string.Format(GeneralConstants.EmailErrorBody, Message, Session["pageOfLastError"]);

            try
            {
                GeneralFunctions.SendEmail(GeneralConstants.AdminReplyToEmailDefault,
                    GeneralConstants.EmailErrorSubject, EmailMessage,
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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void OnServerValidate(object source, ServerValidateEventArgs args)
    {
        Validation.ValidateEmailInput(ref args);

        if (!args.IsValid)
        {
            litEmailResponse.Text = "ERROR: email is not valid. Please Correct.";
        }
    }
}