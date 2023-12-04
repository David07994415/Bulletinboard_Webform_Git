using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.DynamicData;
using System.Security.Policy;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Data.Common;

namespace WebApplication1
{
    public class Database
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Partice_Bulletin_Board_SystemConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public bool ConnDB() 
        {
            bool connResult = false;
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open) { connResult = true; }
            return connResult;
        }
        public bool CloseDB() 
        {
            bool closeResult =false;
            conn.Close();
            if (conn.State == System.Data.ConnectionState.Closed) { closeResult = true; }
            return closeResult;
        }
        public SqlDataReader  ShowDB(string sql, WebControl controller = null) 
        {
            cmd.Connection = conn;
            cmd.CommandText = sql;
            reader=cmd.ExecuteReader();
            if (controller is GridView gridView) 
            {
                gridView.DataSource = reader;
                gridView.DataBind();
            }
            return reader;
        }
        
        public SqlDataReader CheckUserDB(string sql, string[] parame = null, string[] controls = null)
        {
            cmd.Connection = conn;
            cmd.CommandText = sql;
            if (parame.Count() != 0 && controls.Count() != 0)
            {
                for (int i = 0; i < parame.Count(); i++)
                {
                    cmd.Parameters.Add(parame[i], controls[i]);
                }
            }
            reader = cmd.ExecuteReader();
            return reader;
        }
        public int ReviseDB(string sql,string[] parame=null, object[] controls = null)
        {
            cmd.Connection = conn;
            cmd.CommandText = sql;
            if (parame.Count()!=0&& controls.Count() != 0) 
            {
                for (int i = 0; i < parame.Count(); i++) 
                {
                    cmd.Parameters.Add(parame[i], controls[i]);
                }
            }
            int resultcount=cmd.ExecuteNonQuery();
            return resultcount;
        }

        public int ReviseDBreturnOneInsert(string sql, string[] parame = null, object[] controls = null)  // 沒有用到
        {
            cmd.Connection = conn;
            cmd.CommandText = sql;
            
            if (parame.Count() != 0 && controls.Count() != 0)
            {
                for (int i = 0; i < parame.Count(); i++)
                {
                    cmd.Parameters.Add(parame[i], controls[i]);
                }
            }
            int resultcount = cmd.ExecuteNonQuery();
            if (resultcount == 1) 
            {
                cmd.CommandText = "SELECT SCOPE_IDENTITY()";
            }
            return resultcount;
        }

        public string HashPassword(string password) 
        {
            string salt = WebConfigurationManager.AppSettings["Salt"].ToString();
            byte[] coding = Encoding.UTF8.GetBytes("salt");
            string hashpw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: coding,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 128 / 8)); ; 
            return hashpw;
        }

        public void readerclose() 
        {
            reader.Dispose();
        }
    }
}