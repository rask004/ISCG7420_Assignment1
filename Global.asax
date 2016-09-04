<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="ASP_Alt" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="CommonLogging" %>
<%@ Import Namespace="SecurityLayer" %>

<script runat="server">

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

    void Session_Start(object sender, EventArgs e) 
    {
        Session[Security.SessionIdentifierLogin] = null;
        Session[Security.SessionIdentifierSecurityToken] = null;
    }

    void Session_End(object sender, EventArgs e) 
    {
        Session[Security.SessionIdentifierLogin] = null;
        Session[Security.SessionIdentifierSecurityToken] = null;
    }

</script>
