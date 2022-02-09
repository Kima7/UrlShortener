using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UrlShortener.Common.Interfaces;

namespace UrlShortener.WebApp.Controllers
{
    [Authorize]
    public class UrlController : ApiController
    {
        private IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }


        [Route("SaveLink")]
        [HttpPost]
        public IHttpActionResult SaveLink(string link)
        {
            try
            {
                Uri uri;
                bool result = Uri.TryCreate(link, UriKind.Absolute, out uri);

                if (result)
                {
                    _urlService.SaveLink(link, User.Identity.Name);
                }
                else
                {
                    return BadRequest("Wrong url format.");
                }
                return Ok();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("DeleteLink")]
        [HttpGet]
        public IHttpActionResult DeleteLink(string link)
        {
            bool result;
            try
            {
                result = _urlService.DeleteLink(link, User.Identity.Name);
                if (result)
                    return Ok("Link deleted successful.");
                else
                    return BadRequest("You dont have this link saved before.");

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /*[Route("CountLink")]
        [HttpGet]
        public IHttpActionResult CountLink(string link)
        {
            bool result;
            try
            {
                result = _urlService.DeleteLink(link, User.Identity.Name);
                if (result)
                    return Ok("Link deleted successful.");
                else
                    return BadRequest("You dont have this link saved before.");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }*/

        [Route("RedirectLink")]
        [HttpGet]
        public IHttpActionResult RedirectLink(string shortLink)
        {
            try
            {
                var link = _urlService.RedirectPath(shortLink);
                if (!link.Equals(""))
                    return Redirect(link);
                else
                    return NotFound();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
