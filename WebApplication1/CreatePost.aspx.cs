using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class CreatePost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["LoginState"]) == false)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else if (Session["ReviseAriticle"].ToString() == "Editing" && Request.QueryString["posts"] !=null) 
                {
                    ButtonRevise.Visible = true;
                    ButtonCreate.Visible = false;
                    ButtonCreate.Enabled = false;
                    LinkDB();
                }
            }
         }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid) 
            {
                Database db = new Database();
                db.ConnDB();
                string sql = "INSERT INTO PostInfor(PostTheme,PostContent,UserInforID,AritcleCategroyID) VALUES(@PostTheme,@PostContent,@UserInforID,@AritcleCategroyID)";
                string[] param = { "@PostTheme", "@PostContent", "@UserInforID", "@AritcleCategroyID" };
                string[] datasours = { TextTopicInput.Text, TextContentInput.Text, Request.Cookies["userID"].Value.ToString(), DropDownListCategloy.SelectedValue};
                int insertResult = db.ReviseDB(sql, param, datasours);
                if (insertResult == 1) 
                {
                    Response.Write("<script>alert('新增成功文章')</script>");
                    Response.Redirect("~/Bulletinboard.aspx");
                }
                db.CloseDB();
            }
        }

        protected void ButtonReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Bulletinboard.aspx");
        }


        protected void LinkDB() 
        {
            string postid = Request.QueryString["posts"];
            Database db =new Database();
            if (db.ConnDB()) 
            {
                string sql = "SELECT PostTheme,PostContent,AritcleCategroyID FROM PostInfor Where Id=@Id";
                string[] para = { "@Id" };
                string[] control = { postid };
                SqlDataReader Reader= db.CheckUserDB(sql, para, control);
                if (Reader.Read()) 
                {
                    DropDownListCategloy.SelectedValue= Reader["AritcleCategroyID"].ToString();
                    TextTopicInput.Text= Reader["PostTheme"].ToString();
                    TextContentInput.Text=Reader["PostContent"].ToString();
                }
                Reader.Close();
                db.CloseDB();   
            }
        }

        protected void ButtonRevise_Click(object sender, EventArgs e)
        {
            string postid = Request.QueryString["posts"];
            string AuthLevel = "3";                                     // 對應資料庫，3為未通過授權
            string datetimenow = DateTime.Now.ToString();

            Database db = new Database();
            if (db.ConnDB())
            {
                string sql = "Update PostInfor Set PostTheme=@PostTheme,PostContent=@PostContent,AritcleCategroyID=@AritcleCategroyID,AuthPostId=@AuthPostId,CreateTime=@CreateTime Where Id=@Id";
                string[] para = { "@PostTheme", "@PostContent", "@AritcleCategroyID", "@AuthPostId", "@CreateTime", "@Id" };
                object[] control = { TextTopicInput.Text, TextContentInput.Text, DropDownListCategloy.SelectedValue, AuthLevel, DateTime.Now, postid };
                int reviseNum = db.ReviseDB(sql, para, control);
                if (reviseNum==1)
                {
                    Response.Write("<script>alert('修改完成')</script>");
                    Response.Redirect("~/MyPostPage.aspx");
                }
                else 
                {
                    Response.Write("<script>alert('修改失敗')</script>");
                }
                db.CloseDB();
            }
        }
    }
}