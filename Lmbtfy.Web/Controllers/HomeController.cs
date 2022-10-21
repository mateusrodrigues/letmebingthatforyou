using Lmbtfy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace Lmbtfy.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Index(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return View();
            }

            ViewBag.Question = q;
            return View("BingThis");
        }

        public ActionResult GenerateUrl(string q)
        {
            string path = Url.ActionLink("Index", "Home", new { q }, "https", Request.Host.Value);
            string tinyUrl = GenerateTinyUrl(path);

            return PartialView("_GenerateUrl", new GeneratedUrlModel { Url = path, TinyUrl = tinyUrl });
        }

        protected string GenerateTinyUrl(string realUrl)
        {
            // prepare the web page we will be asking for
            var request = (HttpWebRequest)WebRequest.Create("https://tinyurl.com/api-create.php?url=" + realUrl);

            // execute the request
            request.AllowAutoRedirect = false;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
