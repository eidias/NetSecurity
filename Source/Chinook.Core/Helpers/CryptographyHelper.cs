using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Core.Helpers
{
    public static class CryptographyHelper
    {
        /// <summary>
        /// Creates a SHA1 hash over the given input
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format">Use "x2" (default) for lowercase and "X2" for uppercase hashes.</param>
        /// <returns>A string representing the SHA1 hast according to the specified format.</returns>
        public static string ComputeHash(string input, string format = "x2")
        {
            //Refer to https://stackoverflow.com/questions/22806745/why-do-samples-of-hashing-strings-typically-use-encoding-utf8 
            var buffer = Encoding.UTF8.GetBytes(input);
            return ComputeHash(buffer, format);
        }

        public static string ComputeHash(byte[] buffer, string format = "x2")
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var chars = sha1.ComputeHash(buffer).Select(x => x.ToString(format));
            return string.Join(string.Empty, chars);
        }
    }
}
