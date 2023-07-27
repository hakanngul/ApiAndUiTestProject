namespace SpecFlowPorjectX.Pages {
    public class CartPage {
        private readonly IWebDriver driver;
        private IWebElement TitleOfCartPageElement => driver.FindElement(By.ClassName("title"));
        private IList<IWebElement> CartItemsElement => driver.FindElements(By.ClassName("cart_item"));
        private IList<IWebElement> CartItemsRemoveBtnElement => driver.FindElements(By.XPath("//div[@class='cart_item']//button[text()='Remove']"));
        private IWebElement ContinueShoppingBtnElement => driver.FindElement(By.Id("continue-shopping"));
        private IWebElement CheckOutBtnElement => driver.FindElement(By.Id("checkout"));
        private IList<IWebElement> ItemPriceList => driver.FindElements(By.ClassName("inventory_item_price"));

        public CartPage(IWebDriver driver) => this.driver = driver;

        public string GetUrl => driver.Url;
        public string GetTitle => TitleOfCartPageElement.Text;
        public int GetCartItemCount => CartItemsElement.Count;
        public void ClickContinueShoppingBtn() => ContinueShoppingBtnElement.Click();

        public decimal TotalItemPrice() {
            decimal total = 0;
            foreach (IWebElement item in ItemPriceList) {
                total += decimal.Parse(item.Text.Replace("$", ""));
            }

            return total;
        }

        public CheckOutPage ClickCheckOutBtnElement() {
            CheckOutBtnElement.Click();
            return new CheckOutPage(driver);
        }

        public void RemoveRandomItemOnCart() {
            Random r = new();
            int randomItem = r.Next(0, CartItemsRemoveBtnElement.Count - 1);
            CartItemsRemoveBtnElement[randomItem].Click();
        }

    }
}
