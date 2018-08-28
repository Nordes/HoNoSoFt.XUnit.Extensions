using Xunit;

namespace HoNoSoFt.XUnit.Extensions.Tests
{
    public class JsonDataFileAttributeTests
    {
        [Theory]
        [JsonFileData("./assets/sample.json")]
        public void JsonFileDataAttribute(SampleFakeObject mySample)
        {
            Assert.Equal("data", mySample.SampleProp);
        }

        [Theory]
        [JsonFileData("./assets/sample.json", "data")]
        public void JsonFileDataAttribute_WhenMultipleParameter_ExpectExtraParametersToDisplay(SampleFakeObject mySample, string expectedResult)
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
        public void JsonFileDataAttribute_WhenAttributeMultiple_ExpectProperDiscoveryButNotVisibleInTestExplorer(SampleFakeObject mySample, string expectedResult)
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
        public void JsonFileDataAttribute_WhenAttributeMultiple_ExpectVisibleInTestExplorer(JsonData mySample, string expectedResult)
        {
            Assert.Equal(typeof(SampleFakeObject), mySample.Data.GetType());
            Assert.Equal(expectedResult, (mySample.Data as SampleFakeObject).SampleProp);
        }
    }

    public class SampleFakeObject
    {
        public string SampleProp { get; set; }
    }
}
