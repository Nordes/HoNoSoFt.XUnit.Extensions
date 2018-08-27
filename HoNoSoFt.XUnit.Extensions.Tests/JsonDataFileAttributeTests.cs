using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace HoNoSoFt.XUnit.Extensions.Tests
{
    public class JsonDataFileAttributeTests
    {
        //[Theory]
        //[JsonFileData("./assets/sample.json")]
        //public void JsonFileAttribute(Sample mySample)
        //{
        //    Assert.Equal("data", mySample.SampleProp);
        //}

        //[Theory]
        //[JsonFileData("./assets/sample.json", "data")]
        //public void JsonFileAttribute_WhenMultipleParameter_ExpectExtraParametersToDisplay(Sample mySample, string expectedResult)
        //{
        //    Assert.Equal(expectedResult, mySample.SampleProp);
        //}

        [Theory]
        [Scrap("{ \"sampleProp\": \"ascrap\", \"fakeProp\": \"fakePropdata\"}")]
        [Scrap("{ \"sampleProp\": \"scrapthe\"}")]
        [Scrap("{ \"sampleProp\": \"scrapthefuck\"}")]
        public void Crap(JsonData<Sample> mySample)
        {
            // maybe go into the path of MemberDataAttribute... instead of dataAttribute
            // look at the base 
            // example with memberData : http://hamidmosalla.com/2017/02/25/xunit-theory-working-with-inlinedata-memberdata-classdata/
            Assert.True(true);
        }

        [Theory]
        [JsonFileData("./assets/sample.json", "data")]
        [JsonFileData("./assets/sample2.json", "data2")]
        public void JsonFileAttribute_WhenAttributeMultiple_ExpectProperDiscovery(Sample mySample, string expectedResult)
        {
            Assert.Equal(expectedResult, mySample.SampleProp);
        }
    }
}
