using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using Lmbtfy.Web.Models;

namespace Lmbtfy.Web.Controllers {
    [HandleError]
    public class HomeController : Controller {
        public ActionResult About() {
            return View();
        }

        public ActionResult Index(string q) {
            if (string.IsNullOrEmpty(q)) {
                return View();
            }
            return View("BingThis", (object)q);
        }

        public ActionResult GenerateUrl(string q) {
            string virtualPath = Url.RouteUrl("Search", new { q, Controller = "Home", Action = "Index" });
            string url = Url.ToPublicUrl(virtualPath);
            string tinyUrl = GenerateTinyUrl(url);

            return PartialView("_GenerateUrl", new GeneratedUrlModel { Url = url, TinyUrl = tinyUrl });
        }

        protected string GenerateTinyUrl(string realUrl) {
            // prepare the web page we will be asking for
            var request = (HttpWebRequest)WebRequest.Create("http://tinyurl.com/api-create.php?url=" + realUrl);

            // execute the request
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream())) {
                return sr.ReadToEnd();
            }
        }
    }

    public static class Helpers {
        public static string ToPublicUrl(this UrlHelper urlHelper, string relativeUri) {
            var httpContext = urlHelper.RequestContext.HttpContext;

            var uriBuilder = new UriBuilder {
                Host = httpContext.Request.Url.Host,
                Path = "/",
                Port = 80,
                Scheme = "http",
            };

            if (httpContext.Request.IsLocal) {
                uriBuilder.Port = httpContext.Request.Url.Port;
            }

            return new Uri(uriBuilder.Uri, relativeUri).AbsoluteUri;
        }
    }
}
