using System.Collections.Generic;
using System.IO;

namespace Lmbtfy.Web.Services {
    public interface IDirectoryService {
        IEnumerable<string> EnumerateFiles(string path, string pattern);
        TextReader GetReader(string path);
    }
}