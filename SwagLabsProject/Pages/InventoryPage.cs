using OpenQA.Selenium.Support.UI;
using SpecFlowPorjectX.Support;

namespace SpecFlowPorjectX.Pages {
    public class InventoryPage {
        private readonly IWebDriver webDriver;
        public InventoryPage(IWebDriver webDriver) => this.webDriver = webDriver;

        private IWebElement SpanOfProducts => webDriver.FindElement(By.XPath("//span[text()='Products']"));
        private IWebElement SpanCartBadge => webDriver.FindElement(By.XPath("//span[@class='shopping_cart_badge']"));
        private IList<IWebElement> ItemsOnInventoryPage => webDriver.FindElements(By.XPath("//button[text()='Add to cart']"));
        private IWebElement SelectFilterItem => webDriver.FindElement(By.ClassName("product_sort_container"));
        private IList<IWebElement> ItemName => webDriver.FindElements(By.ClassName("inventory_item_name"));
        private IList<IWebElement> ItemPrice => webDriver.FindElements(By.ClassName("inventory_item_price"));
        private IWebElement BurgerMenuItem => webDriver.FindElement(By.Id("react-burger-menu-btn"));
        private IWebElement BurgerMenuResetAppStateItem => webDriver.FindElement(By.XPath("//div[@class='bm-menu']//a[text()='Reset App State']"));
        private IWebElement BurgerMenuLogoutItem => webDriver.FindElement(By.XPath("//div[@class='bm-menu']//a[text()='Logout']"));
        private IWebElement BurgerMenuAbout => webDriver.FindElement(By.XPath("//div[@class='bm-menu']//a[text()='About']"));
        private IList<IWebElement> CardItemComponent => webDriver.FindElements(By.XPath("//div[@id='shopping_cart_container']//a"));
        private IWebElement CardItemIcon => webDriver.FindElement(By.Id("shopping_cart_container"));


        public string GetTitle => webDriver.Title;
        public string GetProductsSpan => SpanOfProducts.Text;
        public int GetCardItemCount => CardItemComponent.Count;
        public string GetUrl => webDriver.Url;
        public int GetItemCount => ItemsOnInventoryPage.Count;
        public int GetCartBadgeCount => Int32.Parse(SpanCartBadge.Text);
        public string GetFirstItemName => ItemName[0].Text;
        public string GetLastItemName => ItemName[ItemName.Count - 1].Text;
        public decimal GetFirstItemPrice => Convert.ToDecimal(ItemPrice[0].Text.Substring(1));
        public decimal GetLastItemPrice => Convert.ToDecimal(ItemPrice[ItemPrice.Count - 1].Text.Substring(1));

        public void ClickCardItemIcon() => CardItemIcon.Click();
        public void ClickBurgerMenuLogout() => BurgerMenuLogoutItem.Click();
        //public void ClickBurgerMenuResetAppState() => BurgerMenuResetAppStateItem.Click();
        public void ClickBurgerMenu() => BurgerMenuItem.Click();
        public void ClickFilterItemElement() => SelectFilterItem.Click();


        public void ClickBurgerMenuResetAppState() {
            BurgerMenuResetAppStateItem.WaitForElementToBeVisible(webDriver);
            BurgerMenuResetAppStateItem.Click();
        }

        public void SelectItemFilter(string text) {
            SelectElement select = new(SelectFilterItem);
            select.SelectByText(text);
        }
        public SauceLabsPage ClickBurgerMenuAbout() {
            Thread.Sleep(3000);
            BurgerMenuAbout.Click();
            return new SauceLabsPage(webDriver);
        }

        public void SelectRandomItemOnInventoryPage() {
            Random random = new();
            int randomItem = random.Next(0, GetItemCount - 1);
            ItemsOnInventoryPage[randomItem].Click();
        }


    }
}
