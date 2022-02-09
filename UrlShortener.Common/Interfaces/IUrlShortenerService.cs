using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Common.Interfaces
{
    public interface IUrlShortenerService
    {
        string LinkToShortLink(string link);
    }
}
