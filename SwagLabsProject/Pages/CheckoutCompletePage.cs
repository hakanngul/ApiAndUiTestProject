namespace SpecFlowPorjectX.Pages {
    public class CheckoutCompletePage {
        private readonly IWebDriver webDriver;
        public CheckoutCompletePage(IWebDriver webDriver) => this.webDriver = webDriver;

        private IWebElement OrderMessage => webDriver.FindElement(By.ClassName("complete-header"));
        private IWebElement OrderCompleteMessage => webDriver.FindElement(By.ClassName("complete-text"));
        private IWebElement BackToHomeBtn => webDriver.FindElement(By.Id("back-to-products"));
        public string GetOrderMessage => OrderMessage.Text;
        public string GetOrderCompleteMessage => OrderCompleteMessage.Text;
        public string GetUrl => webDriver.Url;
        public void ClickBackToHomeBtn() => BackToHomeBtn.Click();
    }
}
