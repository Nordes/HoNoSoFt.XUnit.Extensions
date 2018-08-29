using HoNoSoFt.XUnit.Extensions.Utilities;
using System;
using Xunit.Abstractions;

namespace HoNoSoFt.XUnit.Extensions
{
    public class XmlData<T> : XmlData
    {
        public new T Data { get; private set; }

        public XmlData(string content) : base(content, typeof(T))
        {
        }
    }

    public class XmlData : IXunitSerializable
    {
        private readonly Type _type;

        public object Data { get; private set; }
        public string Original { get; private set; }

        public XmlData(string content, Type type)
        {
            Data = XmlUtility.DeserializeXml(content, type);
            Original = content;
            _type = type;
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Original = info.GetValue<string>("rawJson");
            Data = XmlUtility.DeserializeXml(Original, _type);
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("rawJson", Original, typeof(string));
        }
    }
}
