﻿using System;
using Castle.Facilities.Logging;
using Abp;
using Abp.Castle.Logging.Log4Net;
using Abp.Collections.Extensions;
using Abp.Dependency;

namespace Global.Project.Migrator
{
    public class Program
    {
        private static bool _quietMode;

        public static void Main(string[] args)
        {
            ParseArgs(args);

            using (var bootstrapper = AbpBootstrapper.Create<ProjectMigratorModule>())
            {
                bootstrapper.IocManager.IocContainer
                    .AddFacility<LoggingFacility>(
                        f => f.UseAbpLog4Net().WithConfig("log4net.config")
                    );

                bootstrapper.Initialize();

                using (var migrateExecuter = bootstrapper.IocManager.ResolveAsDisposable<MultiTenantMigrateExecuter>())
                {
                    var migrationSucceeded = migrateExecuter.Object.Run(_quietMode);
                    
                    if (_quietMode)
                    {
                        var exitCode = Convert.ToInt32(!migrationSucceeded);
                        Environment.Exit(exitCode);
                    }
                    else
                    {
                        Console.WriteLine("Press ENTER to exit...");
                        Console.ReadLine();
                    }
                }
            }
        }

        private static void ParseArgs(string[] args)
        {
            if (args.IsNullOrEmpty())
            {
                return;
            }

            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "-q":
                        _quietMode = true;
                        break;
                }
            }
        }
    }
}
