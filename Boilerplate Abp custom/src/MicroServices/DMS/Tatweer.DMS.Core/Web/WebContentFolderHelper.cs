using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Abp.Reflection.Extensions;

namespace Tatweer.ITSM.Web
{
    /// <summary>
    /// This class is used to find root path of the web project in;
    /// unit tests (to find views) and entity framework core command line commands (to find conn string).
    /// </summary>
    public static class WebContentDirectoryFinder
    {
        public static string CalculateContentRootFolder()
        {
            var startupAssembly = Assembly.GetEntryAssembly();

            // Check if it is running from Package Manager Console (Add-Migration, Update-Database, etc)
            if (startupAssembly.GetName().Name == "ef")
            {
                string path = Path.GetDirectoryName(typeof(ITSMCoreModule).GetAssembly().Location);
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
            string path = Path.GetDirectoryName(typeof(ITSMCoreModule).GetAssembly().Location);
            return path;
        }
        //public static string CalculateContentRootFolder()
        //{
        //    var coreAssemblyDirectoryPath = Path.GetDirectoryName(typeof(SmartOfficerCoreModule).GetAssembly().Location);
        //    if (coreAssemblyDirectoryPath == null)
        //    {
        //        throw new Exception("Could not find location of Tatweer.ITSM.Core assembly!");
        //    }

        //    var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
        //    while (!DirectoryContains(directoryInfo.FullName, "Tatweer.SmartOfficer.sln"))
        //    {
        //        if (directoryInfo.Parent == null)
        //        {
        //            throw new Exception("Could not find content root folder!");
        //        }

        //        directoryInfo = directoryInfo.Parent;
        //    }

        //    var webMvcFolder = Path.Combine(directoryInfo.FullName, "src", "Tatweer.SmartOfficer.Web.Mvc");
        //    if (Directory.Exists(webMvcFolder))
        //    {
        //        return webMvcFolder;
        //    }

        //    var webHostFolder = Path.Combine(directoryInfo.FullName, "src", "Tatweer.SmartOfficer.Web.Host");
        //    if (Directory.Exists(webHostFolder))
        //    {
        //        return webHostFolder;
        //    }

        //    throw new Exception("Could not find root folder of the web project!");
        //}

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
