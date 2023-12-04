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
            Application["OnlineAccount"] = 0;
        }
        void Session_Start(object sender, EventArgs e) 
        {
            Session["LoginState"] = false;
        }
        void Session_End(object sender, EventArgs e)
        {

        }
    }
}