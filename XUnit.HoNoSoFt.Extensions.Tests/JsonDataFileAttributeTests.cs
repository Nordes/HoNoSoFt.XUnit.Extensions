using System;
using Xunit;

namespace XUnit.HoNoSoFt.Extensions.Tests
{
    public class UnitTest1
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

        [Theory]
        [JsonFileData("./assets/sample.json", "data")]
        [JsonFileData("./assets/sample2.json", "data2")]
        public void JsonFileAttribute_WhenAttributeMultiple_ExpectProperDiscovery(Sample mySample, string expectedResult)
        {
            Assert.Equal(expectedResult, mySample.SampleProp);
        }

        public class Sample
        {
            public string SampleProp { get; set; }
        }
    }
}
