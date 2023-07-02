using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppl.Models;
using System.IO;

namespace WebAppl

{
    public partial class WebForm10 : System.Web.UI.Page

    {




        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
                Drop2();
                Drop3();
                //blank();
            }
            //blank();

        }
        //Add button
        protected async void Button4_Click(object sender, EventArgs e)
        {


            try
            {
                var mongoclient = new MongoClient(strcon);
                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<Book>("book");

                var filter = Builders<Book>.Filter.Eq("Bookid", TextBox1.Text);
                var result = collection.Find(filter).ToList();


                //string Genres = "";
                //foreach (int i in ListBox1.GetSelectedIndices())
                //{
                //    Genres = Genres + ListBox1.Items[i] + ",";
                //}
                //// genres = Adventure,Self Help,
                //Genres = Genres.Remove(Genres.Length - 1);

                string flocation = "~/titelimage/.jpg";
                string pathstring = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string directoryPath = Server.MapPath("~/titelimage/");
                string filePath = Path.Combine(directoryPath, pathstring);
                FileUpload1.SaveAs(filePath);
                flocation = "~/titelimage/" + pathstring;

                string file = "~/bookpd/.pdf";
                string path = Path.GetFileName(FileUpload2.PostedFile.FileName);
                string directory = Server.MapPath("~/bookpd/");
                string filepath2 = Path.Combine(directory, path);
                FileUpload2.SaveAs(filepath2);
                file = "~/bookpd/" + path;

                if (result.Count == 0)
                {


                    Book book = new Book
                    {
                        BookId = TextBox1.Text.Trim(),
                        BookName = TextBox2.Text.Trim(),
                        Description = TextBox9.Text.Trim(),
                        Edition = TextBox10.Text.Trim(),
                        PublisherName = DropDownList2.SelectedValue.Trim(),
                        AuthorName = DropDownList3.SelectedValue.Trim(),
                        Genres = ListBox1.SelectedValue.Trim(),
                        Language = DropDownList1.SelectedValue.Trim(),
                        PublishDate = TextBox5.Text.Trim(),
                        bookpdf = flocation,
                        BookCost = int.Parse(TextBox3.Text.Trim()),
                        bookpd = file
                    };

                    //int bookCost;
                    //if (!int.TryParse(TextBox3.Text, out bookCost))
                    //{
                    //    throw new Exception("Invalid book cost. Please enter a valid value.");
                    //}
                    //book.BookCost = bookCost;

                    int pages;
                    if (!int.TryParse(TextBox4.Text, out pages))
                    {
                        throw new Exception("Invalid number of pages. Please enter a valid  value.");
                    }
                    book.Pages = pages;




                    int actualStock;
                    if (!int.TryParse(TextBox6.Text, out actualStock))
                    {
                        throw new Exception("Invalid actual stock value. Please enter a valid  value.");
                    }
                    book.ActualStock = actualStock;




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


        //Go button
        protected async void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                string bookId = TextBox1.Text.Trim();

                if (string.IsNullOrEmpty(bookId))
                {
                    // Display an error message if the book ID is empty or null.
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a book ID.')", true);
                    return;
                }

                // Connect to the MongoDB database and retrieve the "book" collection.
                var mongoClient = new MongoClient(strcon);
                var database = mongoClient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<BsonDocument>("book");

                // Find the book with the specified book ID.
                var filter = Builders<BsonDocument>.Filter.Eq("Bookid", bookId);
                var book = collection.Find(filter).FirstOrDefault();

                if (book != null)
                {
                    // Populate the form fields with data from the retrieved book document.
                    TextBox2.Text = book.GetValue("bookname").ToString();
                    TextBox3.Text = book.GetValue("bookcost").ToString();
                    ListBox1.SelectedValue = book.GetValue("genres").ToString();
                    DropDownList1.SelectedValue = book.GetValue("Language").ToString();
                    DropDownList2.SelectedValue = book.GetValue("Publishername").ToString();
                    DropDownList3.SelectedValue = book.GetValue("Authorname").ToString();
                    TextBox5.Text = book.GetValue("PublishDate").ToString();
                    TextBox6.Text = book.GetValue("ActualStock").ToString();

                    TextBox9.Text = book.GetValue("Description").ToString();
                    TextBox10.Text = book.GetValue("Edition").ToString();
                    TextBox7.Text = book.GetValue("Pages").ToString();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Found.')", true);
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






        //Update button
        protected void Button5_Click(object sender, EventArgs e)
        {

            try
            {
                var mongoclient = new MongoClient(strcon);
                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<Book>("book");

                // Find the book document to be updated
                var filter = Builders<Book>.Filter.Eq("Bookid", TextBox1.Text);
                var result = collection.Find(filter).ToList();
                if (result.Count > 0)
                {



                    string flocation = "~/titelimage/.jpg";
                    string pathstring = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string directoryPath = Server.MapPath("~/titelimage/");
                    string filePath = Path.Combine(directoryPath, pathstring);
                    FileUpload1.SaveAs(filePath);
                    flocation = "~/titelimage/" + pathstring;
                    string file = "~/bookpd/.pdf";
                    string path = Path.GetFileName(FileUpload2.PostedFile.FileName);
                    string directory = Server.MapPath("~/bookpd/");
                    string filepath2 = Path.Combine(directory, path);
                    FileUpload2.SaveAs(filepath2);
                    file = "~/bookpd/" + path;
                    // Update the book document with the new values
                    var book = result[0];
                    book.BookName = TextBox2.Text;
                    book.BookCost = int.Parse(TextBox3.Text);
                    book.Pages = int.Parse(TextBox4.Text);
                    book.PublishDate = TextBox5.Text;
                    book.ActualStock = int.Parse(TextBox6.Text);
                    book.Description = TextBox9.Text;
                    book.Edition = TextBox10.Text;
                    book.PublisherName = DropDownList2.SelectedValue;
                    book.AuthorName = DropDownList3.SelectedValue;
                    book.Genres = ListBox1.SelectedValue;
                    book.Language = DropDownList1.SelectedValue;
                    book.bookpdf = flocation;
                    book.bookpd = file;



                    // Update the document in the collection
                    var updateResult = collection.ReplaceOne(filter, book);
                    BindGrid();

                    if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Book updated successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to update book')", true);
                    }
                }



                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Book not found')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }




        }
        //Delete button
        protected async void Button6_Click(object sender, EventArgs e)
        {
           
            BindGrid();

            try
            {
                var mongoclient = new MongoClient(strcon);
                var database = mongoclient.GetDatabase("ELibraryManagment");
                var collection = database.GetCollection<Book>("book");

                // Find the book document to be deleted
                var filter = Builders<Book>.Filter.Eq("Bookid", TextBox1.Text);
                var result = collection.Find(filter).ToList();

                if (result.Count > 0)
                {
                    // Delete the book document from the collection
                    var deleteResult = collection.DeleteOne(filter);

                    BindGrid();
                    if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Book deleted successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to delete book')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Book not found')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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

                GridView2.DataSource = bt;
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }


        private void Drop2()
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");
                var collection = db.GetCollection<BsonDocument>("author managment");
                var filter = Builders<BsonDocument>.Filter.Empty;
                var document = collection.Find(filter).ToList();
                List<string> List = new List<string>();

                foreach (var item in document)
                {
                    List.Add(item["Authorname"].ToString());

                }

                DropDownList3.DataSource = List;
                DropDownList3.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }

        }
        private void Drop3()
        {
            try
            {
                var mongoclient = new MongoClient(strcon);
                var db = mongoclient.GetDatabase("ELibraryManagment");
                var collection = db.GetCollection<BsonDocument>("publisher managment");
                var filter = Builders<BsonDocument>.Filter.Empty;
                var document = collection.Find(filter).ToList();

                List<string> PublishernameList = new List<string>();
                foreach (var item in document)
                {

                    PublishernameList.Add(item["Publishername"].ToString());
                }

                DropDownList2.DataSource = PublishernameList;
                DropDownList2.DataBind();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }




        }


        void blank()
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Get the button that was clicked
            Button btn = (Button)sender;

            // Get the PDF file URL from the button's Text property
            string pdfUrl = btn.CommandArgument;

            // Redirect to the PDF file URL
            Response.Redirect(pdfUrl);
        }





    }
}

