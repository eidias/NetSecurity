using Chinook.Core.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Chinook.Web
{
    public class Program
    {
        public static readonly Guid Id;
        public static readonly List<LoadedAssembly> LoadedAssemblies;

        static Program()
        {
            //Don't use Assembly.GetExecutingAssembly() here as this can have side effects in unit tests.
            var assembly = typeof(Program).Assembly;

            //The compiler ensures that no more than one GuidAttribute exists in the assembly.
            var guidAttribute = CustomAttributeData.GetCustomAttributes(assembly).FirstOrDefault(x => x.AttributeType == typeof(GuidAttribute));
            if (guidAttribute != null)
            {
                //The compiler ensures that only valid Guids are used in the attribute.
                Id = new Guid(guidAttribute.ConstructorArguments[0].Value as string);
            }

            var entryAssembly = new LoadedAssembly(assembly, loadTime: DateTime.Now, isEntryAssembly: true);
            LoadedAssemblies = new List<LoadedAssembly> { entryAssembly };

            AppDomain.CurrentDomain.AssemblyLoad += (_, args) =>
            {
                var loadedAssembly = new LoadedAssembly(args.LoadedAssembly, loadTime: DateTime.Now);
                LoadedAssemblies.Add(loadedAssembly);
            };
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
