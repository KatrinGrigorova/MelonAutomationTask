using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using TechTalk.SpecFlow;

namespace MelonTestAutomation.Drivers
{
    public class WebDriverFactory
    {
        private readonly ConfigurationDriver _configurationDriver;

        public WebDriverFactory(ConfigurationDriver configurationDriver)
        {
            _configurationDriver = configurationDriver;
        }

        public IWebDriver GetAllWebDrivers(string browser)
        {
            string browserName = browser.ToUpper();

            switch (browserName)
            {
                case "EDGE":
                    return EdgeWebDriver();
                case "FIREFOX":
                    return FirefoxWebDriver();
                case "CHROME":
                    return ChromeWebDriver();
                default: throw new NotSupportedException("not supported browser: <null>");
            }
        }

        private IWebDriver EdgeWebDriver()
        {
            EdgeOptions options = new EdgeOptions();
            options.UseChromium = true;

            return new EdgeDriver(options)
            {
                Url = _configurationDriver.SeleniumBaseUrl
            };
        }

        private IWebDriver FirefoxWebDriver()
        {
            return new FirefoxDriver()
            {
                Url = _configurationDriver.SeleniumBaseUrl,
            };
        }

        private IWebDriver ChromeWebDriver()
        {
            return new ChromeDriver()
            {
                Url = _configurationDriver.SeleniumBaseUrl
            };
        }
    }
}
