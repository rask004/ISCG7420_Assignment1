<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="ASP_Alt" %>
<%@ Import Namespace="BusinessLayer" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="CommonLogging" %>
<%@ Import Namespace="SecurityLayer" %>

<script runat="server">

    /// <summary>
    ///     Manage start of application
    ///     Setup logger.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        // attach a logger instance to the Global application.
        // keep the logger, and target logging stream, open until the application ends.
        var writer = new StreamWriter(
            Server.MapPath(GeneralConstants.LogFileDefaultLocation), true);
        writer.AutoFlush = true;
        var logger = new Logger(LoggingLevel.Debug, writer);
        logger.AppendDateTime = true;
        Application.Add(GeneralConstants.LoggerApplicationStateKey, logger);
    }

    /// <summary>
    ///     Manage errors, globally
    ///     Manage failure to connect to database
    ///     Otherwise use default error page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Application_Error(object sender, EventArgs e)
    {
        var ex = Server.GetLastError();

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
    ///     Manage session start
    ///     Set for not logged in, no security tokens, empty shopping cart, no previous errors.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Session_Start(object sender, EventArgs e)
    {
        Session["lastError"] = null;
        Session["pageOfLastError"] = null;
        Session[GeneralConstants.SessionCartItems] = new List<OrderItem>();
        Session[Security.SessionIdentifierSecurityToken] = null;
    }

    /// <summary>
    ///     End session or abandon session
    ///     clear any security or authentication tokens, log the customer out, send to default page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Session_End(object sender, EventArgs e)
    {
        Session["lastError"] = null;
        Session["pageOfLastError"] = null;
        Session[Security.SessionIdentifierSecurityToken] = null;
    }

</script>