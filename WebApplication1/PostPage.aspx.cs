using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ExplorerBar;

namespace WebApplication1
{
    public partial class PostPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["LoginState"]) == false || Request.QueryString["posts"] == null)
                {
                    //Server.Transfer("~/Login.aspx");  //網址URL會有問題
                    Response.Redirect("~/Login.aspx");
                }
                if (Request.QueryString["posts"] != null)
                {
                    Database db = new Database();
                    if (db.ConnDB())                                                                                                   // 留言區
                    {
                        string Sql = "SELECT R.UserInforID, U.UserName, A.Appraisal,R.CreateTime,R.ReplyContent from PostInfor as P,AppraisalCategory as A,ReplyInfor as R,UserInfor as U\r\nWHERE P.id=R.PostInforID and  R.AppraisalId=A.Id and R.UserInforID=U.Id and R.PostInforID=@PostInforID";
                        string[] parameterpost = { "@PostInforID" };
                        string[] controlspost = { Request.QueryString["posts"] };
                        SqlDataReader reader = db.CheckUserDB(Sql, parameterpost, controlspost);
                        //PostPage testobj = new PostPage();   // Reapeater嘗試
                        //testobj.RepeaterBind(reader);
                        while (reader.Read())
                        {
                            if (reader["UserInforID"].ToString() == Request.Cookies["userID"].Value)
                            {
                                LiteralReplys.Text +=
                                    "<div class='d-flex justify-content-between align-items-center bg-info'>" +
                                            "<div>" + $"[{reader["Appraisal"].ToString().Trim()}]" + "\t\t" + reader["UserName"].ToString() + "：" + reader["ReplyContent"].ToString() + "</div>" +
                                            "<div>" + reader["CreateTime"].ToString() + "</div>" +
                                      "</div>";
                            }
                            else
                            {
                                LiteralReplys.Text +=
                                    "<div class='d-flex justify-content-between align-items-center'>" +
                                            "<div>" + $"[{reader["Appraisal"].ToString().Trim()}]" + "\t\t" + reader["UserName"].ToString() + "：" + reader["ReplyContent"].ToString() + "</div>" +
                                             "<div>" + reader["CreateTime"].ToString() + "</div>" +
                                     "</div>";
                            }
                        }

                        db.readerclose();   //要先關閉才能讓reader物件連接到新的物件    // 主題區
                        Sql = "SELECT A.Categloy,P.Id,P.PostTheme, P.PostContent,P.CreateTime,U.UserName FROM dbo.AritcleCategroy as A, dbo.PostInfor as P,dbo.UserInfor as U Where A.id=P.AritcleCategroyID and P.UserInforID=U.Id and P.Id=@PID";
                        string[] parameter = { "@PID" };
                        string[] controls = { Request.QueryString["posts"] };
                        reader = db.CheckUserDB(Sql, parameter, controls);
                        while (reader.Read())
                        {
                            string content = reader["PostContent"].ToString();
                            string contentbr = content.Replace("\r\n", "<br/>");
                            LabelTopicSQL.Text = $"[{reader["Categloy"].ToString().Trim()}]" + "\t" + reader["PostTheme"].ToString() + $"(發文者：{reader["UserName"].ToString()})";
                            LabelCreateTimeSQL.Text = reader["CreateTime"].ToString();
                            LabelContentSQL.Text = contentbr;
                        }
                        db.readerclose();
                        reader.Close();
                    }
                    db.CloseDB();
                }
            }

        }

        protected void ButtonAddReply_Click(object sender, EventArgs e)
        {
            LabelAppraisal.Visible = true;
            DropDownListAppraisal.Visible = true;
            LabelReply.Visible = true;
            TextBoxReply.Visible = true;
            ButtonCancel.Visible = true;
            ButtonSubmitReply.Visible = true;
            ButtonAddReply.Visible = false;
            SetFocus(DropDownListAppraisal);

        }

        protected void ButtonSubmitReply_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                Database db = new Database();
                if (db.ConnDB())
                {
                    string Sql = "INSERT INTO ReplyInfor(UserInforID,PostInforID,ReplyContent,AppraisalId) VALUES (@UserInforID,@PostInforID,@ReplyContent,@AppraisalId)";
                    string[] paramets = { "@UserInforID", "@PostInforID", "@ReplyContent", "@AppraisalId" };
                    string[] controls = { Request.Cookies["userID"].Value, Request.QueryString["posts"], TextBoxReply.Text, DropDownListAppraisal.SelectedValue };
                    int replynum = db.ReviseDB(Sql, paramets, controls);
                    if (replynum == 1)
                    {
                        Response.Write("<script>alert('新增成功')</script>");
                        Response.Redirect($"~/PostPage.aspx?posts={Request.QueryString["posts"]}");
                    }
                }
                db.CloseDB();
            }
            ButtonAddReply.Visible = true;
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            LabelAppraisal.Visible = false;
            DropDownListAppraisal.Visible = false;
            LabelReply.Visible = false;
            TextBoxReply.Visible = false;
            TextBoxReply.Text = string.Empty;
            ButtonCancel.Visible = false;
            ButtonSubmitReply.Visible = false;
            ButtonAddReply.Visible = true;
        }

        //protected void RepeaterBind(SqlDataReader reader)
        //{
        //    RepeaterReplays.DataSource = reader;
        //    RepeaterReplays.DataBind();
        //}

    }

}