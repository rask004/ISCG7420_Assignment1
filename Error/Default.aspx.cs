using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        Exception ex = Server.GetLastError();

        if (ex != null)
        {
            UnknownErrorSection.Visible = false;

            lblErrorName.InnerText = ex.GetType().ToString();
            lblErrorDescription.InnerText = ex.Message;
            lblErrorHResult.InnerText = ex.HResult.ToString();
            lblErrorSourceMethod.InnerText = ex.Source;
            if (ex.InnerException != null)
            {
                lblInnerException.InnerText = ex.InnerException.ToString() + ";  " + ex.InnerException.Message;
            }
            else
            {
                lblInnerException.InnerText = "NULL";
            }
        }
        else
        {
            KnownErrorSection.Visible = false;
        }


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEmailAdmin_OnClick(object sender, EventArgs e)
    {
        
    }
}