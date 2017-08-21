using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using TwitterPostsStatistic.Abstract;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace TwitterPostsStatistic.Entities
{
    public class TwitterApi : ITwitterApi
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public string OauthToken { get; set; }
        public string OauthTokenSecret { get; set; }
        
        public TwitterApi()
        {
            ConsumerKey = "Iy5egv3JqJTxFu6DQVrMZEkLc";
            ConsumerSecret = "5JxVltixFGUlni9Doy4d2YatOOdt4QuLCgLJfaTG05g1OFsY4v";
        }

        public async void SendGetRequestAsync()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "	https://api.twitter.com/oauth/request_token");

            string oauth_consumer_key = "Iy5egv3JqJTxFu6DQVrMZEkLc";
            string oauth_consumer_secret = "5JxVltixFGUlni9Doy4d2YatOOdt4QuLCgLJfaTG05g1OFsY4v";

            string url = "https://api.twitter.com/oauth2/token?oauth_consumer_key=" + oauth_consumer_key + "&oauth_consumer_secret=" + oauth_consumer_secret;

            var customerInfo = Convert.ToBase64String(new UTF8Encoding()
                                .GetBytes(oauth_consumer_key + ":" + oauth_consumer_secret));

            // Add authorization to headers
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8,
                                                                "application/x-www-form-urlencoded");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;

            string json = await response.Content.ReadAsStringAsync();
            Regex reg = new Regex("\"access_token\":");
            OauthToken = reg.Split(json)[1].Replace("\"", "").Replace("}", "");
        }

        public void SendPostRequest(string resource_url, string post_data, string auth_header)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
            request.Headers.Add("Authorization", auth_header);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = post_data.Length;

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
                Console.WriteLine(response.ToString());
            }
            catch (WebException e)
            {
                throw new Exception(e.Status.ToString());
            }
        }

        public void SendTwit(string text)
        {
            string post_data;
            string resource_url;

            string authHeader = GetStatusBaseString(text, out post_data, out resource_url);
            SendPostRequest(resource_url, post_data, authHeader);
        }

        private string GetStatusBaseString(string status, out string post_data, out string resource_url)
        {
            post_data = "status=" + Uri.EscapeDataString(status);
            resource_url = "https://api.twitter.com/1.1/statuses/update.json";

            return GetBaseString(post_data, resource_url);
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
                                           ConsumerKey,
                                           oauth_nonce,
                                           oauth_signature_method,
                                           oauth_timestamp,
                                           OauthToken,
                                           oauth_version);

            baseString = string.Concat("POST&", Uri.EscapeDataString(resource_url),
                         "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(ConsumerSecret),
                        "&", Uri.EscapeDataString(OauthTokenSecret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(Encoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(baseString)));
            }

            var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                               "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                               "oauthToken=\"{4}\", oauth_signature=\"{5}\", " +
                               "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(ConsumerKey),
                                    Uri.EscapeDataString(OauthToken),
                                    Uri.EscapeDataString(oauth_signature),
                                    Uri.EscapeDataString(oauth_version)
                            );

            return authHeader;
        }
    }
}
