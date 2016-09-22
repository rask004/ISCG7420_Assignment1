using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

//TODO: complete this section addressing generic errors. An email can be sent by User to Admin.

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
            Exception ex = Server.GetLastError();
            if (ex == null)
            {
                KnownErrorSection.Visible = false;
                return;
            }

            UnknownErrorSection.Visible = false;

            lblErrorName.InnerText = ex.GetType().ToString();
            lblErrorHResult.InnerText = ex.HResult.ToString();
            lblErrorSourceUrl.InnerText = "~" + Request.RawUrl;
            if (ex.InnerException != null)
            {
                lblInnerExceptionName.InnerText = ex.InnerException.GetType().ToString();
                lblInnerExceptionHeader.Visible = true;
                hddnInnerHResult.InnerText = ex.InnerException.HResult.ToString();
                hddnInnerMessage.InnerText = ex.InnerException.Message;
                hddnInnerStackTrace.InnerText = ex.InnerException.StackTrace;
                hddnInnerName.InnerText = ex.InnerException.GetType().ToString();
            }
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
            StringBuilder builder = new StringBuilder();
            //builder.Append("SENDER: " + inputReportUserName.Value + "\n\n");
            //builder.Append("ERROR:\n" + lblReportData.InnerText + "\n\n");
            string EmailMessage = String.Format(
                GeneralConstants.EmailErrorBody, builder.ToString(), "");
            try
            {
                GeneralFunctions.SendEmail(GeneralConstants.AdminReplyToEmailDefault,
                    GeneralConstants.EmailErrorSubject,
                    EmailMessage, "");
                    //inputReportUserEmail.Value);

                litEmailResponse.Text = "The Eamil was sent successfully.";
            }
            catch (SmtpException)
            {
                litEmailResponse.Text = "Could not send the email. You can manually email the Administrator at " +
                                        GeneralConstants.AdminReplyToEmailDefault + ".";
            }


        }
    }
}