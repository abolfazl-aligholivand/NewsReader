using Newtonsoft.Json;

namespace NewsReader.Domain.Helpers
{
    public static class ExtentionMethods
    {
        public static T Deserialize<T>(this string json)
        {
            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JsonSerializer serializer = new JsonSerializer();
                var model = serializer.Deserialize(reader, typeof(T));
                return (T)model;
            }
        }
    }
}
