using Newtonsoft.Json;
using System;
using Xunit.Abstractions;

namespace HoNoSoFt.XUnit.Extensions
{
    public class JsonData<T> : JsonData
    {
        public new T Data { get; private set; }

        public JsonData(string originalJson) : base(originalJson, typeof(T))
        {
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
            Original = info.GetValue<string>("rawJson");
            Data = JsonConvert.DeserializeObject(Original, _type);
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("rawJson", Original);
        }
    }
}
