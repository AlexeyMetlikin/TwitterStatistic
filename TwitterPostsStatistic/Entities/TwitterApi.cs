using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TwitterPostsStatistic.Abstract;

namespace TwitterPostsStatistic.Entities
{
    public class TwitterApi : ITwitterApi
    {
        private const string headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
              "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
              "oauthToken=\"{4}\", oauth_signature=\"{5}\", " +
              "oauth_version=\"{6}\"";

        private readonly string consumerKey;
        private readonly string consumerSecret;

        private string oauthToken;
        private string oauthTokenSecret;

        public string OauthToken
        {
            set { oauthToken = value; }
        }

        public string OauthTokenSecret
        {
            set { oauthTokenSecret = value; }
        }

        public TwitterApi(string consumerKey, string consumerSecret)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
        }

        public void SendGetRequest()
        {
            Uri uri = new Uri("http://api.twitter.com/oauth/request_token");
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
                byte[] content = Encoding.ASCII.GetBytes(post_data);
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

        public void SendDirectMessage(string user, string text)
        {
            string post_data;
            string resource_url;

            string authHeader = GetPostDirectMessageBaseString(text, user, out post_data, out resource_url);
            SendPostRequest(resource_url, post_data, authHeader);
        }

        public void SendTwit(string text)
        {
            string post_data;
            string resource_url;

            string authHeader = GetStatusBaseString(text, out post_data, out resource_url);
            SendPostRequest(resource_url, post_data, authHeader);
        }

        private string GetBaseString(string post_data, string resource_url)
        {
            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";
            var oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                "&oauth_timestamp={3}&oauthToken={4}&oauth_version={5}&" + post_data;

            var baseString = string.Format(baseFormat,
                                        consumerKey,
                                        oauth_nonce,
                                        oauth_signature_method,
                                        oauth_timestamp,
                                        oauthToken,
                                        oauth_version
                                        );

            baseString = string.Concat("POST&", Uri.EscapeDataString(resource_url),
                         "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(consumerSecret),
                        "&", Uri.EscapeDataString(oauthTokenSecret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(Encoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(baseString)));
            }

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(consumerKey),
                                    Uri.EscapeDataString(oauthToken),
                                    Uri.EscapeDataString(oauth_signature),
                                    Uri.EscapeDataString(oauth_version)
                            );

            return authHeader;
        }

        private string GetStatusBaseString(string status, out string post_data, out string resource_url)
        {
            post_data = "status=" + Uri.EscapeDataString(status);
            resource_url = "https://api.twitter.com/1.1/statuses/update.json";

            return GetBaseString(post_data, resource_url);
        }

        private string GetPostDirectMessageBaseString(string text, string screen_name, out string post_data, out string resource_url)
        {
            post_data = "screen_name=" + Uri.EscapeDataString(screen_name) + "&text=" + Uri.EscapeDataString(text);
            resource_url = "https://api.twitter.com/1.1/direct_messages/new.json";

            return GetBaseString(post_data, resource_url);
        }
    }
}
