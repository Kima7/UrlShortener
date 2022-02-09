using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Common.Interfaces;
using UrlShortener.Common.Repository;
using UrlShortener.Model;

namespace UrlShortener.Services.Services
{
    public class UrlService : IUrlService
    {
        private IUrlRepository _urlRepository;
        private IUrlShortenerService _urlShortenerService;

        public UrlService(IUrlRepository urlRepository, IUrlShortenerService urlShortenerService)
        {
            _urlRepository = urlRepository;
            _urlShortenerService = urlShortenerService;
        }

        public void Register(string userName)
        {
            var user = new User() { UserName = userName };
            _urlRepository.Register(user);
        }

        public void SaveLink(string link,string userName)
        {
            if (!_urlRepository.LinkExists(link))
            {
                var shortLink = _urlShortenerService.LinkToShortLink(link);
                var linkk = new Link() { Link1 = link, ShortLink = shortLink };
                _urlRepository.SaveLink(linkk, userName);

            }
            else
            {
                if(!_urlRepository.UserLinkExists(link,userName))
                {
                    _urlRepository.SaveUserLink(link, userName);
                }
            }
            
        }

        public bool DeleteLink(string link, string userName)
        {
            bool result;
            if(_urlRepository.UserLinkExists(link,userName))
            {
                _urlRepository.DeleteLink(link, userName);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public string RedirectPath(string shortLink)
        {
            string link;
            try
            {
                link = _urlRepository.RedirectPath(shortLink);
            }
            catch
            {
                throw;
            }
            return link;
        }
    }
}
