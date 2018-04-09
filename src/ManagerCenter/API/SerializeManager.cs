using System;
using System.IO;
using System.Text;

namespace ManagerCenter.API
{
    public class SerializeManager
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        public static string Serialize(object reqObj, string contentType = "application/json")
        {
            if (reqObj is string)
            {
                return reqObj as string;
            }

            //var accept = this.Header["Content-Type"];
            if (string.IsNullOrWhiteSpace(contentType))
            {
                return null;
            }

            if (string.Compare(contentType, ContentFormat.Xml, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return ServiceStack.Text.XmlSerializer.SerializeToString(reqObj);
            }

            return string.Compare(contentType, ContentFormat.Json, StringComparison.OrdinalIgnoreCase) == 0 ? ServiceStack.Text.JsonSerializer.SerializeToString(reqObj) : null;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseStr"></param>
        /// <param name="accept"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string responseStr, string contentType = "application/json")
            where T : class
        {
            if (string.IsNullOrWhiteSpace(responseStr))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                return null;
            }

            var t = default(T);
            if (string.Compare(contentType, ContentFormat.Xml, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return ServiceStack.Text.XmlSerializer.DeserializeFromString<T>(responseStr);
                /*
                byte[] buffer = Encoding.UTF8.GetBytes(responseStr);
                using (var stream = new MemoryStream(buffer))
                {
                    t = ServiceStack.Text.XmlSimpleSerializer.DeserializeFromStream<T>(stream);
                }
                */
            }
            else if (string.Compare(contentType, ContentFormat.Json, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(responseStr);
            }

            return t;
        }
    }
}
