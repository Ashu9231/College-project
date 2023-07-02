using Microsoft.SqlServer.Server;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebAppl.Models;



namespace WebAppl
{

    public partial class WebForm4 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        /*(------------------------------------------------------------------
         * --------------------------------------------------------)*/


        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            /*  try
              {
                  var mongoClient = new MongoClient(strcon);
                  var database = mongoClient.GetDatabase("ELibraryManagment");

                  var collection = database.GetCollection<Client>("userloginform");
                  var filter = Builders<Client>.Filter.Eq("UserID", Textuserid.Text);

                  var result = collection.Find(filter).ToList();

                  if (result.Count == 0)
                  {
                      Client user = new Client
                      {
                          FullName = Textfullname.Text,
                          EmailID = email.Text,
                          State = Drop.SelectedValue,
                          CityName = cityname.Text,
                          ContactNumber = int.Parse(number.Text),
                          FullAddress = Textfulladress.Text,
                          UserID = Textuserid.Text,
                          Password = Textpass.Text,
                          Accountstatus = "pending"
                      };
                      int PinCode;
                      if (!int.TryParse(Textpincode.Text, out PinCode))
                      {
                          throw new Exception("Please enter a valid PinCode.");
                      }
                      user.PinCode = PinCode;

                      DateTime DateOfBirth;
                      if (!DateTime.TryParse(Textdob.Text, out DateOfBirth))
                      {
                          throw new Exception("Please enter a valid date.");
                      }
                      user.DateOfBirth = DateOfBirth;


                      if (string.IsNullOrEmpty(Textuserid.Text) ||
                          string.IsNullOrEmpty(Textfullname.Text) ||
                          string.IsNullOrEmpty(email.Text) ||
                          string.IsNullOrEmpty(Drop.SelectedValue) ||
                          string.IsNullOrEmpty(cityname.Text) ||
                          string.IsNullOrEmpty(number.Text) ||
                          string.IsNullOrEmpty(Textfulladress.Text) ||
                          string.IsNullOrEmpty(Textpass.Text))
                      {
                          ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please fill in all fields.')", true);
                      }
                      else
                      {
                          collection.InsertOne(user);
                          ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Login successful.')", true);
                      }
                  }
                  else
                  {
                      ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User ID already exists.')", true);
                  }
              }
              catch (Exception ex)
              {
                  var errorMessage = $"An error occurred while creating the user: {ex.Message}";
                  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('{errorMessage}')", true);
              }
            */

            try
            {
                var httpClient = new HttpClient();
                var apiEndpoint = "https://localhost:7202/api/User";


                // Prepare the data to be sent to the API
                var data = new user
                {
                    FullName = Textfullname.Text,

                    EmailID = email.Text,
                    ContactNumber = Int64.Parse(number.Text),
                    State = Drop.SelectedValue,
                    CityName = cityname.Text,

                    FullAddress = Textfulladress.Text,
                    UserId = Textuserid.Text,
                    Password = Textpass.Text,
                    Accountstatus = "pending",
                    DateOfBirth =  DateTime.Now.ToString (Textdob.Text)

                };
                Int64 PinCode;
                if (!Int64.TryParse(Textpincode.Text, out PinCode))
                {
                    throw new Exception("Please enter a valid PinCode.");
                }
                data.PinCode = PinCode;



                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the request to the API endpoint
                var response = await httpClient.PostAsync(apiEndpoint, content);


                // Check the response status code
                if (response.IsSuccessStatusCode)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Login successful.')", true);
                }
                else
                {
                    var errorMessage = $"An error occurred while creating the user: {response.ReasonPhrase}";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('{errorMessage}')", true);
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while creating the user: {ex.Message}";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('{errorMessage}')", true);
            }


        }

        void blank()
        {

            Textfullname.Text = "";
            Textdob.Text = "";
            email.Text = "";
            number.Text = "";
            cityname.Text = "";
            Textpincode.Text = "";
            Textfulladress.Text = "";
            Textuserid.Text = "";
            Textpass.Text = "";


        }

    }


}







































































