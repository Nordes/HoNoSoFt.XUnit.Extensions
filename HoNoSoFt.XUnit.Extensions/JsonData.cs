using Newtonsoft.Json;
using System;
using Xunit.Abstractions;

namespace HoNoSoFt.XUnit.Extensions
{
    public class JsonData<T> : IXunitSerializable
    {
        public T Data { get; private set; }
        public string Original { get; private set; }

        public JsonData(string originalJson)
        {
            Data = JsonConvert.DeserializeObject<T>(originalJson);
            Original = originalJson;
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Original = info.GetValue<string>("data");
            Data = JsonConvert.DeserializeObject<T>(Original);
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("data", Original, typeof(T));
        }
    }

    public class JsonData : IXunitSerializable
    {
        private readonly Type _type;

        public object Data { get; private set; }
        public string Original { get; private set; }

        public JsonData(string originalJson, Type type)
        {
            Data = JsonConvert.DeserializeObject(originalJson, type);
            Original = originalJson;
            _type = type;
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Original = info.GetValue<string>("data");
            Data = JsonConvert.DeserializeObject(Original, _type);
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("data", Original, _type);
        }
    }

}
