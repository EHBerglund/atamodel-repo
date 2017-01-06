using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemLibrary
{
    public interface IFileSystemHelper
    {
        String getCurrentDirectory();
        void deleteOnPath(String path);
        void createFolderIfNonExisting(String fullPath);

        List<String> getFileNames(string directory, string prefix, string suffix);
    }
}
