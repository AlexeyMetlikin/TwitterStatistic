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
        private IApi API;

        public TwitterStatisctic(IApi API)
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
    }
}
