using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace HoNoSoFt.XUnit.Extensions
{
    /// <summary>
    /// Come from:
    /// https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/
    /// </summary>
    public class JsonFileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        private readonly Type _type = null;
        private readonly object[] _data;

        /// <inheritdoc />
        public JsonFileDataAttribute(string filePath, params object[] data)
        {
            _filePath = filePath; // Could also look if this is inline json, but it make no sense.
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

            // Get the absolute path to the JSON file
            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);
            var type = testMethod.GetParameters()[0].ParameterType;

            if (!File.Exists(path))
            {
                throw new ArgumentException($"Could not find file at path: {path}");
            }

            // Load the file
            var fileData = File.ReadAllText(_filePath);
            var result = new List<object>(_data);
            if (_type != null)
            {
                result.Insert(0, new JsonData(fileData, _type));
            }
            else
            {
                //whole file is the data
                //var result = new List<object>(_data);
                if (type == null)
                {
                    // This will return a JObject.
                    result.Insert(0, JsonConvert.DeserializeObject<object>(fileData));
                }
                else
                {
                    result.Insert(0, JsonConvert.DeserializeObject(fileData, type));
                }
            }

            // maybe think of https://stackoverflow.com/questions/17519078/initializing-a-generic-variable-from-a-c-sharp-type-variable...
            // however it's not working well yet.
            return new[] { result.ToArray() };
        }
    }
}
