using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwitterPostsStatistic.Abstract;

namespace TwitterPostsStatistic.Entities
{
    public class TwitterApi : ITwitterApi
    {
        private readonly string consumerKey;

        private readonly string consumerSecret;

        public string OauthToken { set; }

        public string OauthTokenSecret { set; }

        public TwitterApi(string consumerKey, string consumerSecret)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
        }

        public void SendGetRequest()
        {

        }

        public void SendPostRequest(string resource_url, string post_data, string auth_header)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
            request.Headers.Add("Authorization", auth_header);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = post_data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(post_data);
                stream.Write(content, 0, content.Length);
            }
            try
            {
                WebResponse response = request.GetResponse();
                Console.WriteLine(response.ToString());
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Status.ToString());
            }
        }

        public void SendPostRequest()
        {
            throw new NotImplementedException();
        }
    }
}
