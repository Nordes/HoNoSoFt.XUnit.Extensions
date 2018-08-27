using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Xunit.Sdk;

namespace HoNoSoFt.XUnit.Extensions
{
    /// <summary>
    /// Come from:
    /// https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/
    /// </summary>
    //[CLSCompliant(false)]
    //[DataDiscoverer("HoNoSoFt.XUnit.Extensions.ScrapDiscoverer", "HoNoSoFt.XUnit.Extensions")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ScrapAttribute : DataAttribute
    {
        private readonly object[] _data;

        /// <summary>
        /// Load data from a JSON file as the data source for a theory
        /// </summary>
        /// <param name="filePath">The absolute or relative path to the JSON file to load</param>
        /// <param name="data">The parameters.</param>
        public ScrapAttribute(params object[] data)
        {
            _data = data;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            //if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }
            //var type = testMethod.GetParameters()[0].ParameterType;
            //var result = new List<object>();
            //string abc = "{\"SampleProp\": \"value\"}";
            //result.Add(JsonConvert.DeserializeObject<Sample>(abc));
            Sample s = JsonConvert.DeserializeObject<Sample>((string)_data[0]);

            var test = new JsonData<Sample>((string)_data[0]);
            yield return new object[] { test };
        }
    }

    public class Sample : ISerializable
    {
        public Sample() { }
        public string SampleProp { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SampleProp", SampleProp);
        }

        public override string ToString()
        {
            return SampleProp;
        }
    }
}
