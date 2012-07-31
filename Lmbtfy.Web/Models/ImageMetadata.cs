using System.IO;
using System.Web;
using System;

namespace Lmbtfy.Web.Models {
    public class ImageMetadata {
        public ImageMetadata(string imageUrl) {
            ImageUrl = imageUrl;
        }

        public static ImageMetadata GetImageMetadata(HttpServerUtilityBase server, string imageUrl) {
            string path = server.MapPath(imageUrl + ".meta");

            using (var reader = new StreamReader(path)) {
                return new ImageMetadata(imageUrl, reader);
            }
        }

        public ImageMetadata(string imageUrl, TextReader reader) {
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }
            ImageUrl = imageUrl;
            LicenseUrl = reader.ReadLine();
            LicenseType = reader.ReadLine();
            SourceUrl = reader.ReadLine();
            Author = reader.ReadLine();
        }

        public string ImageUrl { get; private set; }
        public string LicenseUrl { get; private set; }
        public string LicenseType { get; private set; }
        public string SourceUrl { get; private set; }
        public string Author { get; private set; }
    }
}