using System;
using TwitterPostsStatistic.Entities;
using TwitterPostsStatistic.Settings;

namespace TwitterPostsStatistic
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterStatisctic statistic = new TwitterStatisctic(new TwitterApi());
            MainFunc(statistic);
        }

        private static void MainFunc(TwitterStatisctic statisctic)
        {
            Console.WriteLine("1. Получить статистику частотности");
            Console.WriteLine("2. Напечатать твит");
            Console.WriteLine("3. Exit");
            int control;
            int.TryParse(Console.ReadLine(), out control);
            while (control != 3)
            {
                switch (control)
                {
                    case 1:
                        statisctic.GetToken();
                        break;
                    case 2:
                        statisctic.GetUserPIN();
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
                int.TryParse(Console.ReadLine(), out control);
            }
        }
    }
}
