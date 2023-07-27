namespace SpecFlowPorjectX.Pages {
    public class CheckoutStepTwoPage {
        private readonly IWebDriver webDriver;
        public CheckoutStepTwoPage(IWebDriver webDriver) => this.webDriver = webDriver;

        private IList<IWebElement> ProductCount => webDriver.FindElements(By.ClassName("inventory_item_name"));
        private IWebElement TitleOfCheckOutStepTwo => webDriver.FindElement(By.ClassName("title"));
        private IWebElement ItemTotalPrice => webDriver.FindElement(By.ClassName("summary_subtotal_label"));
        private IWebElement TaxPrice => webDriver.FindElement(By.ClassName("summary_tax_label"));
        private IWebElement TotalTaxAndPrice => webDriver.FindElement(By.XPath("//div[@class='summary_info_label summary_total_label']"));
        private IWebElement FinishBtn => webDriver.FindElement(By.Id("finish"));
        public decimal GetItemTotalPrice => decimal.Parse(ItemTotalPrice.Text.Split(" ")[2].Replace("$", ""));
        public decimal GetTaxPrice => decimal.Parse(TaxPrice.Text.Split(" ")[1].Replace("$", ""));
        public decimal GetTotalTaxAndPrice => decimal.Parse(TotalTaxAndPrice.Text.Split(" ")[1].Trim().Replace("$", ""));
        public string GetUrl => webDriver.Url;
        public string GetTitle => TitleOfCheckOutStepTwo.Text;
        public int GetProductCount => ProductCount.Count;

        public CheckoutCompletePage ClickFinishBtn() {
            FinishBtn.Click();
            return new CheckoutCompletePage(webDriver);
        }
    }
}
