using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Harvest.Bridge.Util
{
    public class JsonCommonHelper
    {
        public static string PrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json) == false && json.IndexOf("{") != -1)
            {
                try
                {
                    dynamic parsedJson = JsonConvert.DeserializeObject(json);
                    return JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented);
                }
                catch { }
            }
            return json;
        }

        public static string SerializeObject(object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(obj, settings);
        }

        public static object DeserializeObject(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        public static string GetBase64Value(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            var jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
            return System.Convert.ToBase64String(jsonBytes);
        }

        public static object FromBase64(string base64Data, Type type)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64Data);
            string jsonString= System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return JsonConvert.DeserializeObject(jsonString, type);
        }
    }
}
