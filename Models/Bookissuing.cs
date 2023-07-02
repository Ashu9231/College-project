using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace WebAppl.Models
{
    public class Bookissuing
    {

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("bookid")]
       
        public string bookid { get; set; }

        [BsonElement("userid")]
        public string userid { get; set; }

        [BsonElement("bookname")]
        
        public string bookname { get; set; }

        [BsonElement("fullname")]
        public string fullname { get; set; }

        [BsonElement("enddate")]
        public string enddate { get; set; }

        [BsonElement("startdate")]
        public string startdate { get; set; }


        [BsonElement("accountstatus")]
        public string accountstatus { get; set; }


    }
}