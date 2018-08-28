using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace HoNoSoFt.XUnit.Extensions.Utilities
{
    internal static class XmlUtility
    {
        public static object DeserializeXml(string content, Type type)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var serializer = new XmlSerializer(type);
            return serializer.Deserialize(ms);
        }

        public static T DeserializeXml<T>(string content)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var serializer = new XmlSerializer(typeof(T));
            return (T) serializer.Deserialize(ms);
        }
    }
}
