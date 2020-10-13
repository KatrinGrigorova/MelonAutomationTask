using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace MelonTestAutomation
{
    public class WebDriverFactory
    {        
        public enum BrowserType
        {
            Edge,
            Chrome,
            Firefox
        }

        public static IWebDriver WebDriver(BrowserType type)
        {
            IWebDriver driver = null;

            switch (type)
            {
                case BrowserType.Edge:
                    driver = EdgeWebDriver();
                    break;
                case BrowserType.Firefox:
                    driver = FirefoxWebDriver();
                    break;
                case BrowserType.Chrome:
                    driver = ChromeWebDriver();
                    break;
                default:
                    break;
            }

            return driver;
        }

        private static IWebDriver EdgeWebDriver()
        {
            var options = new EdgeOptions();
            options.UseChromium = true;

            IWebDriver driver = new EdgeDriver(options);

            return driver;
        }

        private static IWebDriver FirefoxWebDriver()
        {
            IWebDriver driver = new FirefoxDriver();
            return driver;
        }

        private static IWebDriver ChromeWebDriver()
        {
            IWebDriver driver = new ChromeDriver();
            return driver;
        }
    }
}
