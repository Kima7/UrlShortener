using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Model;

namespace UrlShortener.Common.Repository
{
    public interface IUrlRepository
    {
        void Register(User user);

        bool LinkExists(string link);

        bool UserLinkExists(string link, string userName);

        void SaveLink(Link link, string userName);

        void SaveUserLink(string link, string userName);

        void DeleteLink(string link, string userName);

        //int CountLink(string link);

        string RedirectPath(string shortLink);
    }
}
