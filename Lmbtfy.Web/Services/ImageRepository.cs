using System.Collections.Generic;
using System.IO;
using System.Web;
using Lmbtfy.Web.Models;
using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;

namespace Lmbtfy.Web.Services
{
    public class ImageRepository : IImageRepository
    {
        List<ImageMetadata> _images = new List<ImageMetadata>();

        public ImageRepository(IDirectoryService directory, IHostingEnvironment env)
        {
            var rootPath = Path.Combine(env.WebRootPath, "images", "bg");
            var files = directory.EnumerateFiles(rootPath, "*.jpg");

            foreach (var file in files)
            {
                string imageUrl = Path.Combine(rootPath, Path.GetFileName(file));
                string metaPath = file + ".meta";
                using (var reader = directory.GetReader(metaPath))
                {
                    _images.Add(new ImageMetadata(imageUrl, reader));
                }
            }
        }

        public IEnumerable<ImageMetadata> GetImages()
        {
            return _images;
        }
    }
}