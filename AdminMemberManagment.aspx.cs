using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using static System.Net.Mime.MediaTypeNames;

namespace WebAppl
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                BindGrid();

            }
        }
        //go buttom
        protected void Button1_Click(object sender, EventArgs e)
        {


           try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");

                var collection = db.GetCollection<BsonDocument>("userloginform");
                string userId = TextBox1.Text;

                var filter = Builders<BsonDocument>.Filter.Eq("UserId", userId);
                var result = collection.Find(filter).FirstOrDefault();
            

                if (result != null)
                {
                      TextBox2.Text = result.GetValue("FullName").ToString();
                      TextBox7.Text = result.GetValue("Accountstatus").ToString();
                      TextBox5.Text = result.GetValue("EmailID").ToString();
                      TextBox6.Text = result.GetValue("ContactNumber").ToString();
                      TextBox8.Text = result.GetValue("DateOfBirth").ToString();
                      TextBox10.Text =result.GetValue("State").ToString();
                      TextBox3.Text = result.GetValue("CityName").ToString();
                      TextBox4.Text = result.GetValue("PinCode").ToString();
                      TextBox9.Text = result.GetValue("FullAddress").ToString();
                   



                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No records Found.')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }





        }
        //Permanataly delete user button
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");

                var collection = db.GetCollection<BsonDocument>("userloginform");
                string userId = TextBox1.Text;


                var filter = Builders<BsonDocument>.Filter.Eq("UserId", userId);

                var result = collection.Find(filter).ToList();
               



                if (result.Count == 1)
                {
                    BsonDocument doc = new BsonDocument
                    {


                        {"FullName", TextBox2.Text},
                        {"DateofBirth",  TextBox8.Text},
                        {"EmailID",  TextBox5.Text},
                        {"ContactNumber",  TextBox6.Text},
                        {"State",    TextBox10.Text},
                        {"CityName", TextBox3.Text},
                        {"PinCode", TextBox4.Text},
                        {"FullAddress", TextBox9.Text},
                        {"UserID", TextBox1.Text},
                        {"Password", ""},
                        {"Accountstatus","" }


                    };
                    collection.DeleteOne(filter);


                    

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Member ID Deleted Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Member ID Already Exit!! ')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }

            BindGrid();
        }
        //LinkButton1 active
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");

                var collection = db.GetCollection<BsonDocument>("userloginform");
                string userId = TextBox1.Text;


                var filter = Builders<BsonDocument>.Filter.Eq("UserId", userId);

                var result = collection.Find(filter).ToList();
                var update = Builders<BsonDocument>.Update
                      .Set("Accountstatus", "Active");

                if (result.Count == 1)
                {

                    collection.UpdateOne
                        (filter, update);
                    BindGrid();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Member ID Activate Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Member ID Already Exit!! ')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
            BindGrid();
        }
        // //LinkButton1 Resume
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");

                var collection = db.GetCollection<BsonDocument>("userloginform");
                string userId = TextBox1.Text;


                var filter = Builders<BsonDocument>.Filter.Eq("UserId", userId);

                var result = collection.Find(filter).ToList();
                var update = Builders<BsonDocument>.Update
                      .Set("Accountstatus", "Pending");

                if (result.Count == 1)
                {

                    collection.UpdateOne
                        (filter, update);
                    BindGrid();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Member ID resume Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Member ID Already Exit!! ')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
            BindGrid();
        }
        //LinkButton1 Deactive
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");

                var collection = db.GetCollection<BsonDocument>("userloginform");
                string userId = TextBox1.Text;


                var filter = Builders<BsonDocument>.Filter.Eq("UserId", userId);

                var result = collection.Find(filter).ToList();
                var update = Builders<BsonDocument>.Update
                      .Set("Accountstatus", "Deactive");

                if (result.Count == 1)
                {

                    collection.UpdateOne
                        (filter, update);
                    BindGrid();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Member ID Deactivate Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Publisher ID Already Exit!! ')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
            BindGrid();
        }
        protected void BindGrid()
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("userloginform");
                var filter = Builders<BsonDocument>.Filter.Empty;
                var result = collection.Find(filter).ToList();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("DOB", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Contact", typeof(string));

                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("Account Stutas", typeof(string));


                foreach (var item in result)
                {
                    dt.Rows.Add(item["UserId"].ToString(),
                    item["FullName"].ToString(),
                    item["DateOfBirth"].ToString(),
                    item["EmailID"].ToString(),
                     item["ContactNumber"].ToString(),
                   
                     item["CityName"].ToString(),
                     item["Accountstatus"].ToString()

                        );
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
    }
}