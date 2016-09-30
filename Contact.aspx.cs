using System;
using System.Web.UI;
using Common;
using CommonLogging;

public partial class Contact : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        (Application[GeneralConstants.LoggerApplicationStateKey] as Logger).Log(LoggingLevel.Info,
            "Loaded Page " + Page.Title + ", " + Request.RawUrl);
    }
}