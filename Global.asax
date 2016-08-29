<%@ Application Language="C#" %>
<%@ Import Namespace="ASP_Alt" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="SecurityLayer" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    void Session_Start(object sender, EventArgs e) 
    {
        Session[Security.UserNameSessionIdentifier] = null;
        Session[Security.AuthenticationSessionIdentifier] = null;
    }

    void Session_End(object sender, EventArgs e) 
    {
        Session[Security.UserNameSessionIdentifier] = null;
        Session[Security.AuthenticationSessionIdentifier] = null;
    }

</script>
