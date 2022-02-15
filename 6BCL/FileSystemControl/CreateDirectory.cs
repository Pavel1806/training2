using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSystemControl
{
    static class CreateDirectory
    {
        static public Dictionary<string, string> CreateTestDirectory(string pathTracking)
        {
            
            if (!Directory.Exists(pathTracking))
            {
                Directory.CreateDirectory(pathTracking);
                Directory.CreateDirectory(Path.Join(pathTracking, "FolderTxtFiles"));
                Directory.CreateDirectory(Path.Join(pathTracking, "FolderDocxFiles"));
                Directory.CreateDirectory(Path.Join(pathTracking, "FolderDefaultFiles"));
            }

            Dictionary<string, string> pathDirectorys = new Dictionary<string, string>
            {
                ["pathTracking"] = pathTracking,
                ["FolderTxtFiles"] = Path.Join(pathTracking, "FolderTxtFiles"),
                ["FolderDocxFiles"] = Path.Join(pathTracking, "FolderDocxFiles"),
                ["FolderDefaultFiles"] = Path.Join(pathTracking, "FolderDefaultFiles")
            };

            return pathDirectorys;
        }
    }
}
