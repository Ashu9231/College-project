using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppl.Models
{
    public class user
    {
        [BsonElement("FullName")]

        public string FullName { get; set; }

        [BsonElement("DateOfBirth")]


        public string DateOfBirth { get; set; }

        [BsonElement("EmailID")]
        public string EmailID { get; set; }

        [BsonElement("ContactNumber")]

        public Int64 ContactNumber { get; set; }

        [BsonElement("State")]
        public string State { get; set; }

        [BsonElement("CityName")]
        public string CityName { get; set; }

        [BsonElement("PinCode")]
        public Int64 PinCode { get; set; }

        [BsonElement("FullAddress")]
        public string FullAddress { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Accountstatus")]
        public string Accountstatus { get; set; }
    }
}