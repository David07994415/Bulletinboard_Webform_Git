using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class BackPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToBoolean(Session["LoginState"]) == false || Convert.ToInt16(Session["premission"]) != 2)
                {
                    Response.Redirect("~/Login.aspx");  // Server.Transfer("~/Login.aspx");  //網址URL會有問題
                }
                else 
                {
                    ShowGridView_DB();
                }
            }
        }


        protected void ShowGridView_DB() 
        {
            Database db= new Database();
            if (db.ConnDB())
            {
                string sql = "Select P.Id, Ari.Categloy, P.PostTheme, P.PostContent, U.UserName,Auth.State,P.AuthPostId FROM PostInfor AS P, UserInfor  AS U, AritcleCategroy AS Ari, AuthPost AS Auth WHERE P.UserInforID=U.Id and P.AritcleCategroyID=Ari.Id and P.AuthPostId=Auth.Id";
                db.ShowDB(sql, GridViewAllPost);
                db.CloseDB();
            }
        }

        protected void GridViewAllPost_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewAllPost.EditIndex = e.NewEditIndex; // 切換到編輯模式
            ShowGridView_DB();   // 不加入這段，會有問題 (資料行變成白色的)
        }

        protected void GridViewAllPost_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewAllPost.EditIndex = -1; // 取消編輯模式
            ShowGridView_DB();   // 不加入這段，會有問題 (資料行變成白色的)
        }

        protected void GridViewAllPost_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow FocusRow = GridViewAllPost.Rows[e.RowIndex];                                              // 獲得選取的資料行
            TextBox TextBoxPostTheme = (TextBox)FocusRow.FindControl("TextBoxPostThemeEdit");   //  獲得選取的資料行中的指定Textbox
            string PostThemeChange = TextBoxPostTheme.Text;                                                              //  轉存該指定的資料變成string
            TextBox TextBoxPostContent = (TextBox)FocusRow.FindControl("TextBoxPostContentEdit");
            string PostContentChange = TextBoxPostContent.Text;
            DropDownList AuthLevel = (DropDownList)FocusRow.FindControl("DropDownListAuth");
            string AuthLevelChange = AuthLevel.SelectedValue;

            int idIndex = Convert.ToInt32(GridViewAllPost.DataKeys[e.RowIndex].Value);                        //  獲得選得的欄位的id值

            Database db = new Database();
            if (db.ConnDB())
            {
                string sql = "UPDATE PostInfor SET PostTheme=@PostTheme, PostContent=@PostContent, AuthPostId=@AuthPostId WHERE Id=@Id";
                string[] parar = { "@PostTheme", "@PostContent", "@AuthPostId", "@Id" };
                string[] source = { PostThemeChange,PostContentChange, AuthLevelChange, idIndex.ToString() };
                int reviseResult = db.ReviseDB(sql, parar, source);
                if (reviseResult == 1) { Response.Write("<script>alert('修改成功')</script>"); }
                else { Response.Write("<script>alert('修改失敗')</script>"); }
                db.CloseDB();
            }
            GridViewAllPost.EditIndex = -1;
            ShowGridView_DB();
        }

        protected void GridViewAllPost_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt16(GridViewAllPost.DataKeys[e.RowIndex].Value);
            Database db = new Database();
            if (db.ConnDB())
            {
                string sql = "DELETE PostInfor WHERE Id=@Id";
                string[] parar = { "@Id" };
                string[] source = { id.ToString()  };
                int reviseResult = db.ReviseDB(sql, parar, source);
                if (reviseResult == 1) { Response.Write("<script>alert('刪除成功')</script>"); }
                else { Response.Write("<script>alert('刪除失敗')</script>"); }
                db.CloseDB();
            }
            GridViewAllPost.EditIndex = -1;
            ShowGridView_DB();

        }
    }
}