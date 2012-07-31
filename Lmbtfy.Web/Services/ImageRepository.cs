using System.Collections.Generic;
using System.IO;
using System.Web;
using Lmbtfy.Web.Models;
using System;

namespace Lmbtfy.Web.Services {
    public class ImageRepository {
        List<ImageMetadata> _images = new List<ImageMetadata>();

        public ImageRepository(HttpServerUtilityBase server, IDirectoryService directory, string rootPath) {
            string path = server.MapPath(rootPath);
            var files = directory.EnumerateFiles(path, "*.jpg");

            foreach (var file in files) {

                string imageUrl = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(rootPath), Path.GetFileName(file));
                string metaPath = file + ".meta";
                using (var reader = directory.GetReader(metaPath)) {
                    _images.Add(new ImageMetadata(imageUrl, reader));
                }
            }
        }

        public IEnumerable<ImageMetadata> GetImages() {
            return _images;
        }
    }
}