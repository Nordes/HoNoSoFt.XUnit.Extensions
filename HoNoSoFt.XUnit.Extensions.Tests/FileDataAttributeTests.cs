using System.Text;
using Xunit;

namespace HoNoSoFt.XUnit.Extensions.Tests
{
    public class FileDataAttributeTests
    {
        [Theory]
        [FileData("./assets/sample.txt")]
        public void FileData_Base(string sampleContent)
        {
            var sb = new StringBuilder();
            sb.AppendLine("this");
            sb.AppendLine("is");
            sb.AppendLine("some");
            sb.AppendLine("kind");
            sb.AppendLine("of sample");
            sb.Append("");

            Assert.NotNull(sampleContent);
            Assert.Equal(sb.ToString(), sampleContent);
        }

        [Theory]
        [FileData("./assets/sample2.txt")]
        public void FileData_WhenSpace_ExpectSpacesToBeKept(string sampleContent)
        {
            var sb = new StringBuilder();
            sb.AppendLine("i");
            sb.AppendLine(" have");
            sb.AppendLine("  spacified ");
            sb.AppendLine("   the");
            sb.Append("    line");

            Assert.NotNull(sampleContent);
            Assert.Equal(sb.ToString(), sampleContent);
        }
    }
}
