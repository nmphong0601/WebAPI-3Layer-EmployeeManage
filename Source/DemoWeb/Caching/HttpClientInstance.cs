using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace DemoWeb.Caching
{
    /// <summary>
    ///   HttpClientInstance class provides singleton instance of HttpClient.
    /// </summary>
    public static class HttpClientInstance
    {
        private static Lazy<HttpClient> httpClient = new Lazy<HttpClient>(() => new HttpClient());

        /// <summary>
        ///   Get singleton instance of HttpClient.
        /// </summary>
        public static HttpClient Instance
        {
            get
            {
                return httpClient.Value;
            }
        }

        /// <summary>
        ///   Dispose current instance of HttpClient.
        /// </summary>
        public static void Dispose()
        {
            if (httpClient.IsValueCreated)
            {
                httpClient.Value.Dispose();
                httpClient = new Lazy<HttpClient>(() => new HttpClient());
            }
        }
    }
}