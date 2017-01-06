using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemLibrary
{
    public class FileSystemHelper : IFileSystemHelper
    {
        public void createFolderIfNonExisting(string fullPath)
        {
            throw new NotImplementedException();
        }

        public void deleteOnPath(string path)
        {
            throw new NotImplementedException();
        }

        public string getCurrentDirectory()
        {
            throw new NotImplementedException();
        }

        public List<String> getFileNames(string directory, string prefix="",string suffix="")
        {
            List<String> fns = new List<String>();
            String format = prefix == "" ? "*" : prefix + "*";
            format = suffix == "" ? format : format + "." + suffix;
            foreach (string s in System.IO.Directory.GetFiles(directory, format))
            {
                fns.Add(s.Split('\\').Last());
            }
            return fns;
        }
    }
}
