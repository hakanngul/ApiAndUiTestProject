using OpenQA.Selenium.Support.UI;
namespace SpecFlowPorjectX.Support {
    public static class WebElementExtensions {
        public static bool IsVisible(this IWebElement element, IWebDriver driver) {
            try {
                // Check if the element is displayed and has a size greater than 0
                if (element.Displayed && element.Size.Height > 0 && element.Size.Width > 0) {
                    // Additional check: Verify if the element is within the visible viewport
                    bool isDisplayedInViewPort = ((bool)((IJavaScriptExecutor)driver).ExecuteScript(@"
                    var rect = arguments[0].getBoundingClientRect();
                    return (
                        rect.top >= 0 &&
                        rect.left >= 0 &&
                        rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
                        rect.right <= (window.innerWidth || document.documentElement.clientWidth)
                    );", element));

                    return isDisplayedInViewPort;
                }

                return false;
            }
            catch (NoSuchElementException) {
                return false; // Element not found
            }
        }

        public static void WaitForElementToBeVisible(this IWebElement element, IWebDriver driver, int timeoutInSeconds = 10) {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(d => element.Displayed);
        }

    }

}
