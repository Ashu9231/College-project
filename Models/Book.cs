using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppl.Models
{
    public class Book
    {
        
            [BsonId]
            public ObjectId Id { get; set; }

            [BsonElement("Bookid")]
            [Required(ErrorMessage = "Book ID is required.")]
            public string BookId { get; set; }

            [BsonElement("bookname")]
            [Required(ErrorMessage = "Book name is required.")]
            public string BookName { get; set; }

            [BsonElement("bookcost")]
            [Required(ErrorMessage = "Book cost is required.")]
            public int BookCost { get; set; }

            [BsonElement("Pages")]
            [Required(ErrorMessage = "Number of pages is required.")]
            public int Pages { get; set; }

            [BsonElement("PublishDate")]
            [Required(ErrorMessage = "Publish date is required.")]
            public string PublishDate { get; set; }

            [BsonElement("ActualStock")]
            [Required(ErrorMessage = "Actual stock is required.")]
            public int ActualStock { get; set; }

          // [BsonElement("CurrentStock")]
          // public int CurrentStock { get; set; }
          //
          // [BsonElement("IssuedBook")]
          // public int IssuedBook { get; set; }

            [BsonElement("Description")]
            [Required(ErrorMessage = "Description is required.")]
            public string Description { get; set; }

            [BsonElement("Edition")]
            [Required(ErrorMessage = "Edition is required.")]
            public string Edition { get; set; }

            [BsonElement("Publishername")]
            [Required(ErrorMessage = "Publisher name is required.")]
            public string PublisherName { get; set; }

            [BsonElement("Authorname")]
            [Required(ErrorMessage = "Author name is required.")]
            public string AuthorName { get; set; }

            [BsonElement("genres")]
            [Required(ErrorMessage = "Genres are required.")]
            public string Genres { get; set; }

            [BsonElement("Language")]
            [Required(ErrorMessage = "Language is required.")]
            public string Language { get; set; }

           [BsonElement("bookadres")]
           // [Required(ErrorMessage = "Book image is required.")]
           public string bookpdf { get; set; }
        [BsonElement("bookpdf")]
        // [Required(ErrorMessage = "Book image is required.")]
        public string bookpd{ get; set; }

    }
}