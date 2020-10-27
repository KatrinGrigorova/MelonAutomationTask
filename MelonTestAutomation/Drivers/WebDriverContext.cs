using MelonTestAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace MelonTestAutomation.Drivers
{
    public class WebDriverContext
    {
        private IWebDriver _driver;
        private HomePage homePage;
        private ProductsPage productsPage;
        private ShoppingCartPage shoppingCartPage;

        public WebDriverContext()
        {
            //_driver = driver;
            _driver = new ChromeDriver();
            homePage = new HomePage(_driver);
            productsPage = new ProductsPage(_driver);
            shoppingCartPage = new ShoppingCartPage(_driver);
        }

        public IWebDriver Driver => _driver;

        public HomePage HomePage => homePage;

        public ProductsPage ProductsPage => productsPage;

        public ShoppingCartPage ShoppingCartPage => shoppingCartPage;

    }
}
