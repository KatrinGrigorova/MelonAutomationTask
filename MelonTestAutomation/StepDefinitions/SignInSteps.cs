using MelonTestAutomation.Drivers;
using MelonTestAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace MelonTestAutomation.StepDefinitions
{
    [Binding]
    public class SignInSteps
    {
        #region Local Variables, Before and After scenario
        private MyworldSigninPage myworldSigninPage;
        private CashbackSigninPage cashbackSigninPage;
        private ShoppingCartCheckValuesSteps getCategoriesSteps;
        private WebDriverContext _context;
        private readonly ScenarioContext _scenarioContext;
        private string currentPage;
        string urlDirectory;

        string url = "https://de.myworld.com/";

        public SignInSteps(WebDriverContext context, ScenarioContext scenarioContext)
        {
            _context = context;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario("@signinFeature")]
        public void BeforeScenario()
        {
            _context.Driver.Navigate().GoToUrl(url);
            _context.Driver.Manage().Window.Maximize();
            _context.HomePage.CookieButton.Click();

            myworldSigninPage = new MyworldSigninPage(_context.Driver);
            cashbackSigninPage = new CashbackSigninPage(_context.Driver);
            getCategoriesSteps = new ShoppingCartCheckValuesSteps(_context) { };
        }

        [AfterScenario("@signinFeature")]
        public void DesposeWebDriver()
        {
            _context.Driver.Dispose();
        }
        #endregion

        [Given("I am on page (.*)")]
        public void GivenIAmOnPage(string page)
        {
            currentPage = page;

            switch (page)
            {
                case "homePage":
                    url = _context.Driver.Url;
                    break;
                case "categoriesPage":
                    getCategoriesSteps.WhenIPressAllCategoriesDropdownMenu();
                    getCategoriesSteps.WhenIPressAllCategoriesTitle();

                    url = _context.Driver.Url;
                    break;
                case "randomCategoryPage":
                    getCategoriesSteps.WhenIPressAllCategoriesDropdownMenu();
                    getCategoriesSteps.WhenIPressAllCategoriesTitle();
                    getCategoriesSteps.WhenIOpenRandomCategory();
                    break;
                case "shoppingCartPage":
                    getCategoriesSteps.WhenIPressAllCategoriesDropdownMenu();
                    getCategoriesSteps.WhenIPressAllCategoriesTitle();
                    getCategoriesSteps.WhenIOpenRandomCategory();
                    getCategoriesSteps.WhenIAddRandomAvailableProductsToTheShoppingCart(1, "1");
                    getCategoriesSteps.WhenIGoToTheShoppingCart();
                    break;
                case "randomProductDetailsPage":
                    OpenRandomProductDetails();

                    url = _context.Driver.Url;
                    break;
                case "checkout":
                    getCategoriesSteps.WhenIPressAllCategoriesDropdownMenu();
                    getCategoriesSteps.WhenIPressAllCategoriesTitle();
                    getCategoriesSteps.WhenIOpenRandomCategory();
                    getCategoriesSteps.WhenIAddRandomAvailableProductsToTheShoppingCart(1, "1");
                    getCategoriesSteps.WhenIGoToTheShoppingCart();
                    _context.ShoppingCartPage.ShoppingCartCheckoutButton.Click();
                    break;
                default:
                    break;
            }
        }

        [When(@"I press MyAccount")]
        public void WhenIPressMyAccount()
        {
            if (currentPage != "checkout")
            {
                _context.HomePage.MyAccountNotLoggedIn.Click();
            }
        }

        [When("I fill (.*) and (.*) with (.*)")]
        public void WhenIFillTheCredentialFields(string email, string password, string accountType)
        {
            switch (accountType)
            {
                case "myworld":
                    myworldSigninPage.LoginInputEmail.SendKeys(email);
                    myworldSigninPage.LoginInputPassword.SendKeys(password);
                    break;
                case "cashback":
                    myworldSigninPage.LoginCashBackSubmitButton.Click();
                    cashbackSigninPage.LoginInputEmail.SendKeys(email);
                    cashbackSigninPage.LoginInputPassword.SendKeys(password);
                    break;
                default:
                    break;
            }
        }

        [When("I press Login button in (.*) SignIn form")]
        public void WhenIPressLoginButton(string accountType)
        {
            switch (accountType)
            {
                case "myworld":
                    myworldSigninPage.LoginSubmitButton.Click();
                    break;
                case "cashback":
                    cashbackSigninPage.LoginSubmitButton.Click();
                    break;
                default:
                    break;
            }
        }

        [Then(@"The Sign In page is loaded")]
        public void ThenTheSignInPageIsLoaded()
        {
            Uri currentUrl = new Uri(_context.Driver.Url);
            string urlDirectory = currentUrl.Segments.LastOrDefault();

            Assert.AreEqual("signin", urlDirectory, "Wrong page is loaded.");
        }

        [Then("I am logged with my (.*) account")]
        public void ThenIAmLoggedWithAccount(string accountType)
        {
            if (currentPage != "checkout")
            {
                bool isLoggedIn;
                bool isCashBackIconDisplayed;

                switch (accountType)
                {
                    case "myworld":
                        isLoggedIn = _context.HomePage.MyAccountLoggedIn.Displayed;
                        isCashBackIconDisplayed = _context.HomePage.MyAccountCashbackIcon == null;

                        Assert.Multiple(() =>
                        {
                            Assert.IsTrue(isLoggedIn, "I am not logged in.");
                            Assert.IsTrue(isCashBackIconDisplayed, "The cashback icon appears, but it shouldn't.");
                        });
                        break;
                    case "cashback":
                        isLoggedIn = _context.HomePage.MyAccountLoggedIn.Displayed;
                        isCashBackIconDisplayed = _context.HomePage.MyAccountCashbackIcon == null;

                        if (_scenarioContext.ScenarioInfo.Tags.Contains("shoppingCart"))
                        {
                            Assert.Multiple(() =>
                            {
                                Assert.AreEqual(url, _context.Driver.Url, "I am not redirected back to myworld.");
                                Assert.IsTrue(isLoggedIn, "I am not logged in.");
                                Assert.IsFalse(isCashBackIconDisplayed, "I am not logged in with my cashback account.");
                            });
                        }
                        else
                        {
                            Assert.Multiple(() =>
                            {
                                Assert.IsTrue(isLoggedIn, "I am not logged in.");
                                Assert.IsFalse(isCashBackIconDisplayed, "I am not logged in with my cashback account.");
                            });
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        [Then("I am on page (.*)")]
        public void ThenIAmOnPage(string page)
        {
            Uri currentUrl;
            string currentUrlString;

            switch (page)
            {
                case "homePage":
                    currentUrl = new Uri(_context.Driver.Url);
                    currentUrlString = currentUrl.ToString();

                    Assert.AreEqual(url, currentUrlString, "Wrong page is loaded.");
                    break;
                case "categoriesPage":
                    currentUrl = new Uri(_context.Driver.Url);
                    urlDirectory = currentUrl.Segments.LastOrDefault();

                    Assert.AreEqual("categories", urlDirectory, "Wrong page is loaded.");
                    break;
                case "randomCategoryPage":
                    currentUrlString = _context.Driver.Url;

                    Assert.AreEqual(getCategoriesSteps.CategoryLink, currentUrlString, "Wrong category is loaded.");
                    break;
                case "shoppingCartPage":
                    List<string> shoppingCartProductNames = _context.ShoppingCartPage.ShoppingCartProductsNames.Select(p => p.Text).ToList();

                    currentUrl = new Uri(_context.Driver.Url);
                    urlDirectory = currentUrl.Segments.LastOrDefault();

                    DeleteProductsFromShoppingCart();

                    Assert.Multiple(() =>
                    {
                        Assert.AreEqual("cart", urlDirectory, "Wrong page is loaded.");
                        CollectionAssert.AreEquivalent(getCategoriesSteps.ProductList, shoppingCartProductNames, "The products in the shopping cart don't match the added ones.");
                    });
                    break;
                case "randomProductDetailsPage":
                    currentUrl = new Uri(_context.Driver.Url);
                    currentUrlString = currentUrl.ToString();

                    Assert.AreEqual(url, currentUrlString, "Wrong page is loaded.");
                    break;
                case "checkout":
                    currentUrl = new Uri(_context.Driver.Url);
                    urlDirectory = currentUrl.Segments.LastOrDefault();

                    DeleteProductsFromShoppingCart();

                    Assert.AreEqual("address", urlDirectory, "Wrong page is loaded.");
                    break;
                default:
                    break;
            }
        }


        #region Internal methods
        public void DeleteProductsFromShoppingCart()
        {
            List<IWebElement> removeItemsList;

            _context.HomePage.MyWorldMainLogo.Click();
            _context.HomePage.ShoppingCartIcon.Click();

            removeItemsList = _context.HomePage.RemoveItemFromTheCart.ToList();

            while (removeItemsList.Count() > 0)
            {
                removeItemsList[0].Click();
                Thread.Sleep(1000);
                var shoppingCartQuantity = _context.HomePage.ShoppingCartIcon.FindElement(By.XPath("//span[@data-qa='cartPopupItemsQty']")).Text;

                if (shoppingCartQuantity != "")
                {
                    removeItemsList = _context.HomePage.RemoveItemFromTheCart.ToList();
                }
                else
                {
                    break;
                }
            }
        }

        public void OpenRandomProductDetails()
        {
            getCategoriesSteps.WhenIPressAllCategoriesDropdownMenu();
            getCategoriesSteps.WhenIPressAllCategoriesTitle();
            getCategoriesSteps.WhenIOpenRandomCategory();

            int randomProduct = Enumerable.Range(1, _context.ProductsPage.CategoryProductList.ToList().Count).OrderBy(o => (new Random()).Next()).Take(1).FirstOrDefault();

            IWebElement product = _context.ProductsPage.CategoryProductList[randomProduct - 1];

            string productName = product.GetAttribute("text").Trim();

            _context.HomePage.ScrollToElement(product);
            product.Click();
        }
        #endregion
    }
}
