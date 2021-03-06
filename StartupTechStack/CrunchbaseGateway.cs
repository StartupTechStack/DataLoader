﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Text;

namespace StartupTechStack
{
    public class CrunchbaseGateway
    {
        public static string API_KEY = "h2q7g7ry3vx7urvvb92d3j6u";
        public static string CB_URL = "http://api.crunchbase.com/v/1/";
        public const string ASSET_URL = "http://s3.amazonaws.com/crunchbase_prod_assets/";

        public async Task<List<Company>> LoadCompanies(int page)
        {
            // http://api.crunchbase.com/v/1/companies.js?page=0&api_key=h2q7g7ry3vx7urvvb92d3j6u
            var url = "{0}{1}.js?page={2}&api_key={3}".Fmt(CB_URL, "companies", page, API_KEY);

            WebRequest webRequestForUrl = GetWebRequestForUrl(url);

            var responseAsync = await webRequestForUrl.GetResponseAsync();

            var responseString = GetResponseString(responseAsync);
            List<Company> companies = responseString.FromJson<List<Company>>();


            return companies;
        }

        public async Task<Company> FillCompanyDetails(Company aCompany)
        {
            if (aCompany.Permalink.IsNullOrEmpty())
            {
                return null;
            }

            var url = "{0}{1}/{2}.js?api_key={3}".Fmt(CB_URL, "company", aCompany.Permalink, API_KEY);

            WebRequest webRequestForUrl = GetWebRequestForUrl(url);

            try
            {
                var responseAsync = await webRequestForUrl.GetResponseAsync();

                var responseString = GetResponseString(responseAsync);
                Company company = responseString.FromJson<Company>();
                JsonObject jsonObject = JsonObject.Parse(responseString);
                if (null != jsonObject && jsonObject.ContainsKey("image") && jsonObject.Object("image").ContainsKey("available_sizes"))
                {
                    company.logoUrl = ASSET_URL +
                        jsonObject.Object("image").ArrayObjects("available_sizes")[0].ToArray()[1].Key;
                }

                return company;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<string>> SearchCompany(string name)
        {
            if (name.IsNullOrEmpty())
            {
                return null;
            }
            // http://api.crunchbase.com/v/1/search.js?query=Unroll.me&api_key=h2q7g7ry3vx7urvvb92d3j6u
            var url = "{0}{1}.js?query={2}&api_key={3}".Fmt(CB_URL, "search", name.UrlEncode(), API_KEY);

            WebRequest webRequestForUrl = GetWebRequestForUrl(url);

            var responseAsync = await webRequestForUrl.GetResponseAsync();

            var responseString = GetResponseString(responseAsync);
            JsonObject jsonObject = JsonObject.Parse(responseString);

         List<string> permalinks = new List<string>();

            if (jsonObject.ContainsKey("results"))
            {
                var arrayObject = jsonObject.ArrayObjects("results");

                arrayObject.ForEach(o =>
                {
                    var jsonObject1 = o.Child("permalink");

                    permalinks.Add(jsonObject1);

                });
//                company = arrayObject[0].JsonTo<Company>();
            }

            return permalinks;
        }

        private WebRequest GetWebRequestForUrl(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Headers.Add("x-li-format", "json");
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            return webRequest;
        }

        public string GetResponseString(WebResponse webRes)
        {
            string reponseBody;

            using (var stream = webRes.GetResponseStream())
            {
                byte[] readFully = stream.ReadFully();


                reponseBody = readFully.Decompress(CompressionTypes.GZip);
            }

            return reponseBody;
        }
    }
}