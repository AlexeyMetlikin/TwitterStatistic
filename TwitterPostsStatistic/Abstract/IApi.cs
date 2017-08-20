using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterPostsStatistic.Abstract
{
    public interface IApi
    {
        void SendPostRequest(string resource_url, string post_data, string auth_header);
    }
}
