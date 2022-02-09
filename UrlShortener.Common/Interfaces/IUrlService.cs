using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Common.Interfaces
{
    public interface IUrlService
    {
        void Register(string userName);

        void SaveLink(string link, string userName);

        bool DeleteLink(string link, string userName);

        //int CountLink(string link);

        string RedirectPath(string shortLink);

    }
}
