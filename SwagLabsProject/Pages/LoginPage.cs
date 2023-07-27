using SpecFlowPorjectX.Support;

namespace SpecFlowPorjectX.Pages {
    public class LoginPage {
        private readonly IWebDriver webDriver;
        public LoginPage(IWebDriver webDriver) => this.webDriver = webDriver;


        private IWebElement inputUserName => webDriver.FindElement(By.Id("user-name"));
        private IWebElement inputPassword => webDriver.FindElement(By.Id("password"));
        private IWebElement btnLogin => webDriver.FindElement(By.Id("login-button"));
        private IWebElement titleOfLockedError => webDriver.FindElement(By.XPath("//h3[@data-test='error']"));
        private IWebElement inputErrorUserName => webDriver.FindElement(By.XPath("//input[@class='input_error form_input error' and @id='user-name']"));
        private IWebElement inputErrorPassword => webDriver.FindElement(By.XPath("//input[@class='input_error form_input error' and @id='password']"));

        public bool ErrorUserNameIsVisible => inputErrorUserName.IsVisible(webDriver);
        public bool ErrorPasswordIsVisible => inputErrorPassword.IsVisible(webDriver);
        public void SetUserName(string userName) => inputUserName.SendKeys(userName);
        public void SetPassword(string password) => inputPassword.SendKeys(password);
        public string GetTitleOfError => titleOfLockedError.Text;
        public string GetUrl => webDriver.Url;
        public bool IsVisibleBtnLogin => btnLogin.IsVisible(webDriver);

        public InventoryPage ClickLogin() {
            btnLogin.Click();
            return new InventoryPage(webDriver);
        }


    }
}
