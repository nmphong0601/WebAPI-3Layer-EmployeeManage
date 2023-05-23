using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        static bool initialized = false;
        public string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        static BaseApiController()
        {
            if (!initialized)
            {
                initialized = true;
            }
        }

    }

    public static class HrefHelper
    {
        static string root = "http://localhost:58749/api/";

        public static string ToAccountHref(this int? id) { return root + "accounts/" + id; }

        public static int? ToId(this string href)
        {
            if (string.IsNullOrEmpty(href)) return null;
            return int.Parse(href.Substring(href.LastIndexOf('/') + 1));
        }

    }

}
