using NUnit.Framework;
using SpecFlowPorjectX.Pages;
using SpecFlowPorjectX.Support;

namespace SpecFlowPorjectX.StepDefinitions {
    [Binding, Scope(Feature = "Inventory Page Functionality")]
    public class InventoryStepDefinitions {
        private readonly IWebDriver driver;

        public InventoryStepDefinitions(IWebDriver driver) => this.driver = driver;
        public LoginPage LoginPage => new(driver);
        public InventoryPage InventoryPage;
        public SauceLabsPage SauceLabsPage;

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage() {
            driver.Url = Utilities.BaseURL;
            Assert.AreEqual(Utilities.BaseURL, driver.Url);
            Assert.AreEqual("Swag Labs", driver.Title);
        }

        [Given(@"I log in with valid credentials")]
        public void GivenILogİnWithValidCredentials() {
            LoginPage.SetUserName("standard_user");
            LoginPage.SetPassword("secret_sauce");
            InventoryPage = LoginPage.ClickLogin();
        }

        [Given(@"I am on the inventory page")]
        public void GivenIAmOnTheİnventoryPage() {
            Assert.AreEqual("https://www.saucedemo.com/inventory.html", InventoryPage.GetUrl);
            Assert.AreEqual("Swag Labs", InventoryPage.GetTitle);
            Assert.AreEqual("Products", InventoryPage.GetProductsSpan);
        }

        [When(@"I add a product to the cart")]
        public void WhenIAddAProductToTheCart() {
            InventoryPage.SelectRandomItemOnInventoryPage();
        }

        [Then(@"the product should be added to the cart")]
        public void ThenTheProductShouldBeAddedToTheCart() {
            Assert.AreEqual(1, InventoryPage.GetCartBadgeCount, "Item does not added to cart");
        }

        [When(@"I open the filter menu")]
        public void WhenIOpenTheFilterMenu() {
            InventoryPage.ClickFilterItemElement();
        }

        [When(@"I select ""([^""]*)"" filter")]
        public void WhenISelectFilter(string filter) {
            InventoryPage.SelectItemFilter(filter);
            Thread.Sleep(1500);
        }

        [Then(@"the products should be sorted by name in ascending order")]
        public void ThenTheProductsShouldBeSortedByNameİnAscendingOrder() {
            bool check = InventoryPage.GetFirstItemName.CompareTo(InventoryPage.GetLastItemName) < 0;
            Assert.IsTrue(check, "Products are not sorted by name in ascending order");
        }

        [Then(@"the products should be sorted by name in descending order")]
        public void ThenTheProductsShouldBeSortedByNameİnDescendingOrder() {
            bool check = InventoryPage.GetFirstItemName.CompareTo(InventoryPage.GetLastItemName) > 0;
            Assert.True(check, "Products are not sorted by name in descending order");
        }

        [Then(@"the products should be sorted by price in ascending order")]
        public void ThenTheProductsShouldBeSortedByPriceİnAscendingOrder() {
            bool check = InventoryPage.GetFirstItemPrice < InventoryPage.GetLastItemPrice;
            Assert.True(check, "Products are not sorted by price in ascending order");
        }

        [Then(@"the products should be sorted by price in descending order")]
        public void ThenTheProductsShouldBeSortedByPriceİnDescendingOrder() {
            bool check = InventoryPage.GetFirstItemPrice > InventoryPage.GetLastItemPrice;
            Assert.True(check, "Products are not sorted by price in ascending order");
        }

        [When(@"I open the burger menu")]
        public void WhenIOpenTheBurgerMenu() {
            InventoryPage.ClickBurgerMenu();
        }

        [When(@"I select the about page")]
        public void WhenISelectTheAboutPage() {
            SauceLabsPage = InventoryPage.ClickBurgerMenuAbout();
        }

        [Then(@"I should be on the about page")]
        public void ThenIShouldBeOnTheAboutPage() {
            Assert.AreEqual("Sauce Labs: Cross Browser Testing, Selenium Testing & Mobile Testing", SauceLabsPage.GetTitle);
            Assert.AreEqual("https://saucelabs.com/", SauceLabsPage.GetUrl);
        }

        [When(@"I select the reset app state")]
        public void WhenISelectTheResetAppState() {
            InventoryPage.ClickBurgerMenuResetAppState();
        }

        [Then(@"the cart should be empty")]
        public void ThenTheCartShouldBeEmpty() {
            var count = InventoryPage.GetCardItemCount;
            Assert.AreEqual(1, count);
        }


    }
}
