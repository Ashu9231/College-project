using MongoDB.Bson;
using MongoDB.Driver;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppl
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Memberid = TextBox1.Text;
                string password = TextBox4.Text;

                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");
                var collection = db.GetCollection<BsonDocument>("userloginform");
                var filter = Builders<BsonDocument>.Filter.Eq("UserId", Memberid);
                var result = collection.Find(filter).FirstOrDefault();
                if (result != null)
                { 
                    string Password = result["Password"].ToString();
                    if (password == Password)
                    {
                       
                        Session["username"] = result.GetValue("UserId").ToString();
                        
                        Session["Fullname"] = result.GetValue("FullName").ToString();
                         Session["Role"] = "User";
                        Response.Redirect("homepage.aspx");

                    }
                    else
                    {
                        // Wrong password
                        Response.Write("<script>alert('Wrong Password. Please try again.')</script>");
                        
                    }
                }
                else
                {
                    Response.Write("<Script>alert('Invalid MemberID ')</script>");

                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "')</script>");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Userloginform.aspx");
        }
    }

}

