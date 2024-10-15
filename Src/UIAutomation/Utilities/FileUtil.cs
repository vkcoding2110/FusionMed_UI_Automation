using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace UIAutomation.Utilities
{
    public class FileUtil
    {
        public string GetBasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public string GetDownloadPath()
        {
            var pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var pathDownload = Path.Combine(pathUser, "Downloads");
            return pathDownload;
        }

        public void DeleteFileInFolder(string folder, string fileName, string fileType)
        {
            var dir = new DirectoryInfo(folder);

            foreach (var file in dir.GetFiles())
            {
                if (file.Name.StartsWith(fileName) && file.Extension.ToUpper().Equals(fileType.ToUpper()))
                {
                    file.Delete();
                }
            }
        }

        public bool DoesFileExistInFolder(string folder, string fileName, string fileType, int timeout = 10)
        {
            var dir = new DirectoryInfo(folder);

            for (var i = 0; i < timeout; i++)
            {
                if (dir.GetFiles().Any(file => file.Name.StartsWith(fileName) && file.Extension.ToUpper().Equals(fileType.ToUpper())))
                {
                    return true;
                }

                Thread.Sleep(1000);
            }
            return false;
        }
    }
}
