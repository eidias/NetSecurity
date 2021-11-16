using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Core.Helpers
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IgnoreForTestingAttribute : Attribute
    {
        public IgnoreForTestingAttribute(string reason)
        {
            Reason = reason;
        }

        public string Reason { get; }
    }
}
