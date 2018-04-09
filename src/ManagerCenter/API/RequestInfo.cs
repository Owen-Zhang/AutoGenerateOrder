using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace ManagerCenter.API
{
    public class RequestInfo
    {
        public string Url { get; set; }

        public string Method { get; set; }

        public byte[] Body { get; set; }

        public WebHeaderCollection Header { get; set; }
    }

    public class RequestHelper
    {
        List<string> notParamsCollection = new List<string>
            { "connection","host","if-modified-since","transfer-encoding",
              "content-length","expect","date","range","referer","accept-encoding"
            };

        public byte[] GetHttpResponseByte(RequestInfo req)
        {
            byte[] result;
            int readerByteLength = 1024;

            using (var stream = GetWebResponse(req))
            {
                using (var reader = new BinaryReader(stream))
                {
                    using (var memeryStream = new MemoryStream())
                    {
                        while (true)
                        {
                            var temp = reader.ReadBytes(readerByteLength);
                            memeryStream.Write(temp, 0, temp.Length);
                            if (temp.Length != readerByteLength)
                                break;
                        }
                        result = memeryStream.ToArray();
                    }
                }
            }
            return result;
        }

        public string GetHttpResponseStr(RequestInfo req, string encoding = "Utf-8")
        {
            return Encoding.GetEncoding(encoding).GetString(GetHttpResponseByte(req));
        }

        /// <summary>
        /// 没有处理异常
        /// </summary>
        public Stream GetWebResponse(RequestInfo req)
        {
            if (req == null)
                return null;

            var request = (HttpWebRequest)WebRequest.Create(req.Url);
            var header = new WebHeaderCollection();
            var keys = req.Header.AllKeys.ToList().Where(every => !notParamsCollection.Contains(every));

            if (req.Header != null)
            {
                foreach (string key in keys)
                {
                    if (string.Equals(key, "Content-Type", System.StringComparison.OrdinalIgnoreCase))
                    {
                        request.ContentType = req.Header[key];
                    }
                    else if (string.Compare(key, "Accept", System.StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        request.Accept = req.Header[key];
                    }
                    else if (string.Compare(key, "User-Agent", System.StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        request.UserAgent = req.Header[key];
                    }
                    else
                    {
                        header.Add(key, req.Header[key]);
                    }
                }
            }

            request.Headers.Add(header);
            request.Method = req.Method;

            WebResponse respnse = null;
            if (req.Body == null || req.Body.Count() <= 0)
            {
                respnse = request.GetResponse();
                req.Header = respnse.Headers;

                return respnse.GetResponseStream();
            }
                
            request.ContentLength = req.Body.Length;
            using (var outputStream = request.GetRequestStream())
                outputStream.Write(req.Body, 0, req.Body.Length);

            respnse = request.GetResponse();
            req.Header = respnse.Headers;

            return respnse.GetResponseStream();

        }
    }
}
