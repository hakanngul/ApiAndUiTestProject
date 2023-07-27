using NUnit.Framework;
using SpecFlowPorjectX.Pages;
using SpecFlowPorjectX.Support;

namespace SpecFlowPorjectX.StepDefinitions {
    [Binding, Scope(Feature = "Checkout Page Functionality")]
    public class CheckoutStepDefinitions {
        private readonly IWebDriver webDriver;

        public CheckoutStepDefinitions(IWebDriver driver) => webDriver = driver;
        public LoginPage LoginPage => new(webDriver);
        public InventoryPage InventoryPage;
        public CartPage CartPage;
        public CheckOutPage CheckOutPage;
        public CheckoutStepTwoPage CheckoutStepTwo;
        public CheckoutCompletePage CheckoutCompletePage;
        public decimal TotalPriceCheckout { get; set; }
        public decimal TotalPriceCheckoutStepTwo { get; set; }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage() {
            webDriver.Url = Utilities.BaseURL;
            Assert.AreEqual(Utilities.BaseURL, webDriver.Url);
            Assert.AreEqual("Swag Labs", webDriver.Title);
        }

        [Then(@"I am on the inventory page")]
        public void GivenIAmOnTheİnventoryPage() {
            InventoryPage = new InventoryPage(webDriver);
            Assert.AreEqual("https://www.saucedemo.com/inventory.html", InventoryPage.GetUrl);
            Assert.AreEqual("Swag Labs", InventoryPage.GetTitle);
            Assert.AreEqual("Products", InventoryPage.GetProductsSpan);
        }

        [When(@"I add a product to the cart")]
        public void WhenIAddAProductToTheCart() {
            InventoryPage.SelectRandomItemOnInventoryPage();
        }

        [When(@"I click to the cart icon")]
        public void WhenIClickToTheCartİcon() {
            InventoryPage.ClickCardItemIcon();
        }


        [Given(@"I log in with valid credentials")]
        public void GivenILogİnWithValidCredentials() {
            LoginPage.SetUserName("standard_user");
            LoginPage.SetPassword("secret_sauce");
            InventoryPage = LoginPage.ClickLogin();
        }



        [Then(@"I am on the cart page")]
        public void GivenIAmOnTheCartPage() {
            CartPage = new CartPage(webDriver);
            Assert.AreEqual("https://www.saucedemo.com/cart.html", CartPage.GetUrl);
            Assert.AreEqual("Your Cart", CartPage.GetTitle);
            TotalPriceCheckout = CartPage.TotalItemPrice();
        }

        [When(@"I click on the checkout button")]
        public void WhenIClickOnTheCheckoutButton() {
            CheckOutPage = CartPage.ClickCheckOutBtnElement();
        }

        [Then(@"I should be on the checkout page")]
        public void ThenIShouldBeOnTheCheckoutPage() {
            Assert.AreEqual("https://www.saucedemo.com/checkout-step-one.html", CheckOutPage.GetUrl);
            Assert.AreEqual("Checkout: Your Information", CheckOutPage.GetTitle);
        }

        [When(@"I enter ""([^""]*)"" into the first name field")]
        public void WhenIEnterEmptyIntoTheFirstNameField(string value) {
            CheckOutPage.SetFirtNameInput(value);
        }


        [When(@"the last name field should contain ""([^""]*)""")]
        public void WhenTheLastNameFieldShouldContain(string value) {
            CheckOutPage.SetLastNameInput(value);
        }

        [When(@"I enter postal code ""([^""]*)""")]
        public void ThenIEnterPostalCode(string value) {
            CheckOutPage.SetPostCodeInput(value);
        }

        [When(@"I click continue button")]
        public void WhenIClickContinueButton() {
            CheckOutPage.ClickContinueBtn();
        }


        [Then(@"I should see first name field required warning")]
        public void ThenIShouldSeeFirstNameFieldRequiredWarning() {
            Assert.AreEqual("Error: First Name is required", CheckOutPage.GetTitleOfError);
        }

        [Then(@"I should see last name field required warning")]
        public void ThenIShouldSeeLastNameFieldRequiredWarning() {
            Assert.AreEqual("Error: Last Name is required", CheckOutPage.GetTitleOfError);
        }

        [Then(@"I should see zip code field required warning")]
        public void ThenIShouldSeeZipCodeFieldRequiredWarning() {
            Assert.AreEqual("Error: Postal Code is required", CheckOutPage.GetTitleOfError);
        }

        [Then(@"I am on the checout step two page")]
        public void ThenIAmOnTheChecoutStepTwoPage() {
            CheckoutStepTwo = new CheckoutStepTwoPage(webDriver);
            Assert.AreEqual("https://www.saucedemo.com/checkout-step-two.html", CheckoutStepTwo.GetUrl);
            Assert.AreEqual("Checkout: Overview", CheckoutStepTwo.GetTitle);
        }

        [Then(@"I should see the price field fill")]
        public void ThenIShouldSeeThePriceFieldFill() {
            Assert.IsNotEmpty(CheckoutStepTwo.GetItemTotalPrice.ToString());
        }

        [Then(@"I should see the tax field fill")]
        public void ThenIShouldSeeTheTaxFieldFill() {
            Assert.IsNotEmpty(CheckoutStepTwo.GetTaxPrice.ToString());
        }

        [Then(@"I should see the total field is correct")]
        public void ThenIShouldSeeTheTotalFieldİsCorrect() {
            var totalPrice = CheckoutStepTwo.GetItemTotalPrice + CheckoutStepTwo.GetTaxPrice;
            Assert.AreEqual(totalPrice, Convert.ToDecimal(CheckoutStepTwo.GetTotalTaxAndPrice));
        }

        [Then(@"I click the finish button")]
        public void ThenIClickTheFinishButton() {
            CheckoutCompletePage = CheckoutStepTwo.ClickFinishBtn();
        }

        [Then(@"I should be on the checkout complete page")]
        public void ThenIShouldBeOnTheCheckoutCompletePage() {
            Assert.AreEqual("https://www.saucedemo.com/checkout-complete.html", CheckoutCompletePage.GetUrl);

        }

        [Then(@"I should see the thank you message")]
        public void ThenIShouldSeeTheThankYouMessage() {
            Assert.AreEqual("Thank you for your order!", CheckoutCompletePage.GetOrderMessage);
            Assert.AreEqual("Your order has been dispatched, and will arrive just as fast as the pony can get there!", CheckoutCompletePage.GetOrderCompleteMessage);
        }


        [Then(@"I click back to home button")]
        public void ThenIClickBackToHomeButton() {
            CheckoutCompletePage.ClickBackToHomeBtn();
        }



    }
}
