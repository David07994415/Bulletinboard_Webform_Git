using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class MyPostPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["LoginState"]) == false)
                {
                    //Server.Transfer("~/Login.aspx");  //網址URL會有問題
                    Response.Redirect("~/Login.aspx");
                }
                else 
                {
                    ShowDB();
                }
            }

        }

        protected void ShowDB() 
        {
            string CookieUSERID=Request.Cookies["userID"].Value;
            Database db = new Database();
            if (db.ConnDB()) 
            {
                string sql = "SELECT U.UserName, P.Id, P.PostTheme, P.PostContent,Ari.Categloy, P.CreateTime  FROM PostInfor AS P, UserInfor AS U, AritcleCategroy AS Ari Where  P.UserInforID=U.Id and P.AritcleCategroyID=Ari.id and  P.UserInforID=@UID";
                string[] prar = { "@UID" };
                string[] controller = { CookieUSERID };
                SqlDataReader reader =db.CheckUserDB(sql, prar, controller);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LiteralShowPoster.Text += "<div class='text-white bg-dark border border-white w-100 px-3 py-3'>" +
                        "<p class='my-0'>" + "文章主題：" + $"[{reader["Categloy"]}]\t{reader["PostTheme"]}" + "</p>" +
                        "<p class='my-0'>" + $"發文作者：{reader["UserName"]}\t(發文時間：{reader["CreateTime"]})" + "</p>" +
                        $"<a class='btn btn-info' href='/CreatePost.aspx?posts={reader["Id"]}' style='text-decoration:none'>" + "編輯文章頁面" + "</a>" +
                        "</div>";
                    }
                    Session["ReviseAriticle"] = "Editing";
                }
                else 
                {
                    LiteralShowPoster.Text = "<div class='text-white bg-dark border border-white w-100 px-3 py-3'>" +
                        "<p  class='my-0 text-center ' >" + "查無搜尋內容 " + "</p>" +
                        "</div>";
                }
                db.CloseDB();
            
            }

        
        }


    }
}