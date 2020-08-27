namespace ProcessTests.Api.ProcessTests.Infrastructure
{
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class Serializer
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatString = "o"
        };

        public static string Serialize<T>(this T objectToSerialize)
            where T : class
        {
            return JsonConvert.SerializeObject(objectToSerialize, SerializerSettings);
        }

        public static T Deserialize<T>(this string objectToDeserialize)
        {
            return JsonConvert.DeserializeObject<T>(objectToDeserialize, SerializerSettings);
        }

        public static StringContent ToStringContent<T>(this T objectToConvert)
            where T : class
        {
            return new StringContent(objectToConvert.Serialize(), Encoding.UTF8, "application/json");
        }
    }
}
