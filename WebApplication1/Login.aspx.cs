using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid) 
            {
                Database db= new Database();
                if (db.ConnDB()) 
                {
                    string SqlVerifyAccount = "SELECT Id, UserAccount,UserPassword,PremissionId FROM UserInfor WHERE UserAccount=@UserAccountCheck AND UserPassword=@UserPassword";
                    string[] param = {"@UserAccountCheck" ,"@UserPassword"};
                    string[] control = { TextBoxAccount.Text, db.HashPassword(TextBoxPassword.Text) };
                    SqlDataReader reader = db.CheckUserDB(SqlVerifyAccount, param, control);  

                    if (reader.HasRows)
                    {
                        Session["LoginState"] = true;
                        Session["ReviseAriticle"] = "NotEditing";

                        while (reader.Read()) 
                        {

                            HttpCookie cookieUsername = new HttpCookie("username");   //創造cookie儲存使用者資料
                            cookieUsername.Value = reader["UserAccount"].ToString();
                            Response.Cookies.Add(cookieUsername);

                            HttpCookie cookieUserID = new HttpCookie("userID");   //創造cookie儲存使用者資料
                            cookieUserID.Value= reader.GetGuid(reader.GetOrdinal("Id")).ToString();
                            Response.Cookies.Add(cookieUserID);

                            Session["premission"] = reader["PremissionId"].ToString(); // 儲存權限
                        }

                        Application.Lock();                                        //設定Applicatoin在線總人數
                        Application["OnlineAccount"] = Convert.ToInt16(Application["OnlineAccount"]) + 1;
                        Application.UnLock();

                        Response.Redirect("~/Bulletinboard.aspx");
                    }
                    else { Response.Write("<script>alert('登入失敗，請確認帳號密碼')</script>"); }
                }

                
                
            
            }
        }
    }
}