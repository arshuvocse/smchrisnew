<%@ Application Language="C#" %>
<%@ Import Namespace="MainSite" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex != null)
        {
            try
            {
                string logPath = Server.MapPath("~/App_Data/error_log.txt");
                string eventTarget = Request.Form["__EVENTTARGET"];
                string eventArgument = Request.Form["__EVENTARGUMENT"];
                string empInfoId = Session != null && Session["EmpInfoId"] != null ? Session["EmpInfoId"].ToString() : "";
                string logEntry = DateTime.Now + "\r\nURL: " + Request.Url
                    + "\r\n__EVENTTARGET: " + eventTarget
                    + "\r\n__EVENTARGUMENT: " + eventArgument
                    + "\r\nEmpInfoId: " + empInfoId
                    + "\r\n" + ex.ToString() + "\r\n\r\n";
                System.IO.File.AppendAllText(logPath, logEntry);
            }
            catch { }
        }
    }

</script>
