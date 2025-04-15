using System;
using System.IO;
using Abp.Reflection.Extensions;

namespace Project.Customer
{
    public class AppVersionHelper
    {
        public const string Version = "5.5.0.0";

        public static DateTime ReleaseDate => LzyReleaseDate.Value;

        private static readonly Lazy<DateTime> LzyReleaseDate = new Lazy<DateTime>(() => new FileInfo(typeof(AppVersionHelper).GetAssembly().Location).LastWriteTime);
    }
}
