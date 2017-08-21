using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using TwitterPostsStatistic.Abstract;
using OAuth;

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
            ConsumerKey = "MUixq6JoFMljSJvMngSjfXVy7";
            ConsumerSecret = "rtK3bMFj4Vazd9dVp1qmsT9LoVnQ16WCm17bQvHEzhDz7tdNiM";
            OauthToken = "898408814252236800-taHgdPecPXVC7yKyOoKW6xieJ4jhvbE";
            OauthTokenSecret = "kezqBUJU6HMxVMfW55e1CazRASUlismsSWX9sMdhiSoJV";
        }

        public void SendGetRequest()
        {
            string resource_url = "https://api.twitter.com/oauth2/token?grant_type=client_credentials";

            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";
            var oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow - new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                             "&oauth_timestamp={3}&oauthToken={4}&oauth_version={5}";

            var baseString = string.Format(baseFormat,
                                           ConsumerKey,
                                           oauth_nonce,
                                           oauth_signature_method,
                                           oauth_timestamp,
                                           OauthToken,
                                           oauth_version);

            baseString = string.Concat("POST&", Uri.EscapeDataString(resource_url),
                         "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(ConsumerKey), ":", Uri.EscapeDataString(ConsumerSecret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(Encoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(baseString)));
            }

            var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                               "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                               "oauth_signature=\"{4}\", " +
                               "oauth_version=\"{5}\"";

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(ConsumerKey),
                                    Uri.EscapeDataString("TVVpeHE2Sm9GTWxqU0p2TW5nU2pmWFZ5NyZydEszYk1GajRWYXpkOWRWcDFxbXNUOUxvVm5RMTZXQ20xN2JRdkhFemhEejd0ZE5pTQ=="),
                                    Uri.EscapeDataString(oauth_version)
                            );

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.twitter.com/oauth2/token?grant_type=client_credentials");
            request.Headers.Add("Authorization", "TVVpeHE2Sm9GTWxqU0p2TW5nU2pmWFZ5NyZydEszYk1GajRWYXpkOWRWcDFxbXNUOUxvVm5RMTZXQ20xN2JRdkhFemhEejd0ZE5pTQ==");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";


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
