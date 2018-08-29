[![GitHub](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/Nordes/HoNoSoFt.XUnit.Extensions/blob/master/LICENSE) [![Build status](https://ci.appveyor.com/api/projects/status/oxhhrmmf711syrkv?svg=true)](https://ci.appveyor.com/project/Nordes/honosoft-xunit-extensions) [![NuGet](https://img.shields.io/nuget/v/HoNoSoFt.XUnit.Extensions.svg)](https://www.nuget.org/packages/HoNoSoFt.XUnit.Extensions) [![NuGet](https://img.shields.io/nuget/dt/HoNoSoFt.XUnit.Extensions.svg)](https://www.nuget.org/packages/HoNoSoFt.XUnit.Extensions)


# HoNoSoFt.XUnit.Extensions
XUnit extensions in order to have new attributes.

## Attribute JsonFileData
This attribute is in order to use Json file. This attribute can be used in two different way.

1. The easy and strongly typed version
    * Issue that can't be fixed due to XUnit framework: The display in test explorer will show 1, and all tests are combined. Basically it's because we use a strongly typed object and that the framework can't allow that.
2. The bit more step, but somewhat strongly typed
    * Issue: Require a bit more typing

### Lazy version
This will display as one global tests, if ran in command line, all tests will be counted properly.

```csharp
public class MyTests {
  [Theory]
  [JsonFileData("./assets/sample.json", "data")]
  [JsonFileData("./assets/sample.json", "data2")]
  public void JsonFileAttribute(Sample mySample, string expectedResult)
  {
      Assert.Equal(expectedResult, mySample.SampleProp);
  }
}

public class Sample
{
    public string SampleProp { get; set; }
}
```

### Discoverable in test explorer version
This will display as multiple global tests (one per JsonFileData), if ran in command line, all tests will be counted properly.

```csharp
public class MyTests {

  [Theory]
  [JsonFileData("./assets/sample.json", typeof(Sample), "data")]
  [JsonFileData("./assets/sample2.json", typeof(Sample), "data2")]
  public void JsonFileAttribute(JsonData mySpecialJson, string expectedResult)
  {
      Assert.Equal(typeof(Sample), mySpecialJson.Data.GetType());
      Assert.Equal(expectedResult, (mySample.Data as Sample).SampleProp);
  }
}

public class Sample
{
    public string SampleProp { get; set; }
}
```