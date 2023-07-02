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

namespace WebAppl
{
    public partial class WebForm16 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
           
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
                bt.Columns.Add("Cost", typeof(string));
                bt.Columns.Add("Edition", typeof(string));
                bt.Columns.Add("Description", typeof(string));
                bt.Columns.Add("Book", typeof(string));
                bt.Columns.Add("Genres", typeof(string));
                bt.Columns.Add("Language", typeof(string));
                bt.Columns.Add("PublisherName", typeof(string));
                bt.Columns.Add("publishDate", typeof(string));
                bt.Columns.Add("ActualStock", typeof(string));
                bt.Columns.Add("Bookpdf", typeof(string));


                foreach (var data in result)
                {
                    bt.Rows.Add(
                        data["Bookid"].ToString(),
                        data["bookname"].ToString(),
                        data["Authorname"].ToString(),
                        data["Pages"].ToString(),
                        data["bookcost"].ToString(),
                        data["Edition"].ToString(),
                        data["Description"].ToString(),
                        data["bookadres"].ToString(),
                        data["genres"].ToString(),
                        data["Language"].ToString(),
                        data["Publishername"].ToString(),
                        data["PublishDate"].ToString(),
                        data["ActualStock"].ToString(),
                        data["bookpdf"].ToString()

                    );
                }

                GridView3.DataSource = bt;
                GridView3.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
       protected void Button2_Click(object sender, EventArgs e)
       {
            // Get the button that was clicked
           
            Button btn = (Button)sender;
            
                string pdfU = btn.CommandArgument
                ;

                // Redirect to the PDF file URL
                Response.Redirect(pdfU);
                // Download the PDF file for the book with the specified bookId
            
            // Get the PDF file URL from the button's Text property
            
       }

    }

}