using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Common.Interfaces;

namespace UrlShortener.Services.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private static string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static int BASE = 62;

        public string LinkToShortLink(string link)
        {
            int num = 0;

            for (int i = 0, len = link.Length; i < len; i++)
            {
                num = num * BASE + ALPHABET.IndexOf(link[(i)]);
            }

            return num.ToString();
        }
    }
}
