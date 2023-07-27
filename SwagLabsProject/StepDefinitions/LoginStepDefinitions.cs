using System.Diagnostics;
using NUnit.Framework;
using SpecFlowPorjectX.Pages;
using SpecFlowPorjectX.Support;

namespace SpecFlowPorjectX.StepDefinitions {
    [Binding, Scope(Feature = "Login Page Functionality")]
    public class LoginStepDefinitions {
        private readonly IWebDriver driver;
        private Stopwatch stopwatch;
        public LoginStepDefinitions(IWebDriver driver) => this.driver = driver;

        public LoginPage LoginPage => new(driver);
        public InventoryPage InventoryPage;

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage() {
            driver.Url = Utilities.BaseURL;
            Assert.AreEqual(Utilities.BaseURL, driver.Url);
            Assert.AreEqual("Swag Labs", driver.Title);
        }

        [When(@"I enter the username ""([^""]*)""")]
        public void WhenIEnterTheUsername(string userName) {
            LoginPage.SetUserName(userName);
        }

        [When(@"I enter an invalid username ""([^""]*)""")]
        public void WhenIEnterAnİnvalidUsername(string inValidUserName) {
            LoginPage.SetUserName(inValidUserName);
        }

        [When(@"I leave the username field empty")]
        public void WhenILeaveTheUsernameFieldEmpty() {
            LoginPage.SetUserName("");
        }


        [When(@"I leave the password field empty")]
        public void WhenILeaveThePasswordFieldEmpty() {
            LoginPage.SetPassword("");
        }


        [When(@"I leave both username and password fields empty")]
        public void WhenILeaveBothUsernameAndPasswordFieldsEmpty() {
            LoginPage.SetUserName("");
            LoginPage.SetPassword("");
        }



        [When(@"I enter the password ""([^""]*)""")]
        public void WhenIEnterThePassword(string password) {
            LoginPage.SetPassword(password);
        }

        [When(@"I click on the login button")]
        public void WhenIClickOnTheLoginButton() {
            InventoryPage = LoginPage.ClickLogin();
        }

        [Then(@"I should be redirected to the inventory page")]
        public void ThenIShouldBeRedirectedToTheHomePage() {
            Assert.AreEqual("https://www.saucedemo.com/inventory.html", InventoryPage.GetUrl);
            Assert.AreEqual("Swag Labs", InventoryPage.GetTitle);
            Assert.AreEqual("Products", InventoryPage.GetProductsSpan);
        }

        [Then(@"I should see an error message indicating a locked account")]
        public void ThenIShouldSeeAnErrorMessageİndicatingALockedAccount() {
            Assert.AreEqual("Epic sadface: Sorry, this user has been locked out.", LoginPage.GetTitleOfError);
        }

        [Then(@"I should see an error message indicating a missing password")]
        public void ThenIShouldSeeAnErrorMessageİndicatingAMissingPassword() {
            Assert.AreEqual("Epic sadface: Password is required", LoginPage.GetTitleOfError);
        }


        [Then(@"I should see an error message indicating an invalid username")]
        [Then(@"I should see an error message indicating an incorrect password")]
        public void ThenIShouldSeeAnErrorMessageİndicatingAnİncorrectPassword() {
            Assert.AreEqual("Epic sadface: Username and password do not match any user in this service", LoginPage.GetTitleOfError);
        }

        [Then(@"I should see an error message indicating a missing username")]
        public void ThenIShouldSeeAnErrorMessageİndicatingAMissingUsername() {
            Assert.AreEqual("Epic sadface: Username is required", LoginPage.GetTitleOfError);
        }


        [When(@"I click on the login button multiple times")]
        public void WhenIClickOnTheLoginButtonMultipleTimes() {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            LoginPage.ClickLogin();
        }

        [Then(@"I should see an error message indicating a performance issue")]
        public void ThenIShouldSeeAnErrorMessageİndicatingAPerformanceİssue() {
            stopwatch.Stop();
            const int performanceThreshold = 5000;
            Assert.IsTrue(stopwatch.ElapsedMilliseconds >= performanceThreshold, "Login took longer than 5 seconds.");
        }

        [Then(@"I should see an error message indicating missing credentials")]
        public void ThenIShouldSeeAnErrorMessageİndicatingMissingCredentials() {
            Assert.AreEqual("Epic sadface: Username is required", LoginPage.GetTitleOfError);
            Assert.IsTrue(LoginPage.ErrorUserNameIsVisible);
            Assert.IsTrue(LoginPage.ErrorPasswordIsVisible);
        }

        [When(@"I click on the menu button")]
        public void WhenIClickOnTheMenuButton() {
            InventoryPage.ClickBurgerMenu();
        }
        [When(@"I click on the logout button")]
        public void WhenIClickOnTheLogoutButton() {
            InventoryPage.ClickBurgerMenuLogout();
        }

        [Then(@"I should be redirected to the login page")]
        public void ThenIShouldBeRedirectedToTheLoginPage() {
            Assert.AreEqual("https://www.saucedemo.com/", LoginPage.GetUrl);
            Assert.IsTrue(LoginPage.IsVisibleBtnLogin);
        }




    }
}
