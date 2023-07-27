namespace SpecFlowPorjectX.Pages {
    public class CheckOutPage {
        private readonly IWebDriver webDriver;
        public CheckOutPage(IWebDriver webDriver) => this.webDriver = webDriver;
        private IWebElement TitleOfCheckOutPage => webDriver.FindElement(By.ClassName("title"));
        private IWebElement InputFirstName => webDriver.FindElement(By.Id("first-name"));
        private IWebElement InputLastName => webDriver.FindElement(By.Id("last-name"));
        private IWebElement InputZipCode => webDriver.FindElement(By.Id("postal-code"));
        private IWebElement titleOfLockedError => webDriver.FindElement(By.XPath("//h3[@data-test='error']"));
        private IWebElement ContinueShoppingBtn => webDriver.FindElement(By.Id("continue-shopping"));
        private IWebElement ContinueBtn => webDriver.FindElement(By.Id("continue"));


        public string GetUrl => webDriver.Url;
        public string GetTitle => TitleOfCheckOutPage.Text;
        public string GetTitleOfError => titleOfLockedError.Text;

        public void ClickContinueShoppingBtn() => ContinueShoppingBtn.Click();
        public void ClickContinueBtn() => ContinueBtn.Click();
        public void SetFirtNameInput(string value) => InputFirstName.SendKeys(value);

        public void SetLastNameInput(string value) => InputLastName.SendKeys(value);

        public void SetPostCodeInput(string value) => InputZipCode.SendKeys(value);

    }
}
