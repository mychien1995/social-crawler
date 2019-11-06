using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Engine.Helpers
{
    public static class JsonHelper
    {
        public static bool TryParseToObject<T>(this string jsonValue, out T returnObject)
        {
            try
            {
                returnObject = JsonConvert.DeserializeObject<T>(jsonValue);
            }
            catch
            {
                returnObject = default(T);
                return false;
            }
            return true;
        }


        public static bool TryParseToObject(this string jsonValue, Type t, out object value)
        {
            try
            {
                value = JsonConvert.DeserializeObject(jsonValue, t);
            }
            catch
            {
                value = null;
                return false;
            }
            return true;
        }
    }
}
