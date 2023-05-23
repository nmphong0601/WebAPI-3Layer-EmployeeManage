using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using DemoWeb.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace DemoWeb.Caching
{
    public class CSDLQLNV
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CSDLQLNV));
        static string serverUrl = System.Configuration.ConfigurationManager.AppSettings["QLNVServerUrl"];
        static string accessToken = string.Empty;

        #region cache management

        static ObjectCache cache = new MemoryCache("CSDLQLNV");

        // clear entire cache
        public static void Clear()
        {
            foreach (var item in cache)
                cache.Remove(item.Key);
        }

        // clears single cache entry
        public static void Clear(string key)
        {
            cache.Remove(key);
        }

        // add to cache helper
        static void Add(string key, object value, DateTimeOffset expiration, CacheItemPriority priority = CacheItemPriority.Default)
        {
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = expiration;
            policy.Priority = priority;

            var item = new CacheItem(key, value);
            cache.Add(item, policy);
        }

        #endregion

        #region Access token

        #endregion

        #region Public static methods

            #region Manager

        public static IEnumerable<Manager> GetManagers(string filter = null, string sort = "DOB DESC")
        {
            try
            {
                var endpointString = serverUrl + "api/managers?filter=" + filter + "&sort=" + sort;
                var managers = Task.Run(() => GetNullCheckAsync<IEnumerable<Manager>>(endpointString)).Result;

                return managers;
            }
            catch (BusinessLayerException ex)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu managers.");
            }
            catch (Exception ex)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu managers.");
            }
        }

        public static Dictionary<string, object> GetPagedManagers(string keyword = null, string filter = null, string sort = "DOB DESC", int page = 1, int pageSize = 6)
        {
            try
            {
                var endpointString = serverUrl + "api/managers/paging?keywork="+ keyword + "&filter=" + filter + "&sort=" + sort + "&page=" + page + "&pageSize=" + pageSize;
                var managers = Task.Run(() => GetNullCheckAsync<Dictionary<string, object>>(endpointString)).Result;

                return managers;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu managers.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu managers.");
            }
        }

        public static Manager GetSingleManager(int? id)
        {
            try
            {
                var endpointString = serverUrl + "api/managers/" + id;
                var manager = Task.Run(() => GetNullCheckAsync<Manager>(endpointString)).Result;

                return manager;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu manager.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu manager.");
            }
        }

        public static Manager InsertManager(Manager manager)
        {
            try
            {
                var endpointString = serverUrl + "api/managers";
                var managerInsert = Task.Run(() => PostAsync<Manager>(endpointString, manager)).Result;

                return managerInsert;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không thêm được dữ liệu manager.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không thêm được dữ liệu manager.");
            }
        }

        public static Manager UpdateManager(Manager manager)
        {
            try
            {
                var endpointString = serverUrl + "api/managers";
                var managerUpdate = Task.Run(() => PutAsync<Manager>(endpointString, manager)).Result;

                return managerUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không cập nhật được dữ liệu manager.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không cập nhật được dữ liệu manager.");
            }
        }

        public static Boolean RemoveManager(int? id)
        {
            try
            {
                var endpointString = serverUrl + "api/managers/" + id;
                var managerDelete = Task.Run(() => DeleteAsync<Boolean>(endpointString)).Result;

                return managerDelete;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không xóa được dữ liệu manager.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không xóa được dữ liệu manager.");
            }
        }

        #endregion

            #region Employee

        public static IEnumerable<Employee> GetEmployees(string filter = null, string sort = "DOB DESC")
        {
            try
            {
                var endpointString = serverUrl + "api/employees?filter=" + filter + "&sort=" + sort;
                var employees = Task.Run(() => GetNullCheckAsync<IEnumerable<Employee>>(endpointString)).Result;

                return employees;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu employees.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu employees.");
            }
        }

        public static Dictionary<string, object> GetPagedEmployees(string keyword = null, string filter = null, string sort = "ProID DESC", int page = 1, int pageSize = 6)
        {
            try
            {
                var endpointString = serverUrl + "api/employees?keywork="+ keyword + "&filter=" + filter + "&sort=" + sort + "&page=" + page + "&pageSize=" + pageSize;
                var employees = Task.Run(() => GetNullCheckAsync<Dictionary<string, object>>(endpointString)).Result;

                return employees;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu employees.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu employees.");
            }
        }

        public static Employee GetSingleEmployee(int? id)
        {
            try
            {
                var endpointString = serverUrl + "api/employees/" + id;
                var employee = Task.Run(() => GetNullCheckAsync<Employee>(endpointString)).Result;

                return employee;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu employee.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu employee.");
            }
        }

        public static Employee InsertEmployee(Employee employee)
        {
            try
            {
                var endpointString = serverUrl + "api/employees";
                var employeeInsert = Task.Run(() => PostAsync<Employee>(endpointString, employee)).Result;

                return employeeInsert;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không thêm được dữ liệu employee.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không thêm được dữ liệu employee.");
            }
        }

        public static Employee UpdateEmployee(Employee employee)
        {
            try
            {
                var endpointString = serverUrl + "api/employees";
                var employeeUpdate = Task.Run(() => PutAsync<Employee>(endpointString, employee)).Result;

                return employeeUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không cập nhật được dữ liệu employee.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không cập nhật được dữ liệu employee.");
            }
        }

        public static Boolean RemoveEmployee(int? id)
        {
            try
            {
                var endpointString = serverUrl + "api/employees/" + id;
                var employeeDelete = Task.Run(() => DeleteAsync<Boolean>(endpointString)).Result;

                return employeeDelete;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không xóa được dữ liệu employee.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không xóa được dữ liệu employee.");
            }
        }

        #endregion

        #endregion


        #region Calling api methods

        /// <summary>
        ///   Lấy dữ liệu theo endpoint, có lưu cache, kiểm tra null.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        /// <exception cref="BusinessLayerException">Khi không lấy được dữ liệu.</exception>
        /// <exception cref="Exception">Khi có lỗi khi lấy dữ liệu.</exception>
        private static async Task<T> GetCachedNullCheckAsync<T>(string endpoint, int days = 1)
        {
            try
            {
                var resultCache = cache[endpoint];

                if (resultCache == null)
                {
                    var client = HttpClientInstance.Instance;

                    //if (accessToken == string.Empty)
                    //{
                    //    accessToken = await GetAccessTokenAsync();
                    //}

                    //client.DefaultRequestHeaders.Accept.Clear();

                    //// Add the Authorization header with the AccessToken.
                    //client.DefaultRequestHeaders.Clear();
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    //var response = (await client.GetAsync(endpoint)) ?? throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu.");
                    var response = await client.GetAsync(endpoint);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu.");
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;

                        string responseString = await responseContent.ReadAsStringAsync();

                        resultCache = JsonConvert.DeserializeObject<T>(responseString);
                        if (((dynamic)resultCache).Id == null)
                            throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu.");
                        Add(endpoint, resultCache, DateTime.Now.AddDays(days));
                    }
                    else
                    {
                        Log.Info($"====={nameof(GetCachedNullCheckAsync)} response:{JsonConvert.SerializeObject(response)}=====client:{JsonConvert.SerializeObject(client)}====accessToken:{accessToken}===Endpoint:{endpoint}");
                        throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu");
                    }

                    Log.Info($"====={nameof(GetCachedNullCheckAsync)} Khonglaytucache:Endpoint:{endpoint}");
                }
                else
                {
                    Log.Info($"====={nameof(GetCachedNullCheckAsync)} Laytucache:{endpoint}");
                }

                return (T)resultCache;
            }
            catch (BusinessLayerException ex)
            {
                Log.Debug($"====={nameof(GetCachedNullCheckAsync)} BusinessLayerException: Endpoint:{endpoint}=====ex:{ ex.ToString()}");
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Debug($"====={nameof(GetCachedNullCheckAsync)} Exception:Endpoint:{endpoint}");
                throw ex;
            }
        }

        /// <summary>
        ///   Lấy dữ liệu theo endpoint, có lưu cache, không kiểm tra null.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        /// <exception cref="BusinessLayerException">Khi không lấy được dữ liệu.</exception>
        /// <exception cref="Exception">Khi có lỗi khi lấy dữ liệu.</exception>
        private static async Task<T> GetCachedAsync<T>(string endpoint, int days = 1)
        {
            try
            {
                var resultCache = cache[endpoint];

                if (resultCache == null)
                {
                    var client = HttpClientInstance.Instance;

                    //if (accessToken == string.Empty)
                    //{
                    //    accessToken = await GetAccessTokenAsync();
                    //}

                    //client.DefaultRequestHeaders.Accept.Clear();

                    //// Add the Authorization header with the AccessToken.
                    //client.DefaultRequestHeaders.Clear();
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    var response = (await client.GetAsync(endpoint));

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;

                        string responseString = await responseContent.ReadAsStringAsync();

                        resultCache = JsonConvert.DeserializeObject<T>(responseString);
                        if (resultCache != null && resultCache.GetType().GetProperty("Id")?.GetValue(resultCache, null) != null)
                            Add(endpoint, resultCache, DateTime.Now.AddDays(days));
                    }
                    else
                    {
                        Log.Info($"====={nameof(GetCachedAsync)} response:{JsonConvert.SerializeObject(response)}=====client:{JsonConvert.SerializeObject(client)}====accessToken:{accessToken}===Endpoint:{endpoint}");
                        throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu");
                    }

                    Log.Info($"====={nameof(GetCachedAsync)} Khonglaytucache:Endpoint:{endpoint}");
                }
                else
                {
                    Log.Info($"====={nameof(GetCachedAsync)} Laytucache:{endpoint}");
                }

                return (T)resultCache;
            }
            catch (BusinessLayerException ex)
            {
                Log.Debug($"====={nameof(GetCachedAsync)} BusinessLayerException: Endpoint:{endpoint}=====ex:{ ex.ToString()}");
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Debug($"====={nameof(GetCachedAsync)} Exception:Endpoint:{endpoint}");
                throw ex;
            }
        }

        /// <summary>
        ///   Lấy dữ liệu theo endpoint, không lưu cache, có kiểm tra null.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private static async Task<T> GetNullCheckAsync<T>(string endpoint)
        {
            try
            {
                var client = HttpClientInstance.Instance;

                //if (accessToken == string.Empty)
                //{
                //    accessToken = await GetAccessTokenAsync();
                //}

                //client.DefaultRequestHeaders.Accept.Clear();

                //// Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                //var response = await client.GetAsync(endpoint) ?? throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu.");

                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu.");
                }

                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = await responseContent.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    Log.Info($"====={nameof(GetNullCheckAsync)} response:{JsonConvert.SerializeObject(response)}=====client:{JsonConvert.SerializeObject(client)}====accessToken:{accessToken}===Endpoint:{endpoint}");
                    throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu");
                }
            }
            catch (BusinessLayerException ex)
            {
                Log.Debug($"====={nameof(GetNullCheckAsync)} BusinessLayerException: Endpoint:{endpoint}=====ex:{ex.ToString()}");
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Debug($"====={nameof(GetNullCheckAsync)} Exception:Endpoint:{endpoint}");
                throw ex;
            }
        }
        /// <summary>
        ///   Lấy dữ liệu theo endpoint, không lưu cache, không kiểm tra null.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        /// <exception cref="BusinessLayerException">Khi không lấy được dữ liệu.</exception>
        /// <exception cref="Exception">Khi có lỗi khi lấy dữ liệu.</exception>
        private static async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                var client = HttpClientInstance.Instance;

                //if (accessToken == string.Empty)
                //{
                //    accessToken = await GetAccessTokenAsync();
                //}

                //client.DefaultRequestHeaders.Accept.Clear();

                //// Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = (await client.GetAsync(endpoint));

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    string responseString = await responseContent.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    Log.Info($"====={nameof(GetAsync)} response:{JsonConvert.SerializeObject(response)}=====client:{JsonConvert.SerializeObject(client)}====accessToken:{accessToken}===Endpoint:{endpoint}");
                    throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu");
                }
            }
            catch (BusinessLayerException ex)
            {
                Log.Debug($"====={nameof(GetAsync)} BusinessLayerException: Endpoint:{endpoint}=====ex:{ex.ToString()}");
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Debug($"====={nameof(GetAsync)} Exception:Endpoint:{endpoint}");
                throw ex;
            }
        }

        /// <summary>
        ///   Thực hiện lệnh post.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endPoint"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async static Task<T> PostAsync<T>(string endPoint, object data)
        {
            try
            {
                var client = HttpClientInstance.Instance;

                //if (accessToken == string.Empty)
                //{
                //    accessToken = await GetAccessTokenAsync();
                //}

                //client.DefaultRequestHeaders.Accept.Clear();

                //// Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var httpContent = new StringContent(JsonConvert.SerializeObject(data));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpContent.Headers.ContentType.CharSet = "UTF-8";

                var response = await client.PostAsync(endPoint, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Log.Info($"=====response:{JsonConvert.SerializeObject(content)}object: {JsonConvert.SerializeObject(data)} ");
                    throw new BusinessLayerException(errorDescription: content);
                }
            }
            catch (BusinessLayerException ex)
            {
                Log.Debug(data, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Debug(data, ex);
                throw ex;
            }
        }

        /// <summary>
        ///   Thực hiện lệnh put.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<T> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                var client = HttpClientInstance.Instance;

                //if (accessToken == string.Empty)
                //{
                //    accessToken = await GetAccessTokenAsync();
                //}

                //client.DefaultRequestHeaders.Accept.Clear();

                //// Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var httpContent = new StringContent(JsonConvert.SerializeObject(data));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync(endpoint, httpContent);
                string content = await response.Content.ReadAsStringAsync();

                return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///   Thực hiện lệnh delete.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteAsync<T>(string endpoint)
        {
            try
            {
                var client = HttpClientInstance.Instance;

                //if (accessToken == string.Empty)
                //{
                //    accessToken = await GetAccessTokenAsync();
                //}

                //client.DefaultRequestHeaders.Accept.Clear();

                //// Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = await client.DeleteAsync(endpoint);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}