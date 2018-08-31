using HoNoSoFt.XUnit.Extensions.Tests.assets;
using System.IO;
using System.Linq;
using Xunit;

namespace HoNoSoFt.XUnit.Extensions.Tests
{
    public partial class JsonDataFileAttributeTests
    {
        [Theory]
        [JsonFileData("./assets/sample.json")]
        public void JsonFileData_Simple(SampleFakeObject mySample)
        {
            Assert.Equal("data", mySample.SampleProp);
        }

        [Fact]
        public void JsonFileData_JsonFileNotExists()
        {
            var wrongPath = "./path/not_exists.json";
            var expectedErrorMessage = $"Could not find the JSON file located at: {Directory.GetCurrentDirectory()}/{wrongPath}";
            var currentMethod = typeof(JsonDataFileAttributeTests).GetMethods().First(m=> m.Name == nameof(JsonFileData_JsonFileNotExists));

            var test = new JsonFileDataAttribute(wrongPath);

            var exception = Assert.Throws<FileNotFoundException>(() => test.GetData(currentMethod));
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Theory]
        [JsonFileData("{\"sampleProp\": \"data\"}")]
        public void JsonFileData_SimpleFromJsonString(SampleFakeObject mySample)
        {
            Assert.Equal("data", mySample.SampleProp);
        }

        [Theory]
        [JsonFileData("./assets/sample.json", "data")]
        public void JsonFileData_WhenMultipleParameter_ExpectExtraParametersToDisplay(SampleFakeObject mySample, string expectedResult)
        {
            Assert.Equal(expectedResult, mySample.SampleProp);
        }

        /// <summary>
        /// Tests ok, however, not displayed in normal Test Explorer as separated tests
        /// </summary>
        /// <param name="mySample">My sample.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [JsonFileData("./assets/sample.json", "data")]
        [JsonFileData("./assets/sample2.json", "data2")]
        public void JsonFileData_WhenMultipleNotTyped_ExpectOneGlobalInTestExplorer(SampleFakeObject mySample, string expectedResult)
        {
            Assert.Equal(expectedResult, mySample.SampleProp);
        }

        /// <summary>
        /// Jsons the file attribute when attribute multiple expect visible in test explorer.
        /// </summary>
        /// <param name="mySample">My sample.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [JsonFileData("./assets/sample.json", typeof(SampleFakeObject), "data")]
        [JsonFileData("./assets/sample2.json", typeof(SampleFakeObject), "data2")]
        [JsonFileData("{\"sampleProp\": \"data3\"}", typeof(SampleFakeObject), "data3")]
        public void JsonFileData_WhenMultipleNotTyped_ExpectAllVisibleInTestExplorer(JsonData mySample, string expectedResult)
        {
            Assert.Equal(typeof(SampleFakeObject), mySample.Data.GetType());
            Assert.Equal(expectedResult, (mySample.Data as SampleFakeObject)?.SampleProp);
        }
    }
}
