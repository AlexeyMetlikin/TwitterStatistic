using System;
using TwitterPostsStatistic.Entities;
using TwitterPostsStatistic.Settings;

namespace TwitterPostsStatistic
{
    class Program
    {
        private static IniFile config = new IniFile("settings.ini");

        private static string consumerKey;
        private static string consumerSecret;

        static void Main(string[] args)
        {
            ReadConsumersKeys();
            TwitterStatisctic statistic = new TwitterStatisctic(new TwitterApi(consumerKey, consumerSecret));
            statistic.StartApp();
        }

        private static void ReadConsumersKeys()
        {
            consumerKey = null;
            consumerSecret = null;
            try
            {
                consumerKey = config.ReadINI("API-Key", "ConsumerKey");
                consumerSecret = config.ReadINI("API-Key", "ConsumerSecret");
            }
            catch
            {
                throw new Exception("Отсутствует файл настроек settings.ini либо в файле настроек отсутствуют ключи - 'Consumer Key' и 'Consumer Secret'");
            }
        }
    }
}
