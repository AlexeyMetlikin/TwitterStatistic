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

        private static IniFile config = new IniFile("settings.ini");

        public TwitterStatisctic(ITwitterApi API)
        {
            this.API = API;
            ReadConsumersKeys();
        }

        private void ReadConsumersKeys()
        {
            try
            {
                API.ConsumerKey = config.ReadINI("API-Key", "ConsumerKey");
                API.ConsumerSecret = config.ReadINI("API-Key", "ConsumerSecret");
            }
            catch
            {
                throw new Exception("Отсутствует файл настроек settings.ini либо в файле настроек отсутствуют ключи - 'Consumer Key' и 'Consumer Secret'");
            }
        }

        public void GetUserPIN()
        {
            string request_url = "http://api.twitter.com/oauth/authorize?oauth_token=6ayhlAHbToGIy9qwmUJDeseqnbrNBdj4DXlLuYqqYrs0T";
            System.Diagnostics.Process.Start(request_url); // Передаём ссылку на страницу браузеру по умолчанию и ждём пока пользователь введёт PIN-код
            string oauth_verifier = Console.ReadLine(); // oauth_verifier — это полученный нами PIN-код.
        }

        public void GetToken()
        {
            API.SendGetRequestAsync();
        }
    }
}
