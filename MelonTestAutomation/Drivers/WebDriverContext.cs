using MelonTestAutomation.Pages;
using MelonTestAutomation.StepDefinitions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MelonTestAutomation.Drivers
{
    public class WebDriverContext
    {
        private IWebDriver _driver;
        private CheckoutAddressesPage checkoutAddressesPage;
        private HomePage homePage;
        private ProductsPage productsPage;
        private ProductDetailsPage productDetailsPage;
        private SearchPage searchPage;
        private ShoppingCartPage shoppingCartPage;
        private MyworldSigninPage myworldSigninPage;
        private CashbackSigninPage cashbackSigninPage;

        public WebDriverContext()
        {
            _driver = new ChromeDriver();
            homePage = new HomePage(_driver);
            productsPage = new ProductsPage(_driver);
            productDetailsPage = new ProductDetailsPage(_driver);
            searchPage = new SearchPage(_driver);
            shoppingCartPage = new ShoppingCartPage(_driver);
            checkoutAddressesPage = new CheckoutAddressesPage(_driver);
            myworldSigninPage = new MyworldSigninPage(_driver);
            cashbackSigninPage = new CashbackSigninPage(_driver);

        }

        public IWebDriver Driver => _driver;

        public HomePage HomePage => homePage;

        public ProductsPage ProductsPage => productsPage;

        public ProductDetailsPage ProductDetailsPage => productDetailsPage;

        public SearchPage SearchPage => searchPage;

        public ShoppingCartPage ShoppingCartPage => shoppingCartPage;

        public CheckoutAddressesPage CheckoutAddressesPage => checkoutAddressesPage;

        public MyworldSigninPage MyworldSigninPage => myworldSigninPage;

        public CashbackSigninPage CashbackSigninPage => cashbackSigninPage;
    }
}
