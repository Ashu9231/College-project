using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebAppl
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }

        }

        /*(-----------------------------------------------------------
         * ---------------------------------------------------------------)*/
        //go button
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);

                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("author managment");

                var filter = Builders<BsonDocument>.Filter.Eq("AuthorID", TextID.Text);
                var result = collection.Find(filter).ToList();
               
                DataTable dt = new DataTable();
                dt.Columns.Add("AuthorID");
                dt.Columns.Add("Authorname");

                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        dt.Rows.Add(item["AuthorID"].ToString(), item["Authorname"].ToString());
                        Textauthor.Text = item["Authorname"].ToString();
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

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
        /*(-----------------------------------------------------------
         * ---------------------------------------------------------------)*/
        //add button
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);

                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("author managment");

                var filter = Builders<BsonDocument>.Filter.Eq("AuthorID", TextID.Text);
                var result = collection.Find(filter).ToList();

                if (result.Count == 0)
                {
                    BsonDocument doc = new BsonDocument
                    {
                        {"Authorname", Textauthor.Text},
                          {"AuthorID", TextID.Text}

                    };
                    collection.InsertOne(doc);
                    BindGrid();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Author added Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Author ID Already Exit!! ')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
        /*(-----------------------------------------------------------
         * ---------------------------------------------------------------)*/
        //update button 
        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);

                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("author managment");

                var filter = Builders<BsonDocument>.Filter.Eq("AuthorID", TextID.Text);
                var result = collection.Find(filter).ToList();
                var update = Builders<BsonDocument>.Update
                     .Set("Authorname", Textauthor.Text);

                if (result.Count == 1)
                {

                    collection.UpdateOne
                        (filter, update);
                    BindGrid();



                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Author Update Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Author ID Does not Exit!! ')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }

        }
        /*(-----------------------------------------------------------
         * ---------------------------------------------------------------)*/
        //delete button
        protected void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                var mongoclient = new MongoClient(strcon);

                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("author managment");

                var filter = Builders<BsonDocument>.Filter.Eq("AuthorID", TextID.Text);
                var result = collection.Find(filter).ToList();
                if (result.Count == 1)
                {
                    BsonDocument doc = new BsonDocument
                    {
                    { "AuthorID", TextID.Text },
                        {"Authorname",Textauthor.Text }
                    };

                    collection.DeleteOne(doc);


                    BindGrid();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Author Delete Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Author ID Does not Exit!! ')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }

        }
        //Bind Gridview
        protected void BindGrid()
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("author managment");
                var filter = Builders<BsonDocument>.Filter.Empty;
                var result = collection.Find(filter).ToList();
                DataTable dt = new DataTable();
                dt.Columns.Add("Author ID", typeof(string));
                dt.Columns.Add("Author Name", typeof(string));
                foreach (var item in result)
                {
                    dt.Rows.Add(item["AuthorID"].ToString(), item["Authorname"].ToString());
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




