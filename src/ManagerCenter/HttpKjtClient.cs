using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ManagerCenter
{
    public class HttpKjtClient
    {
        public static T SendRequest<T>(string url, List<KeyValuePair<string, string>> paraList, string method, ref string cookie)
        {
            T result = default(T);

            if (url == null || url == "")
            {
                return result;
            }
            if (string.IsNullOrWhiteSpace(method))
            {
                method = "GET";
            }

            if (url.StartsWith("https://"))
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            
            // GET方式
            if (method.ToUpper() == "GET")
            {

                string fullUrl = url.TrimEnd('?') + "?";
                if (paraList != null)
                {
                    foreach (KeyValuePair<string, string> kv in paraList)
                    {
                        //Jin：Uri.EscapeDataString是最完美URLENCODE方案
                        fullUrl += kv.Key + "=" + Uri.EscapeDataString(kv.Value) + "&";
                    }
                    fullUrl = fullUrl.TrimEnd('&');
                }

                var wrq = System.Net.WebRequest.Create(fullUrl) as HttpWebRequest;

                if (url.StartsWith("https://"))
                    wrq.ProtocolVersion = HttpVersion.Version10;

                wrq.Method = "GET";
                if (!string.IsNullOrEmpty(cookie))
                    wrq.Headers.Add("Cookie", cookie);

                System.Net.WebResponse response = wrq.GetResponse();
                var header = response.Headers;
                cookie = header["Set-Cookie"];

                System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));

                string strResult = sr.ReadToEnd();
                sr.Close();
                response.Close();

                result = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(strResult);
            }

            // POST方式
            if (method.ToUpper() == "POST")
            {
                url = url.TrimEnd('?');
                var wrq = WebRequest.Create(url) as HttpWebRequest;

                if (url.StartsWith("https://"))
                    wrq.ProtocolVersion = HttpVersion.Version10;

                wrq.Method = "POST";
                wrq.ContentType = "application/x-www-form-urlencoded";
                wrq.Headers.Add("X-OSVersion", "6.0.1");
                wrq.Headers.Add("x-kjt-app-id", "47ac0aa15d54");

                StringBuilder sbPara = new StringBuilder();
                string paraString = string.Empty;
                if (paraList != null)
                {
                    foreach (KeyValuePair<string, string> kv in paraList)
                    {
                        sbPara.Append(kv.Key + "=" + kv.Value + "&");
                    }
                    paraString = sbPara.ToString().TrimEnd('&');

                    byte[] buf = System.Text.Encoding.GetEncoding("utf-8").GetBytes(paraString);
                    System.IO.Stream requestStream = wrq.GetRequestStream();
                    requestStream.Write(buf, 0, buf.Length);
                    requestStream.Close();
                }
                else
                {
                    wrq.ContentLength = 0;
                }

                HttpWebResponse response = null;
                StreamReader reader = null;
                string strResult = string.Empty;

                try
                {
                    response = wrq.GetResponse() as HttpWebResponse;
                    var header = response.Headers;
                    cookie = header["Set-Cookie"];
                    Encoding htmlEncoding = Encoding.UTF8;
                    reader = new StreamReader(response.GetResponseStream(), htmlEncoding);
                    strResult = reader.ReadToEnd();
                    reader.Close();
                    response.Close();

                    
                }
                catch (WebException e)
                {
                    throw new Exception(string.Format("调用接口有问题: {0}", e.Message));
                }
                catch (Exception e)
                {
                    if (reader != null)
                        reader.Close();

                    if (response != null)
                        response.Close();
                }
                if (!string.IsNullOrWhiteSpace(strResult))
                    result = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(strResult);
            }
            return result;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }
    }
}
