using Xunit;

namespace HoNoSoFt.XUnit.Extensions.Tests
{
    public class JsonDataFileAttributeTests
    {
        [Theory]
        [JsonFileData("./assets/sample.json")]
        public void JsonFileAttribute(Sample mySample)
        {
            Assert.Equal("data", mySample.SampleProp);
        }

        [Theory]
        [JsonFileData("./assets/sample.json", "data")]
        public void JsonFileAttribute_WhenMultipleParameter_ExpectExtraParametersToDisplay(Sample mySample, string expectedResult)
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
        public void JsonFileAttribute_WhenAttributeMultiple_ExpectProperDiscoveryButNotVisibleInTestExplorer(Sample mySample, string expectedResult)
        {
            Assert.Equal(expectedResult, mySample.SampleProp);
        }

        /// <summary>
        /// Jsons the file attribute when attribute multiple expect visible in test explorer.
        /// </summary>
        /// <param name="mySample">My sample.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [JsonFileData("./assets/sample.json", typeof(Sample), "data")]
        [JsonFileData("./assets/sample2.json", typeof(Sample), "data2")]
        public void JsonFileAttribute_WhenAttributeMultiple_ExpectVisibleInTestExplorer(JsonData mySample, string expectedResult)
        {
            Assert.Equal(typeof(Sample), mySample.Data.GetType());
            Assert.Equal(expectedResult, (mySample.Data as Sample).SampleProp);
        }
    }

    public class Sample
    {
        public string SampleProp { get; set; }
    }
}
