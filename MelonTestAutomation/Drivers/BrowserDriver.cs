using OpenQA.Selenium;
using System;

namespace MelonTestAutomation.Drivers
{
    public class BrowserDriver
    {
        private readonly WebDriverFactory _webDriverFactory;
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private bool _isDisposed;

        public BrowserDriver(WebDriverFactory webDriverFactory)
        {
            _webDriverFactory = webDriverFactory;
            _currentWebDriverLazy = new Lazy<IWebDriver>(GetWebDriver);
        }

        public IWebDriver Current => _currentWebDriverLazy.Value;

        private IWebDriver GetWebDriver()
        {
            string testBrowserId = Environment.GetEnvironmentVariable("Test_Browser");
            return _webDriverFactory.GetAllWebDrivers(testBrowserId);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}
