using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;


namespace WebAppl
{
    public partial class WebForm3 : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish a connection with the MongoDB server
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");
                var collection = db.GetCollection<BsonDocument>("adminpage");
                string userId = TextBox1.Text;
                string password = TextBox4.Text;
               


                // Build a BsonDocument instance
                var filter = Builders<BsonDocument>.Filter.Eq("admin", userId);

                // Check if the user exists in the collection 
                var user = collection.Find(filter).FirstOrDefault();

                if (user != null)
                {
                    if (password == user["pass"])
                    {
                        // Login Success
                       
                        Session["username"] = user.GetValue("admin").ToString();
                      Session["Role"] = "admin";
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
                    // User does not exist
                    Response.Write("<script>alert('User does not exist. Please try again.')</script>");
                }
            }catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "')</script>");
            }



        }
    }
}