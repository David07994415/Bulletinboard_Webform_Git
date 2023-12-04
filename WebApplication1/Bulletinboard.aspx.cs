using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Bulletinboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["LoginState"]) == false)
                {
                    Response.Redirect("~/Login.aspx");
                }
                if (Convert.ToBoolean(Session["LoginState"]) == true)
                {
                    Database db = new Database();
                    db.ConnDB();
                    string sqlwleftjoin = "SELECT \r\n    CASE WHEN MAX(R.CreateTime) IS NULL THEN 0 ELSE COUNT(*) END AS PostCount,\r\n    CASE WHEN MAX(R.CreateTime) IS NULL THEN '無' ELSE CONVERT(VARCHAR, MAX(R.CreateTime), 120) END AS LastestReply,\r\n    A.Categloy,\r\n    P.Id,\r\n    P.PostTheme, \r\n    P.PostContent,\r\n    P.CreateTime,\r\n    U.UserName\r\nFROM \r\n    dbo.AritcleCategroy AS A\r\nJOIN \r\n    dbo.PostInfor AS P ON A.id = P.AritcleCategroyID\r\nJOIN \r\n    dbo.UserInfor AS U ON P.UserInforID = U.Id\r\nLEFT JOIN \r\n    dbo.ReplyInfor AS R ON R.PostInforID = P.Id WHERE  P.AuthPostId=1 \r\nGROUP BY \r\n    P.Id, P.PostTheme, P.PostContent, P.CreateTime, U.UserName, A.Categloy";
                    SqlDataReader reader = db.ShowDB(sqlwleftjoin);

                    if (reader.HasRows) 
                    {
                        while (reader.Read()) 
                        {
                                LiteralTable.Text +="<div class='text-white bg-dark border border-white w-100 px-3 py-3'>" + 
                                "<p  class='my-0' >" + "文章主題：" + $"[{reader["Categloy"]}]" + "\t" + $"<a class='text-light' href='/PostPage.aspx?posts={reader["Id"]}' style='text-decoration:none'>" + $"{reader["PostTheme"]}" + "</a>" + " </p>" +
                                "<p  class='my-0'>" +  "發文作者：" + $"{reader["UserName"]}" + "\t" + $"(發文時間：{reader["CreateTime"]})" + " </p>" +
                                 "<p  class='my-0'>" + "回覆數量：" + $"{reader["PostCount"]}" + "\t" + $"(最新回覆時間：{reader["LastestReply"]})" + " </p>" +
                                "</div>";
                        }
                    }
                    db.readerclose();
                    db.CloseDB();

                }

            }

        }

        protected void ButtonCreatePost_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreatePost.aspx");
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)   // 搜尋 Search
        {
            if (DropDownListSearch.SelectedValue == "name")
            {
                Database db = new Database();
                db.ConnDB();
                string sql = "SELECT COUNT(*) AS PostCount, MAX(R.CreateTime) as LastestReply, A.Categloy,P.Id,P.PostTheme, P.PostContent,P.CreateTime,U.UserName  FROM dbo.AritcleCategroy as A, dbo.PostInfor as P,dbo.UserInfor as U,dbo.ReplyInfor as R  Where A.id=P.AritcleCategroyID and P.UserInforID=U.Id and R.PostInforID=P.Id and U.UserName LIKE @username and P.AuthPostId=1 GROUP BY P.Id, P.PostTheme, P.PostContent, P.CreateTime, U.UserName,A.Categloy";
                string sqlwleft = "SELECT \r\nCASE WHEN MAX(R.CreateTime) IS NULL THEN 0 ELSE COUNT(*) END AS PostCount,\r\nCASE WHEN MAX(R.CreateTime) IS NULL THEN '無' ELSE CONVERT(VARCHAR, MAX(R.CreateTime), 120) END AS LastestReply,\r\nA.Categloy,P.Id, P.PostTheme,  P.PostContent,P.CreateTime,U.UserName \r\nFROM dbo.AritcleCategroy AS A\r\nJOIN dbo.PostInfor AS P ON A.id = P.AritcleCategroyID \r\nJOIN dbo.UserInfor AS U ON P.UserInforID = U.Id \r\nLEFT JOIN dbo.ReplyInfor AS R ON R.PostInforID = P.Id \r\nWhere U.UserName LIKE @username  \r\nGROUP BY P.Id, P.PostTheme, P.PostContent, P.CreateTime, U.UserName, A.Categloy";
                string[] parar = { "@username" };
                string[] contorls = { $"%{TextBoxSearch.Text}%" };
                SqlDataReader reader = db.CheckUserDB(sqlwleft, parar, contorls); 
                LiteralTable.Text = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LiteralTable.Text += "<div class='text-white bg-dark border border-white w-100 px-3 py-3'>" +
                        "<p  class='my-0' >" + "文章主題：" + $"[{reader["Categloy"]}]" + "\t" + $"<a class='text-light' href='/PostPage.aspx?posts={reader["Id"]}' style='text-decoration:none'>" + $"{reader["PostTheme"]}" + "</a>" + " </p>" +
                        "<p  class='my-0'>" + "發文作者：" + $"{reader["UserName"]}" + "\t" + $"(發文時間：{reader["CreateTime"]})" + " </p>" +
                         "<p  class='my-0'>" + "回覆數量：" + $"{reader["PostCount"]}" + "\t" + $"(最新回覆時間：{reader["LastestReply"]})" + " </p>" +
                        "</div>";
                    }
                }
                else 
                {
                    LiteralTable.Text += "<div class='text-white bg-dark border border-white w-100 px-3 py-3'>" +
                        "<p  class='my-0 text-center ' >" + "查無搜尋內容 " +"</p>" +
                        "</div>";
                }
                db.readerclose();
                db.CloseDB();
            }
            else if (DropDownListSearch.SelectedValue == "post") 
            {
                Database db = new Database();
                db.ConnDB();
            
                string sqlwleft = "SELECT \r\nCASE WHEN MAX(R.CreateTime) IS NULL THEN 0 ELSE COUNT(*) END AS PostCount,\r\nCASE WHEN MAX(R.CreateTime) IS NULL THEN '無' ELSE CONVERT(VARCHAR, MAX(R.CreateTime), 120) END AS LastestReply,\r\nA.Categloy,P.Id, P.PostTheme,  P.PostContent,P.CreateTime,U.UserName \r\nFROM dbo.AritcleCategroy AS A\r\nJOIN dbo.PostInfor AS P ON A.id = P.AritcleCategroyID \r\nJOIN dbo.UserInfor AS U ON P.UserInforID = U.Id \r\nLEFT JOIN dbo.ReplyInfor AS R ON R.PostInforID = P.Id \r\nWhere P.PostTheme LIKE @PostTheme and P.AuthPostId=1  \r\nGROUP BY P.Id, P.PostTheme, P.PostContent, P.CreateTime, U.UserName, A.Categloy"; ;
                string[] parar = { "@PostTheme" };
                string[] contorls = { $"%{TextBoxSearch.Text}%" };
                SqlDataReader reader = db.CheckUserDB(sqlwleft, parar, contorls); 
                LiteralTable.Text = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LiteralTable.Text += "<div class='text-white bg-dark border border-white w-100 px-3 py-3'>" +
                        "<p  class='my-0' >" + "文章主題：" + $"[{reader["Categloy"]}]" + "\t" + $"<a class='text-light' href='/PostPage.aspx?posts={reader["Id"]}' style='text-decoration:none'>" + $"{reader["PostTheme"]}" + "</a>" + " </p>" +
                        "<p  class='my-0'>" + "發文作者：" + $"{reader["UserName"]}" + "\t" + $"(發文時間：{reader["CreateTime"]})" + " </p>" +
                         "<p  class='my-0'>" + "回覆數量：" + $"{reader["PostCount"]}" + "\t" + $"(最新回覆時間：{reader["LastestReply"]})" + " </p>" +
                        "</div>";
                    }
                }
                else
                {
                    LiteralTable.Text += "<div class='text-white bg-dark border border-white w-100 px-3 py-3'>" +
                        "<p  class='my-0 text-center ' >" + "查無搜尋內容 " + "</p>" +
                        "</div>";
                }
                db.readerclose();
                db.CloseDB();

            }
        }
    }
}