using NUnit.Framework;
using SpecFlowPorjectX.Pages;
using SpecFlowPorjectX.Support;

namespace SpecFlowPorjectX.StepDefinitions {
    [Binding, Scope(Feature = "Cart Page Functionality")]
    public class CartStepDefinitions {

        private readonly IWebDriver webDriver;
        private int ItemCount = 0;
        public CartStepDefinitions(IWebDriver webDriver) => this.webDriver = webDriver;

        public LoginPage LoginPage => new(webDriver);
        public InventoryPage InventoryPage;
        public CartPage CartPage;
        public CheckOutPage CheckOutPage;

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage() {
            webDriver.Url = Utilities.BaseURL;
            Assert.AreEqual(Utilities.BaseURL, webDriver.Url);
            Assert.AreEqual("Swag Labs", webDriver.Title);
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

        [Given(@"I am on the cart page")]
        public void GivenIAmOnTheCartPage() {
            CartPage = new CartPage(this.webDriver);
            Assert.AreEqual("https://www.saucedemo.com/cart.html", CartPage.GetUrl);
            Assert.AreEqual("Your Cart", CartPage.GetTitle);
        }

        [When(@"I click cart icon")]
        public void WhenIClickCartİcon() {
            InventoryPage.ClickCardItemIcon();
        }


        [Then(@"I should see the total number of items in the cart")]
        public void ThenIShouldSeeTheTotalNumberOfİtemsİnTheCart() {
            ItemCount = CartPage.GetCartItemCount;
            Assert.AreEqual(InventoryPage.GetCartBadgeCount, CartPage.GetCartItemCount);
        }

        [When(@"I remove an item from the cart")]
        public void WhenIRemoveAnİtemFromTheCart() {
            CartPage.RemoveRandomItemOnCart();
        }

        [Then(@"the item should be removed from the cart")]
        public void ThenTheİtemShouldBeRemovedFromTheCart() {
            Assert.AreEqual(ItemCount - 1, CartPage.GetCartItemCount);
        }

        [When(@"I back to the inventory page")]
        public void WhenIBackToTheİnventoryPage() {
            CheckOutPage = new(webDriver);
            CheckOutPage.ClickContinueShoppingBtn();
        }

        [When(@"I click on the checkout button")]
        public void WhenIClickOnTheCheckoutButton() {
            CheckOutPage = CartPage.ClickCheckOutBtnElement();
        }

        [Then(@"I should be on the checkout page")]
        public void ThenIShouldBeOnTheCheckoutPage() {
            Thread.Sleep(2500);
            Assert.AreEqual("https://www.saucedemo.com/checkout-step-one.html", CheckOutPage.GetUrl);
            Assert.AreEqual("Checkout: Your Information", CheckOutPage.GetTitle);
        }


    }
}
