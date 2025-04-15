using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Abp.Reflection.Extensions;

namespace Project.Customer.Web
{
    public static class WebContentDirectoryFinder
    {
        public static string CalculateContentRootFolder()
        {
            var startupAssembly = Assembly.GetEntryAssembly();

            if (startupAssembly.GetName().Name == "ef")
            {
                string path = Path.GetDirectoryName(typeof(CustomerCoreModule).GetAssembly().Location);
                return path;
            }

            var startupAssemblyDirectoryPath = Path.GetDirectoryName(startupAssembly.Location);

            if (startupAssemblyDirectoryPath == null)
            {
                throw new Exception("Could not find location of startup (entry) assembly!");
            }

            return startupAssemblyDirectoryPath;
        }
        public  static string GetCurentAssemblyPath()
        {
            string path = Path.GetDirectoryName(typeof(CustomerCoreModule).GetAssembly().Location);
            return path;
        }
       

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
