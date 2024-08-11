using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        static bool initialized = false;
        public static string GetHash(string input)
        {
            var message = System.Text.Encoding.UTF8.GetBytes(input);
            using (var alg = SHA512.Create())
            {
                string hex = "";

                var hashValue = alg.ComputeHash(message);
                foreach (byte x in hashValue)
                {
                    hex += String.Format("{0:x2}", x);
                }
                return hex;
            }
        }

        static BaseController()
        {
            if (!initialized)
            {
                initialized = true;
            }
        }
    }

    public static class HrefHelper
    {
        static string root = "http://localhost:5148/api/";

        public static string ToAccountHref(this int? id) { return root + "accounts/" + id; }

        public static int? ToId(this string href)
        {
            if (string.IsNullOrEmpty(href)) return null;
            return int.Parse(href.Substring(href.LastIndexOf('/') + 1));
        }

    }
}
