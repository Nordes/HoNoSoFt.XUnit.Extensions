using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace HoNoSoFt.XUnit.Extensions
{
    /// <summary>
    /// Read file content, nothing more.
    /// </summary>
    /// <seealso cref="Xunit.Sdk.DataAttribute" />
    [CLSCompliant(false)]
    [DataDiscoverer("Xunit.Sdk.DataDiscoverer", "xunit.core")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BinaryDataAttribute : DataAttribute
    {
        private readonly object[] _data;
        private string _filePath;

        /// <inheritdoc />
        public BinaryDataAttribute(string filePath, params object[] data)
        {
            _filePath = filePath;
            _data = data;
        }

        /// <inheritDoc />
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }

            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Directory.GetCurrentDirectory() + "/" + _filePath;
            // Original code (Core 2.1, can't work in Standard2.0, maybe when 2.1 arrives) :(
            ////: Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);

            if (!File.Exists(path))
            {
                throw new ArgumentException($"Could not find file at path: {path}");
            }

            var fileData = File.ReadAllBytes(_filePath);
            var result = new List<object>(_data);
            result.Insert(0, fileData);

            return new List<object[]> { result.ToArray() };
        }
    }
}
