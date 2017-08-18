using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterPostsStatistic.Abstract
{
    public interface ITwitterApi : IApi
    {
        string OauthToken { set; }
        string OauthTokenSecret { set; }
    }
}
