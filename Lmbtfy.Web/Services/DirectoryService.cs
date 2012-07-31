using System.Collections.Generic;
using System.IO;

namespace Lmbtfy.Web.Services {
    public class DirectoryService : IDirectoryService {
        public IEnumerable<string> EnumerateFiles(string path, string pattern) {
            return System.IO.Directory.EnumerateFiles(path, pattern);
        }

        public System.IO.TextReader GetReader(string path) {
            return new StreamReader(path);
        }
    }
}