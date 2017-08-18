using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TwitterPostsStatistic
{
    public class Message
    {
        /*private const string headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
              "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
              "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
              "oauth_version=\"{6}\"";

        

        /// <summary>
        /// В конструктор передаем сохраненные ключи
        /// </summary>
        public Message(string consumer_key, string consumer_secret, string oauth_token, string oauth_token_secret)
        {
            this.consumerKey = consumer_key;
            this.consumerSecret = consumer_secret;
            this.oauthToken = oauth_token;
            this.oauthTokenSecret = oauth_token_secret;
        }

        /// <summary>
        /// Отправляем личное сообщение пользователю
        /// </summary>
        /// <param name="user">without @</param>
        /// <param name="text"></param>
        public void SendDirectMessage(string user, string text)
        {
            string post_data;
            string resource_url;

            string authHeader = GetPostDirectMessageBaseString(text, user, oauthToken, oauthTokenSecret, out post_data, out resource_url);
            Send(resource_url, post_data, authHeader);
        }

        /// <summary>
        /// Публикуем на ленту
        /// </summary>
        /// <param name="text"></param>
        public void SendTwit(string text)
        {
            string post_data;
            string resource_url;

            string authHeader = GetStatusBaseString(text, oauthToken, oauthTokenSecret, out post_data, out resource_url);
            Send(resource_url, post_data, authHeader);
        }

        private string GetBaseString(string oauth_token, string oauth_token_secret, string post_data, string resource_url)
        {
            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";
            var oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&" + post_data;

            var baseString = string.Format(baseFormat,
                                        consumerKey,
                                        oauth_nonce,
                                        oauth_signature_method,
                                        oauth_timestamp,
                                        oauth_token,
                                        oauth_version
                                        );

            baseString = string.Concat("POST&", Uri.EscapeDataString(resource_url),
                         "&", Uri.EscapeDataString(baseString));

            //Encrypt data
            var compositeKey = string.Concat(Uri.EscapeDataString(consumerSecret),
                        "&", Uri.EscapeDataString(oauth_token_secret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(
                    hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            //Finish Auth header
            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(consumerKey),
                                    Uri.EscapeDataString(oauth_token),
                                    Uri.EscapeDataString(oauth_signature),
                                    Uri.EscapeDataString(oauth_version)
                            );

            return authHeader;
        }

        private string GetStatusBaseString(string status, string oauth_token, string oauth_token_secret, out string post_data, out string resource_url)
        {
            post_data = "status=" + Uri.EscapeDataString(status);
            resource_url = "https://api.twitter.com/1.1/statuses/update.json";

            return GetBaseString(oauth_token, oauth_token_secret, post_data, resource_url);
        }

        private string GetPostDirectMessageBaseString(string text, string screen_name, string oauth_token, string oauth_token_secret, out string post_data, out string resource_url)
        {
            post_data = "screen_name=" + Uri.EscapeDataString(screen_name) + "&text=" + Uri.EscapeDataString(text);
            resource_url = "https://api.twitter.com/1.1/direct_messages/new.json";

            return GetBaseString(oauth_token, oauth_token_secret, post_data, resource_url);
        }*/
    }
}
