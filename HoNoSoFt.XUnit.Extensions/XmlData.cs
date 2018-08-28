using HoNoSoFt.XUnit.Extensions.Utilities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Xunit.Abstractions;

namespace HoNoSoFt.XUnit.Extensions
{
    public class XmlData<T> : IXunitSerializable
    {
        public T Data { get; private set; }
        public string Original { get; private set; }

        public XmlData(string content)
        {
            Data = XmlUtility.DeserializeXml<T>(content);
            Original = content;
        }
        
        public void Deserialize(IXunitSerializationInfo info)
        {
            Original = info.GetValue<string>("data");
            Data = XmlUtility.DeserializeXml<T>(Original);
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("data", Original, typeof(T));
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
            Original = info.GetValue<string>("data");
            Data = XmlUtility.DeserializeXml(Original, _type);
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            //info.AddValue("type", _type); // <= Should normally be done.
            info.AddValue("data", Original, _type);
        }
    }
}
