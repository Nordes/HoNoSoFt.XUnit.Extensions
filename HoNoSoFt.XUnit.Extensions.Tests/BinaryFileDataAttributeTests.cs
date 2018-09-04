using System;
using System.IO;
using System.Text;
using Xunit;

namespace HoNoSoFt.XUnit.Extensions.Tests
{
    public class BinaryFileDataAttributeTests
    {
        [Theory]
        [BinaryFileData("./assets/sample.bin")]
        public void FileData_Base(byte[] sampleContent)
        {
            Assert.NotNull(sampleContent);
            // Since I used 4 bytes int then we need to split by 4 bytes
            Assert.Equal(0, sampleContent.Length % 4);
            var sb = new StringBuilder();
            var bytes = new Span<byte>(sampleContent);

            for (var i = 0; i < sampleContent.Length; i += 4)
            {
                Span<byte> splitBytes = bytes.Slice(i, 4);
                int res = BitConverter.ToInt32(splitBytes);
                sb.Append((char)res);
            }

            Assert.Equal("HELLO WORLD", sb.ToString());
        }

        [Theory]
        [BinaryFileData("./assets/sample.bin", "HELLO WORLD")]
        public void FileData_BaseUsingExtraParams(byte[] sampleContent, string expected)
        {
            Assert.NotNull(sampleContent);
            // Since I used 4 bytes int then we need to split by 4 bytes
            Assert.Equal(0, sampleContent.Length % 4);
            var sb = new StringBuilder();
            var bytes = new Span<byte>(sampleContent);

            for (var i = 0; i < sampleContent.Length; i += 4)
            {
                Span<byte> splitBytes = bytes.Slice(i, 4);
                int res = BitConverter.ToInt32(splitBytes);
                sb.Append((char)res);
            }

            Assert.Equal(expected, sb.ToString());
        }
    }
}
