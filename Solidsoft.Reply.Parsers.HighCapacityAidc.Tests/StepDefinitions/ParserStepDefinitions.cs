using Solidsoft.Reply.Parsers.HighCapacityAidc;

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.Tests.StepDefinitions;

[Binding]
public sealed class ParserStepDefinitions {

    private string _input = string.Empty;
    private IBarcode? _barcode = null;

    [Given(@"I have a GS1 element string ""(.*)""")]
    public void GivenIHaveAGS1ElementString(string input) {
        _input = input.Replace("<GS>", ((char)29).ToString());
    }

    [Given(@"I have an MH10.8.2 element string ""(.*)""")]
    public void GivenIHaveAnMH1082ElementString(string input) {
        _input = input.Replace("<GS>", ((char)29).ToString())
                      .Replace("<RS>", ((char)30).ToString())
                      .Replace("<EOT>", ((char)04).ToString());
    }

    [Given(@"I have an EAN element string ""(.*)""")]
    public void GivenIHaveAnEan13ElementString(string input) {
        _input = input;
    }

    [Given(@"I have a UPC-E element string ""(.*)""")]
    public void GivenIHaveAUpcEElementString(string input) {
        _input = input;
    }

    [Given(@"I have an ITF14 element string ""(.*)""")]
    public void GivenIHaveAnItf14ElementString(string input) {
        _input = input;
    }

    [When(@"I extract AIs and values")]
    public void WhenIExtractAIsAndValues() {
        _barcode = Parser.Parse(_input);
    }

    [When(@"I extract DIs and values")]
    public void WhenIExtractDIsAndValues() {
        _barcode = Parser.Parse(_input);
    }

    [When(@"I extract the product code value")]
    public void WhenIExtractProductCodeValue() {
        _barcode = Parser.Parse(_input);
    }

    [Then(@"the result should contain:")]
    public void ThenTheResultShouldContain(Table table) {
        Assert.NotNull(_barcode);

        foreach (var row in table.Rows) {
            var ai = row["AI"];
            var expectedValue = row["Value"];
            Assert.True(_barcode.DataElements.Any(e => ((DataElement)e).Identifier == ai), $"Result should contain AI {ai}");
            Assert.Equal(expectedValue, ((DataElement)_barcode.DataElements.First(e => ((DataElement)e).Identifier == ai)).Data);
        }
    }

    [Then(@"the MH10.8.2 result should contain:")]
    public void ThenTheMH1082ResultShouldContain(Table table) {
        Assert.NotNull(_barcode);

        foreach (var row in table.Rows) {
            var di = row["DI"];
            var expectedValue = row["Value"];
            Assert.True(_barcode.DataElements.Any(e => ((DataElement)e).Identifier == di), $"Result should contain DI {di}");
            Assert.Equal(expectedValue, ((DataElement)_barcode.DataElements.First(e => ((DataElement)e).Identifier == di)).Data);
        }
    }

    [Then(@"the EAN result should contain:")]
    public void ThenTheEanResultShouldContain(Table table) {
        Assert.NotNull(_barcode);

        foreach (var row in table.Rows) {
            var ai = row["AI"];
            var expectedValue = row["Value"];
            Assert.True(_barcode.DataElements.Any(e => ((DataElement)e).Identifier == ai), $"Result should contain AI {ai}");
            Assert.Equal(expectedValue, ((DataElement)_barcode.DataElements.First(e => ((DataElement)e).Identifier == ai)).Data);
        }
    }

    [Then(@"the UPC-E result should contain:")]
    public void ThenTheUpcEResultShouldContain(Table table) {
        Assert.NotNull(_barcode);

        foreach (var row in table.Rows) {
            var ai = row["AI"];
            var expectedValue = row["Value"];
            Assert.True(_barcode.DataElements.Any(e => ((DataElement)e).Identifier == ai), $"Result should contain AI {ai}");
            Assert.Equal(expectedValue, ((DataElement)_barcode.DataElements.First(e => ((DataElement)e).Identifier == ai)).Data);
        }
    }

    [Then(@"the ITF14 result should contain:")]
    public void ThenTheItf14ResultShouldContain(Table table) {
        Assert.NotNull(_barcode);

        foreach (var row in table.Rows) {
            var ai = row["AI"];
            var expectedValue = row["Value"];
            Assert.True(_barcode.DataElements.Any(e => ((DataElement)e).Identifier == ai), $"Result should contain AI {ai}");
            Assert.Equal(expectedValue, ((DataElement)_barcode.DataElements.First(e => ((DataElement)e).Identifier == ai)).Data);
        }
    }
}