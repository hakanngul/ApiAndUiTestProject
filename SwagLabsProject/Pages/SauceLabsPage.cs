namespace SpecFlowPorjectX.Pages {
    public class SauceLabsPage {

        private readonly IWebDriver webDriver;

        public SauceLabsPage(IWebDriver webDriver) => this.webDriver = webDriver;


        public string GetTitle => webDriver.Title;
        public string GetUrl => webDriver.Url;
    }
}
