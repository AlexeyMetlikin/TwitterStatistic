using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterPostsStatistic.Abstract
{
    public interface ITwitterApi : IApi
    {
        string oauthToken { private get; set; }
        string oauthTokenSecret { private get; set; } 
    }
}
