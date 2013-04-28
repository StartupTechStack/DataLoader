using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ServiceStack.Common.ServiceModel;

namespace StartupTechStack
{
    public class FundingEvent
    {

        [BsonElement("type")]
        public string round_code { get; set; }
         [BsonElement("currency_code")]
        public string raised_currency_code { get; set; }
         [BsonElement("amount")]
        public decimal raised_amount { get; set; }
         [BsonElement("year")]
        public int funded_year { get; set; }
         [BsonElement("month")]
        public int funded_month { get; set; }
         [BsonElement("day")]
        public int funded_day { get; set; }

        [BsonElement("date")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime Date
        {
            get
            {
                return  new DateTime(funded_year, funded_month, funded_day);
            }

        }
    }

    public class Company
    {

        public string Id
        {
            get { return Permalink; }
            set { }
        }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("permalink")]
        public string Permalink { get; set; }

        [BsonElement("url")]
        public string homepage_url { get; set; }


        [BsonElement("category")]
        public string category_code { get; set; }

        [BsonElement("techStack")]
        public List<string> TechStack { get; set; }

        [BsonElement("fundingEvent")]
        public List<FundingEvent> funding_rounds { get; set; }

        [BsonElement("launchDate")]
        public string LaunchDate { get; set; }

        [BsonElement("logoUrl")]
        public string logoUrl { get; set; }

        [BsonIgnore()]
        public string Updated { get; set; }

        public string total_money_raised { get; set; }
    }


}