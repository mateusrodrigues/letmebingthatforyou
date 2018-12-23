using Lmbtfy.Web.Extensions;
using Lmbtfy.Web.Models;
using Lmbtfy.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Globalization;
using System.IO;
using System.Net;

namespace Lmbtfy.Web.Controllers
{
    public class HomeController : Controller
    {
        public ImageMetadata _imageOfTheDay;

        public HomeController(ImageRepository repository)
        {
            _imageOfTheDay = repository.GetImages().GetElementOfTheDay(DateTime.Now);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Index(string q)
        {
            if (string.IsNullOrEmpty(q)) {
                return View(_imageOfTheDay);
            }
            ViewBag.Question = q;
            return View("BingThis", _imageOfTheDay);
        }

        public ActionResult GenerateUrl(string q)
        {
            string virtualPath = Url.RouteUrl("Search", new { q, Controller = "Home", Action = "Index" });
            string url = Url.ToPublicUrl(HttpContext, virtualPath);
            string tinyUrl = GenerateTinyUrl(url);

            return PartialView("_GenerateUrl", new GeneratedUrlModel { Url = url, TinyUrl = tinyUrl });
        }

        protected string GenerateTinyUrl(string realUrl)
        {
            // prepare the web page we will be asking for
            var request = (HttpWebRequest)WebRequest.Create("http://tinyurl.com/api-create.php?url=" + realUrl);

            // execute the request
            request.AllowAutoRedirect = false;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

        public ActionResult BackgroundImageCss()
        {
            var imageUrl = _imageOfTheDay.ImageUrl;

            var imageMetadata = ImageMetadata.GetImageMetadata(imageUrl);
            string imageCss = @"#bgDiv {{ BACKGROUND-IMAGE: url({0}); BACKGROUND-REPEAT: no-repeat; }}
#bgDivFull {{ BACKGROUND-IMAGE: url({0}); BACKGROUND-REPEAT: no-repeat; }}";
            imageCss = String.Format(CultureInfo.InvariantCulture, imageCss, imageMetadata.ImageUrl);
            return Content(imageCss, "text/css");
        }
    }

    public static class Helpers
    {
        public static string ToPublicUrl(this IUrlHelper urlHelper, HttpContext httpContext, string relativeUri)
        {
            var uriBuilder = new UriBuilder
            {
                Host = httpContext.Request.PathBase,
                Path = "/",
                Port = 80,
                Scheme = "http",
            };

            //if (httpContext.Request.IsLocal)
            //{
            //    uriBuilder.Port = httpContext.Request.Url.Port;
            //}

            return new Uri(uriBuilder.Uri, relativeUri).AbsoluteUri;
        }
    }
}
