using Lmbtfy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lmbtfy.Web.Services
{
    public interface IImageRepository
    {
        IEnumerable<ImageMetadata> GetImages();
    }
}
