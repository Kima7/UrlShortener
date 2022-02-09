using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Common.Repository;
using UrlShortener.Model;

namespace UrlShortener.Services.Repository
{
    public class UrlRepository : IUrlRepository
    {

        public void Register(User user)
        {
            using (var dbContext = new UrlShortenerDBEntities())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
        }

        public bool LinkExists(string link)
        {
            bool exists = false;
            using (var dbContext = new UrlShortenerDBEntities())
            {
                if (dbContext.Links.FirstOrDefault(l => l.Link1.Equals(link)) != null)
                    exists = true;
            }
            return exists;
        }

        public bool UserLinkExists(string link, string userName)
        {
            bool exists = false;
            using (var dbContext = new UrlShortenerDBEntities())
            {
                var user = dbContext.Users.FirstOrDefault(s => s.UserName.Equals(userName));
                var linkk = dbContext.Links.FirstOrDefault(l => l.Link1.Equals(link));

                if (dbContext.UserLinks.FirstOrDefault(l => l.LinkId == linkk.Id && l.UserId == user.Id) != null)
                    exists = true;
            }
            return exists;
        }

        public void SaveLink(Link link, string userName)
        {
            using (var dbContext = new UrlShortenerDBEntities())
            {
                dbContext.Links.Add(link);

                var user = dbContext.Users.FirstOrDefault(s => s.UserName.Equals(userName));
                var userLink = new UserLink() { LinkId = link.Id, UserId = user.Id };
                dbContext.UserLinks.Add(userLink);
                dbContext.SaveChanges();
            }
        }


        public void SaveUserLink(string link, string userName)
        {
            using (var dbContext = new UrlShortenerDBEntities())
            {
                var linkk = dbContext.Links.FirstOrDefault(l => l.Link1.Equals(link));
                var user = dbContext.Users.FirstOrDefault(s => s.UserName.Equals(userName));
                var userLink = new UserLink() { LinkId = linkk.Id, UserId = user.Id };
                dbContext.UserLinks.Add(userLink);
                dbContext.SaveChanges();
            }
        }

        public void DeleteLink(string link, string userName)
        {
            using (var dbContext = new UrlShortenerDBEntities())
            {
                var linkk = dbContext.Links.FirstOrDefault(l => l.Link1.Equals(link));
                var user = dbContext.Users.FirstOrDefault(s => s.UserName.Equals(userName));
                var userLink = dbContext.UserLinks.FirstOrDefault(l => l.LinkId == linkk.Id && l.UserId == user.Id);
                dbContext.UserLinks.Remove(userLink);
                dbContext.SaveChanges();
            }
        }

        public string RedirectPath(string shortLink)
        {
            string link = "";
            using (var dbContext = new UrlShortenerDBEntities())
            {
                if(dbContext.Links.Any(l => l.ShortLink.Equals(shortLink)))
                {
                    link = dbContext.Links.FirstOrDefault(l => l.ShortLink.Equals(shortLink)).Link1;
                }
            }
            return link;
        }
    }
}
