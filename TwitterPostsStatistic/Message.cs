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
        /**/



        /*// Создадим три переменные и присвоим им значения данных полученных на сайте твиттера:
        */


        /*2 
         request_url = "http://api.twitter.com/oauth/authorize?oauth_token="+ oauth_token;
Console.WriteLine("Req: " + request_url);
Console.WriteLine("--------------------------------------------------------");
System.Diagnostics.Process.Start(request_url); // Передаём ссылку на страницу браузеру по умолчанию и ждём пока пользователь введёт PIN-код
Console.Write("Enter PIN: ");
string oauth_verifier = Console.ReadLine(); // oauth_verifier — это полученный нами PIN-код.
Console.WriteLine("--------------------------------------------------------");
         */

        /*3
         request_url = 
  "http://api.twitter.com/oauth/access_token"+"?" +
  "oauth_consumer_key=" + consumerKey + "&" +
  "oauth_token=" + oauth_token + "&" +
  "oauth_signature_method=" + "HMAC-SHA1" + "&" +
  "oauth_signature=" + sig + "&" +
  "oauth_timestamp=" + timeStamp + "&" +
  "oauth_nonce=" + nonce + "&" +
  "oauth_version=" + "1.0" + "&" +
  "oauth_verifier=" + oauth_verifier;
Console.WriteLine("Req: " + request_url);
Console.WriteLine("--------------------------------------------------------"); 
// Запрос на сервер
Request = (HttpWebRequest) HttpWebRequest.Create(request_url);
Response = (HttpWebResponse)Request.GetResponse();  
Reader = new StreamReader(Response.GetResponseStream(), Encoding.GetEncoding(1251));
outline = Reader.ReadToEnd();  
Console.WriteLine("Out: " + outline);
Console.WriteLine("--------------------------------------------------------");
// Разбор выданной строки и присвоение значений соответствующим переменным
words = outline.Split(delimiterChars);
oauth_token = words[1]; 
oauth_token_secret = words[3];
string user_id = words[5];
string screen_name = words[7];
// Вывод полученных данных  
Console.WriteLine("oauth_token = " + oauth_token);  
Console.WriteLine("oauth_token_secret = " + oauth_token_secret);  
Console.WriteLine("user_id = " + user_id);
Console.WriteLine("screen_name = " + screen_name); 
         */
    }
}
