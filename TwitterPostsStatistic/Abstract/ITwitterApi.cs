using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterPostsStatistic.Abstract
{
    public interface ITwitterApi : IApi
    {
        string ConsumerKey { get; set; }
        string ConsumerSecret { get; set; }
        string OauthToken { get; set; }
        string OauthTokenSecret { get; set; }

        void SendGetRequest();
    }
}

