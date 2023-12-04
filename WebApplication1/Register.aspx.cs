using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ExplorerBar;

namespace WebApplication1
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnRegis_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                Database db = new Database();
                db.ConnDB();
                string sqlCheckAccount = "SELECT UserAccount FROM UserInfor WHERE UserAccount=@UserAccountCheck";
                string[] pararscheck = { "@UserAccountCheck" };
                string[] controlscheck = { TextBoxAccount.Text };
                SqlDataReader reader = db.CheckUserDB(sqlCheckAccount, pararscheck, controlscheck);
                if (!reader.HasRows)
                {
                    db.readerclose();
                    string sqlRevise = "INSERT INTO UserInfor (UserName,UserAccount,UserPassword) VALUES (@UserName,@UserAccount,@UserPassword)";
                    string[] parars = { "@UserName", "@UserAccount", "@UserPassword" };
                    string[] controls = { TextBoxUser.Text, TextBoxAccount.Text, db.HashPassword(TextBoxPassword.Text)};
                    int ReviseNum = db.ReviseDB(sqlRevise, parars, controls);
                    db.CloseDB();
                    if (ReviseNum == 1)
                    {
                        Response.Write("<script>alert('註冊成功')</script>");
                        Response.Redirect("~/Login.aspx");
                    }
                }
                else
                {
                    Response.Write("<script>alert('該帳號已被註冊')</script>");
                }
            }
            TextBoxUser.Text = string.Empty;
            TextBoxAccount.Text = string.Empty;
        }
    }
}