using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MLM
{
    public partial class Login_Page : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            cmd.Connection = con;

            if (!Page.IsPostBack)
            {
                DataShow();
            }

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string rf="";
            string ab = txtReference.Text;
           
            if (ab.Length == 0){
                Random rnd = new Random();
                int num = rnd.Next();
                rf = num.ToString();
            }
            else
            {
                rf = ab; 
            }
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            dt = new DataTable();
            cmd.CommandText = "Insert into tblMLM (FirstName,LastName,EmailID,Password,Reference) values (@FirstName,@LastName,@EmailID,@Password,@Reference)";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
            cmd.Parameters.AddWithValue("@EmailID", txtEmailID.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@Reference", rf.ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            DataShow();
            con.Close();
        }
        public void DataShow()
        {
            cmd.Connection = con;
            cmd.CommandText = "Select * from tblMLM";
            sda = new SqlDataAdapter(cmd);
            ds = new DataSet();
            sda.Fill(ds);
            con.Open();
            cmd.ExecuteReader();
            GridView1.DataSource = ds;
            GridView1.DataBind();
            con.Close();
        }      
    }
}