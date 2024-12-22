using Solidsoft.Reply.Parsers.HighCapacityAidc;

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.Tests.StepDefinitions {
    [Binding]
    public sealed class ParserStepDefinitions {

        private string _input = string.Empty;
        private IBarcode? barcode = null;

        [Given("an input of (.*)")]
        public void GivenAnInputOf(string input) {
            _input = input;
        }

        [When("the input is parsed")]
        public void WhenTheInputIsParsed() {
            barcode = Parser.Parse(_input);
        }

        [Then("the output is valid")]
        public void ThenTheOutputIsValid() {
            Assert.NotNull(barcode);
            Assert.True(barcode.IsValid);
        }
    }
}
