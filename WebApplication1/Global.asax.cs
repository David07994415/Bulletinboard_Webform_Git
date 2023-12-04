using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Windows.Forms;

namespace WebApplication1
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 應用程式啟動時執行的程式碼
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["OnlineAccount"] = 0;
        }
        void Session_Start(object sender, EventArgs e) 
        {
            Session["LoginState"] = false;
            Session["PhotoEdit"] ="-1";
            Session["AlbumName"] = null;
            Session["AlbumId"] = null;
        }
        void Session_End(object sender, EventArgs e)
        {

        }
    }
}