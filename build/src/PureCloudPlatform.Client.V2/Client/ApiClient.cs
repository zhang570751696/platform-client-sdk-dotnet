using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace PureCloudPlatform.Client.V2.Client
{
    /// <summary>
    /// API client is mainly responible for making the HTTP call to the API backend.
    /// </summary>
    public class ApiClient
    {
        private JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class
        /// with default configuration and base path (https://api.mypurecloud.com).
        /// </summary>
        public ApiClient()
        {
            // Use TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Configuration = Configuration.Default;
            RestClient = new RestClient("https://api.mypurecloud.com");
            AddSerializerSettings();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class
        /// with default base path (https://api.mypurecloud.com).
        /// </summary>
        /// <param name="config">An instance of Configuration.</param>
        public ApiClient(Configuration config = null)
        {
            // Use TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (config == null)
                Configuration = Configuration.Default;
            else
                Configuration = config;

            RestClient = new RestClient("https://api.mypurecloud.com");
            AddSerializerSettings();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class
        /// with default configuration.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        public ApiClient(String basePath = "https://api.mypurecloud.com")
        {
            // Use TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (String.IsNullOrEmpty(basePath))
                throw new ArgumentException("basePath cannot be empty");

            RestClient = new RestClient(basePath);
            Configuration = Configuration.Default;
            AddSerializerSettings();
        }

        private void AddSerializerSettings()
        {
            serializerSettings.Converters.Add(new Iso8601DateTimeConverter());
            serializerSettings.Converters.Add(new UpgradeSdkEnumConverter());
        }

        /// <summary>
        /// Gets or sets the default API client for making HTTP calls.
        /// </summary>
        /// <value>The default API client.</value>
        [Obsolete("ApiClient.Default is deprecated, please use 'Configuration.Default.ApiClient' instead.")]
        public static ApiClient Default;

        /// <summary>
        /// Gets or sets the Configuration.
        /// </summary>
        /// <value>An instance of the Configuration.</value>
        public Configuration Configuration { get; set; }

        /// <summary>
        /// Gets or sets the RestClient.
        /// </summary>
        /// <value>An instance of the RestClient</value>
        public RestClient RestClient { get; set; }

        // Creates and sets up a RestRequest prior to a call.
        private RestRequest PrepareRequest(
            String path, RestSharp.Method method, List<Tuple<String, String>> queryParams, Object postBody,
            Dictionary<String, String> headerParams, Dictionary<String, String> formParams,
            Dictionary<String, FileParameter> fileParams, Dictionary<String, String> pathParams,
            String contentType)
        {
            var request = new RestRequest(path, method);

            // add path parameter, if any
            foreach(var param in pathParams)
                request.AddParameter(param.Key, param.Value, ParameterType.UrlSegment);

            // add header parameter, if any
            foreach(var param in headerParams)
                request.AddHeader(param.Key, param.Value);

            // add query parameter, if any
            foreach(var param in queryParams)
                request.AddQueryParameter(param.Item1, param.Item2);

            // add form parameter, if any
            foreach(var param in formParams)
                request.AddParameter(param.Key, param.Value);

            // add file parameter, if any
            foreach(var param in fileParams)
            {
                request.AddFile(param.Value.Name, param.Value.Writer, param.Value.FileName, param.Value.ContentLength, param.Value.ContentType);
            }

            if (postBody != null) // http body (model or byte[]) parameter
            {
                if (postBody.GetType() == typeof(String))
                {
                    request.AddParameter("application/json", postBody, ParameterType.RequestBody);
                }
                else if (postBody.GetType() == typeof(byte[]))
                {
                    request.AddParameter(contentType, postBody, ParameterType.RequestBody);
                }
            }

            return request;
        }

        /// <summary>
        /// Makes the HTTP request (Sync).
        /// </summary>
        /// <param name="path">URL path.</param>
        /// <param name="method">HTTP method.</param>
        /// <param name="queryParams">Query parameters.</param>
        /// <param name="postBody">HTTP body (POST request).</param>
        /// <param name="headerParams">Header parameters.</param>
        /// <param name="formParams">Form parameters.</param>
        /// <param name="fileParams">File parameters.</param>
        /// <param name="pathParams">Path parameters.</param>
        /// <param name="contentType">Content Type of the request</param>
        /// <returns>Object</returns>
        public Object CallApi(
            String path, RestSharp.Method method, List<Tuple<String, String>> queryParams, Object postBody,
            Dictionary<String, String> headerParams, Dictionary<String, String> formParams,
            Dictionary<String, FileParameter> fileParams, Dictionary<String, String> pathParams,
            String contentType)
        {
            var request = PrepareRequest(
                path, method, queryParams, postBody, headerParams, formParams, fileParams,
                pathParams, contentType);

            // set timeout
            RestClient.Timeout = Configuration.Timeout;

            // set user agent
            RestClient.UserAgent = Configuration.UserAgent;

            // Set SDK version
            request.AddHeader("purecloud-sdk", "45.0.1");

            
            var response = RestClient.Execute(request);
            
            
            return (Object) response;
        }
        
        /// <summary>
        /// Makes the asynchronous HTTP request.
        /// </summary>
        /// <param name="path">URL path.</param>
        /// <param name="method">HTTP method.</param>
        /// <param name="queryParams">Query parameters.</param>
        /// <param name="postBody">HTTP body (POST request).</param>
        /// <param name="headerParams">Header parameters.</param>
        /// <param name="formParams">Form parameters.</param>
        /// <param name="fileParams">File parameters.</param>
        /// <param name="pathParams">Path parameters.</param>
        /// <param name="contentType">Content type.</param>
        /// <returns>The Task instance.</returns>
        public async System.Threading.Tasks.Task<Object> CallApiAsync(
            String path, RestSharp.Method method, List<Tuple<String, String>> queryParams, Object postBody,
            Dictionary<String, String> headerParams, Dictionary<String, String> formParams,
            Dictionary<String, FileParameter> fileParams, Dictionary<String, String> pathParams,
            String contentType)
        {
            var request = PrepareRequest(
                path, method, queryParams, postBody, headerParams, formParams, fileParams,
                pathParams, contentType);
            var response = await RestClient.ExecuteTaskAsync(request);
            return (Object)response;
        }

        /// <summary>
        /// Escape string (url-encoded).
        /// </summary>
        /// <param name="str">String to be escaped.</param>
        /// <returns>Escaped string.</returns>
        public string EscapeString(string str)
        {
            return UrlEncode(str);
        }

        /// <summary>
        /// Create FileParameter based on Stream.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="stream">Input stream.</param>
        /// <returns>FileParameter.</returns>
        public FileParameter ParameterToFile(string name, Stream stream)
        {
            if (stream is FileStream)
                return FileParameter.Create(name, ReadAsBytes(stream), Path.GetFileName(((FileStream)stream).Name));
            else
                return FileParameter.Create(name, ReadAsBytes(stream), "no_file_name_provided");
        }

        /// <summary>
        /// If parameter is DateTime, output in a formatted string (default ISO 8601), customizable with Configuration.DateTime.
        /// If parameter is a list, join the list with ",".
        /// Otherwise just return the string.
        /// </summary>
        /// <param name="obj">The parameter (header, path, query, form).</param>
        /// <returns>Formatted string.</returns>
        public string ParameterToString(object obj)
        {
            if (obj is DateTime)
                // Return a formatted date string - Can be customized with Configuration.DateTimeFormat
                // Defaults to an ISO 8601, using the known as a Round-trip date/time pattern ("o")
                // https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Anchor_8
                // For example: 2009-06-15T13:45:30.0000000
                return ((DateTime)obj).ToString (Configuration.DateTimeFormat);
            else if (obj is DateTimeOffset)
                // Return a formatted date string - Can be customized with Configuration.DateTimeFormat
                // Defaults to an ISO 8601, using the known as a Round-trip date/time pattern ("o")
                // https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Anchor_8
                // For example: 2009-06-15T13:45:30.0000000
                return ((DateTimeOffset)obj).ToString (Configuration.DateTimeFormat);
            else if (obj is IList)
            {
                var flattenedString = new StringBuilder();
                foreach (var param in (IList)obj)
                {
                    if (flattenedString.Length > 0)
                        flattenedString.Append(",");
                    flattenedString.Append(param);
                }
                return flattenedString.ToString();
            }
            else
                return Convert.ToString (obj);
        }

        /// <summary>
        /// Deserialize the JSON string into a proper object.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="type">Object type.</param>
        /// <returns>Object representation of the JSON string.</returns>
        public object Deserialize(IRestResponse response, Type type)
        {
            IList<Parameter> headers = response.Headers;
            if (type == typeof(byte[])) // return byte array
            {
                return response.RawBytes;
            }

            if (type == typeof(Stream))
            {
                if (headers != null)
                {
                    var filePath = String.IsNullOrEmpty(Configuration.TempFolderPath)
                        ? Path.GetTempPath()
                        : Configuration.TempFolderPath;
                    var regex = new Regex(@"Content-Disposition=.*filename=['""]?([^'""\s]+)['""]?$");
                    foreach (var header in headers)
                    {
                        var match = regex.Match(header.ToString());
                        if (match.Success)
                        {
                            string fileName = filePath + SanitizeFilename(match.Groups[1].Value.Replace("\"", "").Replace("'", ""));
                            File.WriteAllBytes(fileName, response.RawBytes);
                            return new FileStream(fileName, FileMode.Open);
                        }
                    }
                }
                var stream = new MemoryStream(response.RawBytes);
                return stream;
            }

            if (type.Name.StartsWith("System.Nullable`1[[System.DateTime")) // return a datetime object
            {
                return DateTime.Parse(response.Content,  null, System.Globalization.DateTimeStyles.RoundtripKind);
            }

            if (type == typeof(String) || type.Name.StartsWith("System.Nullable")) // return primitive type
            {
                return ConvertType(response.Content, type);
            }

            // at this point, it must be a model (json)
            try
            {
                return JsonConvert.DeserializeObject(response.Content, type, serializerSettings);
            }
            catch (Exception e)
            {
                throw new ApiException(500, e.Message);
            }
        }

        /// <summary>
        /// Serialize an input (model) into JSON string
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>JSON string.</returns>
        public String Serialize(object obj)
        {
            try
            {
                return obj != null ? JsonConvert.SerializeObject(obj, serializerSettings) : null;
            }
            catch (Exception e)
            {
                throw new ApiException(500, e.Message);
            }
        }

        /// <summary>
        /// Select the Content-Type header's value from the given content-type array:
        /// if JSON exists in the given array, use it;
        /// otherwise use the first one defined in 'consumes'
        /// </summary>
        /// <param name="contentTypes">The Content-Type array to select from.</param>
        /// <returns>The Content-Type header to use.</returns>
        public String SelectHeaderContentType(String[] contentTypes)
        {
            if (contentTypes.Length == 0)
                return null;

            if (contentTypes.Contains("application/json", StringComparer.OrdinalIgnoreCase))
                return "application/json";

            return contentTypes[0]; // use the first content type specified in 'consumes'
        }

        /// <summary>
        /// Select the Accept header's value from the given accepts array:
        /// if JSON exists in the given array, use it;
        /// otherwise use all of them (joining into a string)
        /// </summary>
        /// <param name="accepts">The accepts array to select from.</param>
        /// <returns>The Accept header to use.</returns>
        public String SelectHeaderAccept(String[] accepts)
        {
            if (accepts.Length == 0)
                return null;

            if (accepts.Contains("application/json", StringComparer.OrdinalIgnoreCase))
                return "application/json";

            return String.Join(",", accepts);
        }

        /// <summary>
        /// Encode string in base64 format.
        /// </summary>
        /// <param name="text">String to be encoded.</param>
        /// <returns>Encoded string.</returns>
        public static string Base64Encode(string text)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// Dynamically cast the object into target type.
        /// Ref: http://stackoverflow.com/questions/4925718/c-dynamic-runtime-cast
        /// </summary>
        /// <param name="source">Object to be casted</param>
        /// <param name="dest">Target type</param>
        /// <returns>Casted object</returns>
        public static dynamic ConvertType(dynamic source, Type dest)
        {
            return Convert.ChangeType(source, dest);
        }

        /// <summary>
        /// Convert stream to byte array
        /// Credit/Ref: http://stackoverflow.com/a/221941/677735
        /// </summary>
        /// <param name="input">Input stream to be converted</param>
        /// <returns>Byte array</returns>
        public static byte[] ReadAsBytes(Stream input)
        {
            byte[] buffer = new byte[16*1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// URL encode a string
        /// Credit/Ref: https://github.com/restsharp/RestSharp/blob/master/RestSharp/Extensions/StringExtensions.cs#L50
        /// </summary>
        /// <param name="input">String to be URL encoded</param>
        /// <returns>Byte array</returns>
        public static string UrlEncode(string input)
        {
            const int maxLength = 32766;

            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.Length <= maxLength)
            {
                return Uri.EscapeDataString(input);
            }

            StringBuilder sb = new StringBuilder(input.Length * 2);
            int index = 0;

            while (index < input.Length)
            {
                int length = Math.Min(input.Length - index, maxLength);
                string subString = input.Substring(index, length);

                sb.Append(Uri.EscapeDataString(subString));
                index += subString.Length;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Sanitize filename by removing the path
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>Filename</returns>
        public static string SanitizeFilename(string filename)
        {
            Match match = Regex.Match(filename, @".*[/\\](.*)$");

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return filename;
            }
        }
        
    }
}
