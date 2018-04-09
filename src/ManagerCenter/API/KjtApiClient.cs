using System;
using System.Net;
using System.Text;

namespace ManagerCenter.API
{
    public class KjtApiClient
    {
        public WebHeaderCollection Header;
        public string responseStr;

        public KjtApiClient(WebHeaderCollection header)
        {
            this.Header = header;
        }

        /// <summary>
        /// Get方式需要自己将参数拼接在Url中, 其实也可以通过body对象动态的去构造url参数(这部分没有实现)
        /// </summary>
        public T Service<T>(string url, string method, object body = null, string encoding = "Utf-8")
            where T : class
        {
            var requestStr = GetRequestBodyStr(body);
            string responseStr = string.Empty;

            var request =
                new RequestInfo
                {
                    Url = url,
                    Body = Encoding.GetEncoding(encoding).GetBytes(requestStr),
                    Method = method,
                    Header = this.Header
                };

            try
            {
                responseStr = new RequestHelper().GetHttpResponseStr(request);
            }
            catch (Exception e)
            {
                
            }
            Header.Add("Set-Cookie", request.Header["Set-Cookie"]);
            return
                SerializeManager.Deserialize<T>(responseStr, this.Header["Accept"]);
        }

        private string GetRequestBodyStr(object body)
        {
            if (body == null)
                return string.Empty;
            return SerializeManager.Serialize(body, this.Header["Content-Type"]);
        }
    }
}
