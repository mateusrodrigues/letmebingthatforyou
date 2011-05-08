using System;
using System.IO;
using System.Net;
using System.Web.Caching;
using System.Web.Mvc;

namespace Lmbtfy.Web.Controllers {
    public class BingImageCssController : Controller {
        public ActionResult Index() {
            string imageCss = "";
            if (HttpContext.Cache["___BING_IMAGE"] == null) {
                imageCss += "#bgDiv { BACKGROUND-IMAGE: url(http://www.bing.com" + GetBingImage("http://www.bing.com") + "); BACKGROUND-REPEAT: no-repeat; }";
                imageCss += "#bgDivFull { BACKGROUND-IMAGE: url(http://www.bing.com" + GetBingImage("http://www.bing.com/search?q=TEST") + "); BACKGROUND-REPEAT: no-repeat; }";
                if (!string.IsNullOrEmpty(imageCss)) {
                    HttpContext.Cache.Add("___BING_IMAGE", imageCss, null, DateTime.Now.AddHours(2), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
                }
            }
            else {
                imageCss = HttpContext.Cache["___BING_IMAGE"].ToString();
            }
            return Content(imageCss, "text/css");
        }

        protected string GetBingImage(string url) {
            try {
                const string startString = "g_img={url:'";
                string bingSource = GetBingSource(url);
                int startIndex = bingSource.IndexOf(startString) + startString.Length;
                return bingSource.Substring(startIndex, bingSource.Substring(startIndex).IndexOf(".jpg")).Replace("\\", "") + ".jpg";
            }
            catch {
                return "";
            }
        }

        protected string GetBingSource(string url) {
            // prepare the web page we will be asking for
            var request = (HttpWebRequest)WebRequest.Create(url);

            // execute the request
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream())) {
                return sr.ReadToEnd();
            }
        }
    }
}
