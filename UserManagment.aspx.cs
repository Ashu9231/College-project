using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppl.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebAppl
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                getvalue();
                BindGrid();

            }
        }


        //<--------------------------------------------------------UPDATE BUTTON--------------------------------------------------->
        protected async void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");

                var collection = db.GetCollection<BsonDocument>("userloginform");
                string userId = TextBox8.Text;
                string password = TextBox9.Text;

                var filter = Builders<BsonDocument>.Filter.Eq("UserId", userId);

                var result = collection.Find(filter).ToList().FirstOrDefault();


                if (result != null)
                {
                    //if (password == result["Password"].ToString())
                    //{
                    var update = Builders<BsonDocument>.Update
                      .Set("FullName", TextBox1.Text)
                      .Set("DateofBirth", TextBox2.Text)
                      .Set("EmailID", TextBox3.Text)
                      .Set("ContactNumber", TextBox4.Text)
                      .Set("State", DropDownList1.SelectedValue)
                      .Set("CityName", TextBox5.Text)
                      .Set("PinCode", TextBox6.Text)
                      .Set("FullAddress", TextBox7.Text)
                      
                      .Set("Password", TextBox9.Text);
                    collection.UpdateOne
                    (filter, update);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The update was successful." +
                        "Your profile has been updated with the new information')", true);
                    BindGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Check your Password " +
                        "')", true);

                }




            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
            /* try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");

                var collection = db.GetCollection<BsonDocument>("userloginform");
                string userId = TextBox8.Text;
                string password = TextBox9.Text;
                var httpClient = new HttpClient();
                var apiEndpoint = "your_api_endpoint_here";
                var filter = Builders<BsonDocument>.Filter.Eq("UserId", userId);

                var result = collection.Find(filter).ToList().FirstOrDefault();

                if (result != null)
                {
                   //if (password == result["Password"].ToString())
                   //{
                        // Prepare the data to be sent to the API
                        var data = new user
                        {
                            FullName = TextBox1.Text,
                     
                            EmailID = TextBox3.Text,
                            ContactNumber = Int64.Parse(TextBox4.Text),
                            State = DropDownList1.SelectedValue,
                            CityName = TextBox5.Text,
                        
                            FullAddress = TextBox7.Text,
                            UserId = userId,
                            Password = password,
                            Accountstatus = "pending"
                        };
                        Int64 PinCode;
                        if (!Int64.TryParse(TextBox6.Text, out PinCode))
                        {
                            throw new Exception("Please enter a valid PinCode.");
                        }
                        data.PinCode = PinCode;

                        DateTime DateOfBirth;
                        if (!DateTime.TryParse(TextBox2.Text, out DateOfBirth))
                        {
                            throw new Exception("Please enter a valid date.");
                        }
                        data.DateOfBirth = DateOfBirth;
                        var json = JsonConvert.SerializeObject(data);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        // Send the request to the API endpoint
                        
                        var response = await httpClient.PostAsync(apiEndpoint, content);

                        if (response.IsSuccessStatusCode)
                        {
                            var update = Builders<BsonDocument>.Update
                                .Set("FullName", TextBox1.Text)
                                .Set("DateofBirth", TextBox2.Text)
                                .Set("EmailID", TextBox3.Text)
                                .Set("ContactNumber", TextBox4.Text)
                                .Set("State", DropDownList1.SelectedValue)
                                .Set("CityName", TextBox5.Text)
                                .Set("PinCode", TextBox6.Text)
                                .Set("FullAddress", TextBox7.Text)
                                .Set("Password", TextBox9.Text);
                                
                            collection.UpdateOne(filter, update);

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The update was successful." +
                                "Your profile has been updated with the new information')", true);
                            BindGrid();
                        }
                        else
                        {
                            var errorResponse = await response.Content.ReadAsStringAsync();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('An error occurred while creating the user: " + errorResponse + "')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Check your Password " +
                            "')", true);
                    }
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
            */

        }
        protected void BindGrid()
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("book");
                var filter = Builders<BsonDocument>.Filter.Empty;
                var result = collection.Find(filter).ToList();
                DataTable bt = new DataTable();

                bt.Columns.Add("Bookid", typeof(string));
                bt.Columns.Add("Bookname", typeof(string));
                bt.Columns.Add("Authorname", typeof(string));
                bt.Columns.Add("pages", typeof(string));
            
                bt.Columns.Add("Edition", typeof(string));
               
                bt.Columns.Add("Book", typeof(string));
                bt.Columns.Add("Genres", typeof(string));
                bt.Columns.Add("Language", typeof(string));
                bt.Columns.Add("PublisherName", typeof(string));
                bt.Columns.Add("publishDate", typeof(string));
              
            


                foreach (var data in result)
                {
                    bt.Rows.Add(
                        data["Bookid"].ToString(),
                        data["bookname"].ToString(),
                        data["Authorname"].ToString(),
                        data["Pages"].ToString(),
                      
                        data["Edition"].ToString(),
                    
                        data["bookadres"].ToString(),
                        data["genres"].ToString(),
                        data["Language"].ToString(),
                        data["Publishername"].ToString(),
                        data["PublishDate"].ToString()
                      

                    );
                }

                GridView1.DataSource = bt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
        void getvalue()
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");

                var collection = db.GetCollection<BsonDocument>("userloginform");


                var filter = Builders<BsonDocument>.Filter.Eq("UserId", Session["username"]);

                var result = collection.Find(filter).FirstOrDefault();

                if (result != null)
                {


                    Label1.Text = result.GetValue("Accountstatus").ToString();
                    TextBox6.Text = result.GetValue("PinCode").ToString();
                    TextBox1.Text = result.GetValue("FullName").ToString();
                    TextBox2.Text = result.GetValue("DateOfBirth").ToString();
                    TextBox3.Text = result.GetValue("EmailID").ToString();
                    TextBox4.Text = result.GetValue("ContactNumber").ToString();
                    TextBox5.Text = result.GetValue("State").ToString();
                    TextBox5.Text = result.GetValue("CityName").ToString();
                    TextBox7.Text = result.GetValue("FullAddress").ToString();
                    TextBox8.Text = result.GetValue("UserId").ToString();
                    TextBox9.Text = result.GetValue("Password").ToString();


                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }




    }
}