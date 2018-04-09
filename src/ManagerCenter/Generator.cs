using ManagerCenter.API;
using System.Net;
using System.Collections.Generic;

namespace ManagerCenter
{
    public class Generator
    {
        public static T LoginIn<T>(string url, List<KeyValuePair<string, string>> content,  ref string cookie)
        {
            return HttpKjtClient.SendRequest<T>(url, content, "POST", ref cookie);
        }

        public static T ShippingAddress<T>(string url, ref string cookie) where T :class
        {
            var header = new WebHeaderCollection();
            header.Add("x-kjt-app-id", "47ac0aa15d54");
            header.Add("X-OSVersion", "6.0.1");
            header.Add("Accept", ContentFormat.Json);
            header.Add("Cookie", cookie);

            var client = new KjtApiClient(header);
            var result = client.Service<T>(url, "GET", null);
            cookie = client.Header["Set-Cookie"];
            return result;
        }
        public static T GetCart<T>(string url, ref string cookie) where T : class
        {
            var header = new WebHeaderCollection();
            header.Add("x-kjt-app-id", "47ac0aa15d54");
            header.Add("X-OSVersion", "6.0.1");
            header.Add("Accept", ContentFormat.Json);
            header.Add("Cookie", cookie);

            var client = new KjtApiClient(header);
            var result = client.Service<T>(url, "GET", null);
            cookie = client.Header["Set-Cookie"];
            return result;
        }
        public static T DeleteCart<T>(string url, object request, ref string cookie) where T : class
        {
            var header = new WebHeaderCollection();
            header.Add("x-kjt-app-id", "47ac0aa15d54");
            header.Add("X-OSVersion", "6.0.1");
            header.Add("Content-Type", ContentFormat.Json);
            header.Add("Accept", ContentFormat.Json);
            header.Add("Cookie", cookie);

            var client = new KjtApiClient(header);
            var result = client.Service<T>(url, "POST", request);
            cookie = client.Header["Set-Cookie"];
            return result;
        }

        public static T AddCart<T>(string url, object request, ref string cookie) where T : class
        {
            var header = new WebHeaderCollection();
            header.Add("x-kjt-app-id", "47ac0aa15d54");
            header.Add("X-OSVersion", "6.0.1");
            header.Add("Content-Type", ContentFormat.Json);
            header.Add("Accept", ContentFormat.Json);
            header.Add("Cookie", cookie);

            var client = new KjtApiClient(header);
            var result = client.Service<T>(url, "POST", request);
            cookie = client.Header["Set-Cookie"];
            return result;
        }

        public static T CheckOut<T>(string url, object request, ref string cookie) where T : class
        {
            var header = new WebHeaderCollection();
            header.Add("x-kjt-app-id", "47ac0aa15d54");
            header.Add("X-OSVersion", "6.0.1");
            header.Add("Accept", ContentFormat.Json);
            header.Add("Cookie", cookie);

            var client = new KjtApiClient(header);
            var result = client.Service<T>(url, "GET", request);
            cookie = client.Header["Set-Cookie"];
            return result;
        }
        public static T Generate<T>(string url, object request, ref string cookie) where T : class
        {
            var header = new WebHeaderCollection();
            header.Add("x-kjt-app-id", "47ac0aa15d54");
            header.Add("X-OSVersion", "6.0.1");
            header.Add("Content-Type", ContentFormat.Json);
            header.Add("Accept", ContentFormat.Json);
            header.Add("Cookie", cookie);

            var client = new KjtApiClient(header);
            var result = client.Service<T>(url, "POST", request);
            cookie = client.Header["Set-Cookie"];
            return result;
        }
    }
}
