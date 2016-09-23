<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="ASP_Alt" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="BusinessLayer" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="CommonLogging" %>
<%@ Import Namespace="SecurityLayer" %>

<script runat="server">

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        // attach a logger instance to the Global application.
        // keep the logger, and target logging stream, open until the application ends.
        StreamWriter writer = new StreamWriter(
                Server.MapPath(GeneralConstants.LogFileDefaultLocation), true);
        writer.AutoFlush = true;
        Logger logger = new Logger(LoggingLevel.Debug, writer);
        logger.AppendDateTime = true;
        Application.Add(GeneralConstants.LoggerApplicationStateKey, logger);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();

        if (ex.InnerException != null && ex.InnerException.ToString().Contains("SQL Server") &&
            ex.InnerException.ToString().Contains("Server is not found"))
        {
            Response.Redirect("/Error/ErrorDatabaseConnection");
        }

        Session["lastError"] = ex.InnerException;

        // Issue with the default error page losing information upon postback - Url changes from last page accessed to the Error page, losing data.
        // This approach avoids the problem. 
        Session["pageOfLastError"] = Request.RawUrl;
        Response.Redirect("/Error/Default");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Session_Start(object sender, EventArgs e)
    {
        Session["lastError"] = null;
        Session["pageOfLastError"] = null;
        Session[GeneralConstants.SessionCartItems] = new List<OrderItem>();
        Session[Security.SessionIdentifierLogin] = null;
        Session[Security.SessionIdentifierSecurityToken] = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Session_End(object sender, EventArgs e)
    {
        Session[Security.SessionIdentifierLogin] = null;
        Session["lastError"] = null;
        Session["pageOfLastError"] = null;
        Session[Security.SessionIdentifierSecurityToken] = null;
        if (Session[GeneralConstants.SessionCartItems] != null)
        {
            (Session[GeneralConstants.SessionCartItems] as List<OrderItem>).Clear();
        }
    }

</script>
