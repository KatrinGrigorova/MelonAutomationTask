using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MelonTestAutomation.Pages
{
    public class BasePage
    {
        private IWebDriver _driver;
        private IJavaScriptExecutor js;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            js = (IJavaScriptExecutor)driver;
        }

        public IWebDriver Driver => _driver;

        public WebDriverWait Wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(80));

        public void ScrollToElement(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView(false);", element);
        }
    }
}
