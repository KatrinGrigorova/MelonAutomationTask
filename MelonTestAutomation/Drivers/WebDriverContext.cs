using MelonTestAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MelonTestAutomation.Drivers
{
    public class WebDriverContext
    {
        private IWebDriver _driver;
        //private BasePage basePage;
        private HomePage homePage;
        private ProductsPage productsPage;
        private ProductDetailsPage productDetailsPage;
        private SearchPage searchPage;
        private ShoppingCartPage shoppingCartPage;

        public WebDriverContext()
        {
            _driver = new ChromeDriver();
            //basePage = new BasePage(_driver);
            homePage = new HomePage(_driver);
            productsPage = new ProductsPage(_driver);
            productDetailsPage = new ProductDetailsPage(_driver);
            searchPage = new SearchPage(_driver);
            shoppingCartPage = new ShoppingCartPage(_driver);
        }

        public IWebDriver Driver => _driver;

        //public BasePage BasePage => basePage;

        public HomePage HomePage => homePage;

        public ProductsPage ProductsPage => productsPage;

        public ProductDetailsPage ProductDetailsPage => productDetailsPage;

        public SearchPage SearchPage => searchPage;

        public ShoppingCartPage ShoppingCartPage => shoppingCartPage;

    }
}
