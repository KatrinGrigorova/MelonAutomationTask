using MelonTestAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MelonTestAutomation.Drivers
{
    public class WebDriverContext
    {
        private IWebDriver _driver;
        private HomePage homePage;
        private ProductsPage productsPage;
        private SearchPage searchPage;
        private ShoppingCartPage shoppingCartPage;

        public WebDriverContext()
        {
            _driver = new ChromeDriver();
            homePage = new HomePage(_driver);
            productsPage = new ProductsPage(_driver);
            searchPage = new SearchPage(_driver);
            shoppingCartPage = new ShoppingCartPage(_driver);
        }

        public IWebDriver Driver => _driver;

        public HomePage HomePage => homePage;

        public ProductsPage ProductsPage => productsPage;

        public SearchPage SearchPage => searchPage;

        public ShoppingCartPage ShoppingCartPage => shoppingCartPage;

    }
}
