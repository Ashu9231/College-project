using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace WebAppl
{
    public partial class WebForm6 : System.Web.UI.Page
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
                var collection = database.GetCollection<BsonDocument>("publisher managment");

                var filter = Builders<BsonDocument>.Filter.Eq("PublisherID", TextpubID.Text);
                var result = collection.Find(filter).ToList();

                DataTable td = new DataTable();
                td.Columns.Add("Publisher ID", typeof(string));
                td.Columns.Add("Publisher Name", typeof(string));

                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        td.Rows.Add(item["PublisherID"].ToString(), item["Publishername"].ToString());
                        Textpub.Text = item["Publishername"].ToString();
                       // Textpub.Text = item["Authorname"].ToString();
                    }
                   // GridView1.DataSource = td;
                  //  GridView1.DataBind();

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
                var collection = database.GetCollection<BsonDocument>("publisher managment");

                var filter = Builders<BsonDocument>.Filter.Eq("PublisherID", TextpubID.Text);
                var result = collection.Find(filter).ToList();

                if (result.Count == 0)
                {
                    BsonDocument doc = new BsonDocument
                    {
                        {"Publishername", Textpub.Text},
                          {"PublisherID", TextpubID.Text}

                    };
                    collection.InsertOne(doc);
                    BindGrid();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Publisher Added Successfully')", true);
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

        }
        /*(-----------------------------------------------------------
         * ---------------------------------------------------------------)*/
        //update button 
        protected void Button5_Click(object sender, EventArgs e)
        {

                       try {
                var mongoclient = new MongoClient(strcon);

                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("publisher managment");

                var filter = Builders<BsonDocument>.Filter.Eq("PublisherID", TextpubID.Text);
                var result = collection.Find(filter).ToList();

                var update = Builders<BsonDocument>.Update
                      .Set(   "Publishername", Textpub.Text);

                if (result.Count == 1)
                {

                    collection.UpdateOne
                        (filter, update);
                    BindGrid();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Publisher Update Successfully')", true);
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
                var collection = database.GetCollection<BsonDocument>("publisher managment");

                var filter = Builders<BsonDocument>.Filter.Eq("PublisherID", TextpubID.Text);
                var result = collection.Find(filter).ToList();
                if (result.Count == 1)
                {
                    BsonDocument doc = new BsonDocument
                    {
                     {"Publishername", Textpub.Text},
                          {"PublisherID", TextpubID.Text}
                    };

                    collection.DeleteOne(doc);


                    BindGrid();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Publisher Delete Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Publisher ID Does not Exit!! ')", true);
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
                var collection = database.GetCollection<BsonDocument>("publisher managment");
                var filter = Builders<BsonDocument>.Filter.Empty;
                var result = collection.Find(filter).ToList();
                DataTable td = new DataTable();
                td.Columns.Add("Publisher ID", typeof(string));
                td.Columns.Add("Publisher Name", typeof(string));
                foreach (var item in result)
                {
                    td.Rows.Add(item["PublisherID"].ToString(), item["Publishername"].ToString());
                }
                GridView1.DataSource = td;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }




    }
}