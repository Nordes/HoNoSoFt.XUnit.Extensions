using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace XUnit.HoNoSoFt.Extensions
{
    /// <summary>
    /// Come from:
    /// https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/
    /// </summary>
    public class JsonFileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        private readonly object[] _params;

        //public JsonFileDataAttribute(params object[] @params)
        //{
        //    _filePath = (string)@params[0];
        //    _params = @params.Skip(1).ToArray();
        //}

        /// <summary>
        /// Load data from a JSON file as the data source for a theory
        /// </summary>
        /// <param name="filePath">The absolute or relative path to the JSON file to load</param>
        /// <param name="params">The parameters.</param>
        public JsonFileDataAttribute(string filePath, params object[] @params)
        {
            _filePath = filePath;
            _params = @params;
        }

        /// <summary>
        /// Returns the data to be used to test the theory.
        /// </summary>
        /// <param name="testMethod">The method that is being tested</param>
        /// <returns>
        /// One or more sets of theory data. Each invocation of the test method
        /// is represented by a single object array.
        /// </returns>
        /// <exception cref="ArgumentNullException">testMethod</exception>
        /// <exception cref="ArgumentException"></exception>
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

            //whole file is the data
            var result = new List<object>(_params);
            if (type == null)
            {
                // This will return a JObject.
                result.Insert(0, JsonConvert.DeserializeObject<object>(fileData));
            }
            else
            {
                result.Insert(0, JsonConvert.DeserializeObject(fileData, type));
            }

            return new[] { result.ToArray() };
        }
    }
}
