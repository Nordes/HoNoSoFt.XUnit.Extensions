using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace HoNoSoFt.XUnit.Extensions
{
    /// <summary>
    /// Usage of the JsonFileData attribute gives the opportunity to load json data
    /// in your tests.
    /// </summary>
    public class JsonFileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        private readonly Type _type = null;
        private readonly object[] _data;

        /// <inheritdoc />
        public JsonFileDataAttribute(string filePath, params object[] data)
        {
            _filePath = filePath; // Could also look if this is inline json.
            _data = data;
        }

        /// <inheritdoc />
        public JsonFileDataAttribute(string filePath, Type type, params object[] data)
            : this(filePath, data)
        {
            _type = type;
        }

        /// <inheritdoc />
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }
            string fileData = null;

            if (_filePath.Trim().StartsWith("{"))
            {
                fileData = _filePath; // Apparently it's json, not a path.
            }
            else
            {
                // Get the absolute path to the JSON file
                var path = Path.IsPathRooted(_filePath)
                    ? _filePath
                    : Directory.GetCurrentDirectory() + "/" + _filePath;
                // Original code (Core 2.1, can't work in Standard2.0, maybe when 2.1 arrives) :(
                //: Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);

                fileData = LoadFile(path);
            }

            var result = new List<object>(_data);
            if (_type != null)
            {
                result.Insert(0, new JsonData(fileData, _type));
            }
            else
            {
                var type = testMethod.GetParameters()[0].ParameterType;
                result.Insert(0, (type == null)
                    ? JsonConvert.DeserializeObject<object>(fileData) // This will return a JObject.
                    : JsonConvert.DeserializeObject(fileData, type));
            }

            return new[] { result.ToArray() };
        }

        private static string LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                // Maybe we should return null, and then create an empty argument when not found.
                throw new FileNotFoundException($"Could not find the JSON file located at: {path}");
            }

            var fileData = File.ReadAllText(path);
            return fileData;
        }
    }
}
