using System.Collections.Generic;
using System.IO;

namespace Lmbtfy.Web.Services
{
    public class DirectoryService : IDirectoryService
    {
        public IEnumerable<string> EnumerateFiles(string path, string pattern)
        {
            return Directory.EnumerateFiles(path, pattern);
        }

        public TextReader GetReader(string path)
        {
            return new StreamReader(path);
        }
    }
}