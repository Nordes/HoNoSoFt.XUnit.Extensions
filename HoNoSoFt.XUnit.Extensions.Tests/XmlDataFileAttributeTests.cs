using HoNoSoFt.XUnit.Extensions.Tests.assets;
using System.IO;
using System.Linq;
using Xunit;

namespace HoNoSoFt.XUnit.Extensions.Tests
{
    public class XmlDataFileAttributeTests
    {
        [Theory]
        [XmlFileData("./assets/sample.xml")]
        public void XmlFileDataAttribute(SampleFakeObject mySample)
        {
            Assert.Equal("data", mySample.SampleProp);
        }

        [Fact]
        public void XmlFileData_XmlFileNotExists()
        {
            var wrongPath = "./path/not_exists.xml";
            var expectedErrorMessage = $"Could not find the XML file located at: {Directory.GetCurrentDirectory()}/{wrongPath}";
            var currentMethod = typeof(XmlDataFileAttributeTests).GetMethods().First(m => m.Name == nameof(XmlFileData_XmlFileNotExists));

            var test = new XmlFileDataAttribute(wrongPath);

            var exception = Assert.Throws<FileNotFoundException>(() => test.GetData(currentMethod));
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Theory]
        [XmlFileData("./assets/sample.xml", "data")]
        public void XmlFileData_WhenMultipleParameter_ExpectExtraParametersToDisplay(SampleFakeObject mySample, string expectedResult)
        {
            Assert.Equal(expectedResult, mySample.SampleProp);
        }

        /// <summary>
        /// Tests ok, however, not displayed in normal Test Explorer as separated tests
        /// </summary>
        /// <param name="mySample">My sample.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [XmlFileData("./assets/sample.xml", "data")]
        [XmlFileData("./assets/sample2.xml", "data2")]
        public void XmlFileData_WhenMultipleNotTyped_ExpectOneGlobalInTestExplorer(SampleFakeObject mySample, string expectedResult)
        {
            Assert.Equal(expectedResult, mySample.SampleProp);
        }

        /// <summary>
        /// Xmls the file attribute when attribute multiple expect visible in test explorer.
        /// </summary>
        /// <param name="mySample">My sample.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [XmlFileData("./assets/sample.xml", typeof(SampleFakeObject), "data")]
        [XmlFileData("./assets/sample2.xml", typeof(SampleFakeObject), "data2")]
        public void XmlFileData_WhenMultipleNotTyped_ExpectAllVisibleInTestExplorer(XmlData mySample, string expectedResult)
        {
            Assert.Equal(typeof(SampleFakeObject), mySample.Data.GetType());
            Assert.Equal(expectedResult, (mySample.Data as SampleFakeObject)?.SampleProp);
        }
    }
}
