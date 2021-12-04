using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Core.Helpers
{
    public class LoadedAssembly
    {
        public Assembly Assembly { get; }
        public DateTime LoadTime { get; }
        public bool IsEntryAssembly { get; }

        AssemblyName assemblyName;
        public AssemblyName AssemblyName
        {
            get
            {
                if (assemblyName == null)
                {
                    assemblyName = Assembly.GetName();
                }
                return assemblyName;
            }
        }

        FileVersionInfo versionInfo;
        public FileVersionInfo VersionInfo
        {
            get
            {
                if (versionInfo == null)
                {
                    try
                    {
                        versionInfo = FileVersionInfo.GetVersionInfo(Assembly.Location);
                    }
                    catch
                    {
                        //Intentionally left blank for cases where file version info is not supported (i.e. in dynamic assemblies).
                    }
                }
                return versionInfo;
            }
        }

        X509Certificate certificate;
        public X509Certificate Certificate
        {
            get
            {
                if (certificate == null)
                {
                    try
                    {
                        //Remark: ASN.1 DER is the only format supported in this type.
                        certificate = X509Certificate.CreateFromSignedFile(Assembly.Location);
                    }
                    catch
                    {
                        //Intentionally left blank for cases where a certificate could not be read (e.g. in dynamic or generally unsigned assemblies).
                    }
                }
                return certificate;
            }
        }

        public LoadedAssembly(Assembly assembly, DateTime loadTime, bool isEntryAssembly = false)
        {
            Assembly = assembly;
            LoadTime = loadTime;
            IsEntryAssembly = isEntryAssembly;
        }
    }
}
