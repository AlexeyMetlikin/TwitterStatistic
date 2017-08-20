using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterPostsStatistic.Abstract;
using TwitterPostsStatistic.Settings;

namespace TwitterPostsStatistic
{
    public class TwitterStatisctic
    {
        private ITwitterApi API;

        public TwitterStatisctic(ITwitterApi API)
        {
            try
            {
                this.API = API;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void StartApp()
        {
            Console.WriteLine("123");
        }

        public void GetUserPIN()
        {
            string request_url = "http://api.twitter.com/oauth/authorize?oauth_token=" + API.OauthToken;
            API.SendPostRequest(request_url, "", "");
            System.Diagnostics.Process.Start(request_url); // Передаём ссылку на страницу браузеру по умолчанию и ждём пока пользователь введёт PIN-код
            string oauth_verifier = Console.ReadLine(); // oauth_verifier — это полученный нами PIN-код.
        }
    }
}
