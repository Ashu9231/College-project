using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppl.Models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace WebAppl
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                BindGrid();

            }
        }


        // Go buttun
        protected void Button1_Click(object sender, EventArgs e)
        {
            get();
            get2();
        }
        //issued book 
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<Bookissuing>("bookissuing");

                var filter = Builders<Bookissuing>.Filter.Eq("bookid", TextBox1.Text);
                var result = collection.Find(filter).ToList();




                if (result.Count == 0)
                {


                    Bookissuing book = new Bookissuing
                    {
                        bookid = TextBox1.Text.Trim(),
                        bookname = TextBox6.Text.Trim(),
                        userid = TextBox2.Text.Trim(),
                        fullname = TextBox5.Text.Trim(),
                        enddate = TextBox4.Text.Trim(),
                        startdate = TextBox3.Text.Trim(),
                        accountstatus = "pending"
                    };

                    collection.InsertOne(book);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Book added successfully.')", true);
                    BindGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Book ID already exists. Please enter a unique ID for the book.')", true);
                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }

        }
        //return book
        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);

                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("bookissuing");

                var filter = Builders<BsonDocument>.Filter.Eq("bookid", TextBox1.Text);
                var result = collection.Find(filter).ToList();
                var update = Builders<BsonDocument>.Update
                     .Set("enddate", TextBox4.Text);

                if (result.Count == 1)
                {

                    collection.UpdateOne
                        (filter, update);
                    BindGrid();



                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Update Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' ID Does not Exit!! ')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
            Return();
        }
        void get()
        {
            try
            {
                var mongoClient = new MongoClient(strcon);
                var database = mongoClient.GetDatabase("ELibraryManagment");
                var col = database.GetCollection<BsonDocument>("book");

                // Find the book with the specified book ID.
                var fil = Builders<BsonDocument>.Filter.Eq("Bookid", TextBox1.Text);
                var issu   = col.Find(fil).ToList();
                //DataTable dt = new DataTable();
               
                //dt.Columns.Add("bookname");

                if (issu != null)
                {
                    foreach (var data in issu)
                    {

                        TextBox6.Text = data["bookname"].ToString();

                    }
                    // Populate the form fields with data from the retrieved book document.
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
        void get2()
        {
            try
            {
                var mongoClient = new MongoClient(strcon);
                var database = mongoClient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("userloginform");

                // Find the book with the specified book ID.
                var fil = Builders<BsonDocument>.Filter.Eq("UserId", TextBox2.Text);
                var bo1 = collection.Find(fil).FirstOrDefault();

                if (bo1 != null)
                {
                    TextBox5.Text = bo1.GetValue("FullName").ToString();
                    // Populate the form fields with data from the retrieved book document.
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
        protected void BindGrid()
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("bookissuing");
                var filter = Builders<BsonDocument>.Filter.Empty;
                var result = collection.Find(filter).ToList();
                DataTable bt = new DataTable();

                bt.Columns.Add("Book Id", typeof(string));
                bt.Columns.Add("Book Name", typeof(string));
                bt.Columns.Add("Name", typeof(string));
                bt.Columns.Add("Member ID", typeof(string));
                bt.Columns.Add("Issue Date", typeof(string));
                bt.Columns.Add("Return Date", typeof(string));
                bt.Columns.Add("Account Status", typeof(string));



                foreach (var data in result)
                {
                    bt.Rows.Add(
                        data["bookid"].ToString(),
                        data["bookname"].ToString(),
                        data["fullname"].ToString(),
                        data["userid"].ToString(),
                        data["startdate"].ToString(),
                        data["enddate"].ToString(),
                        data["accountstatus"].ToString()


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
       void Return()
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");

                var collection = db.GetCollection<BsonDocument>("bookissuing");
                string userId = TextBox1.Text;


                var filter = Builders<BsonDocument>.Filter.Eq("bookid", userId);

                var result = collection.Find(filter).ToList();
                var update = Builders<BsonDocument>.Update
                      .Set("accountstatus", "Return");

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

    }

}