using MelonTestAutomation.Drivers;
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
            getCategoriesSteps = new ShoppingCartCheckValuesSteps(_context) { };
        }

        [BeforeScenario("@signinFeature")]
        public void BeforeScenario()
        {
            _context.Driver.Navigate().GoToUrl(url);
            _context.Driver.Manage().Window.Maximize();
            _context.HomePage.AcceptCookiesButton.Click();
            DeleteProductsFromShoppingCart();
        }

        [AfterScenario("@signinFeature")]
        public void DesposeWebDriver()
        {
            DeleteProductsFromShoppingCart();
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
                case "randomCategoryPage":
                    getCategoriesSteps.WhenIClickAllCategoriesDropdownMenu();
                    getCategoriesSteps.WhenIOpenRandomCategory();
                    break;
                case "shoppingCartPage":
                    getCategoriesSteps.WhenIClickAllCategoriesDropdownMenu();
                    getCategoriesSteps.WhenIOpenRandomCategory();
                    getCategoriesSteps.WhenIAddMultipleRandomAvailableProductsToTheShoppingCart(1);
                    getCategoriesSteps.WhenIGoToTheShoppingCart();
                    break;
                case "randomProductDetailsPage":
                    OpenRandomProductDetails();

                    url = _context.Driver.Url;
                    break;
                case "checkout":
                    getCategoriesSteps.WhenIClickAllCategoriesDropdownMenu();
                    getCategoriesSteps.WhenIOpenRandomCategory();
                    getCategoriesSteps.WhenIAddMultipleRandomAvailableProductsToTheShoppingCart(1);
                    getCategoriesSteps.WhenIGoToTheShoppingCart();
                    _context.ShoppingCartPage.ShoppingCartCheckoutButton.Click();
                    break;
                default:
                    break;
            }
        }

        [When(@"I go to User Account and press Cashback button")]
        public void WhenIGoToUserAccountAndPressCashbackButton()
        {
            if (currentPage != "checkout")
            {
                Actions action = new Actions(_context.Driver);
                action.MoveToElement(_context.HomePage.UserAccountIconLoggedOut).Perform();
                _context.HomePage.CashBackLoginButton.Click();
            }
        }

        [When("I fill (.*) and (.*)")]
        public void WhenIFillTheCredentialFields(string email, string password)
        {
            _context.CashbackSigninPage.LoginInputEmail.SendKeys(email);
            _context.CashbackSigninPage.LoginInputPassword.SendKeys(password);
        }

        [When("I click Login button")]
        public void WhenIClickLoginButton()
        {
            _context.CashbackSigninPage.LoginSubmitButton.Click();
        }

        [Then(@"The Sign In page is loaded")]
        public void ThenTheSignInPageIsLoaded()
        {
            Uri currentUrl = new Uri(_context.Driver.Url);
            string urlDirectory = currentUrl.Segments.LastOrDefault();

            Assert.AreEqual("signin", urlDirectory, "Wrong page is loaded.");
        }

        [Then("I am logged in")]
        public void ThenIAmLoggedIn()
        {
            Actions action = new Actions(_context.Driver);
            action.MoveToElement(_context.HomePage.UserAccountIconLoggedIn).Perform();

            bool isLoggedIn = _context.HomePage.LogoutButton.Displayed;

            if (_scenarioContext.ScenarioInfo.Tags.Contains("shoppingCart"))
            {
                Assert.Multiple(() =>
                {
                    Assert.AreEqual(url, _context.Driver.Url, "I am not redirected back to myworld.");
                    Assert.IsTrue(isLoggedIn, "I am not logged in.");
                });
            }
            else
            {
                Assert.IsTrue(isLoggedIn, "I am not logged in.");
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
                    if (_scenarioContext.ScenarioInfo.Tags.Contains("signinFromAnyPage"))
                    {
                        currentUrl = new Uri(_context.Driver.Url);
                        urlDirectory = currentUrl.Segments.LastOrDefault();

                        DeleteProductsFromShoppingCart();

                        Assert.AreEqual("cart", urlDirectory, "Wrong page is loaded.");
                    }
                    else
                    {
                        currentUrl = new Uri(_context.Driver.Url);
                        urlDirectory = currentUrl.Segments.LastOrDefault();

                        //DeleteProductsFromShoppingCart();

                        Assert.AreEqual("address", urlDirectory, "Wrong page is loaded.");
                    }
                    break;
                case "customerAddresses":
                    currentUrl = new Uri(_context.Driver.Url);
                    urlDirectory = currentUrl.AbsolutePath; 

                    Assert.True(urlDirectory.Contains("customer/address"), "Wrong page is loaded.");
                    break;
                default:
                    break;
            }
        }


        #region Internal methods
        public void DeleteProductsFromShoppingCart()
        {
            Actions action = new Actions(_context.Driver);
            action.MoveToElement(_context.HomePage.MyWorldMainLogo).Perform();

            _context.HomePage.MyWorldMainLogo.Click();

            var shoppingCartQuantity = _context.HomePage.ShoppingCartQuantity.Text;

            if (shoppingCartQuantity != "")
            {
                List<IWebElement> removeItemsList;

                _context.HomePage.ShoppingCartIcon.Click();

                removeItemsList = _context.HomePage.RemoveItemFromTheCart.ToList();

                while (removeItemsList.Count() > 0)
                {
                    removeItemsList[0].Click();
                    Thread.Sleep(1000);

                    shoppingCartQuantity = _context.HomePage.ShoppingCartQuantity.Text;

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
        }

        public void OpenRandomProductDetails()
        {
            getCategoriesSteps.WhenIClickAllCategoriesDropdownMenu();
            getCategoriesSteps.WhenIOpenRandomCategory();

            _context.ProductsPage.BenefitStoreFilter.Click();
            _context.ProductsPage.BenefitStoreFilterTrue.Click();
            _context.ProductsPage.ApplyFilterButton.Click();

            int randomProduct = Enumerable.Range(1, _context.ProductsPage.CategoryProductsList.ToList().Count).OrderBy(o => (new Random()).Next()).Take(1).FirstOrDefault();

            IWebElement product = _context.ProductsPage.CategoryProductsList[randomProduct - 1];

            string productName = product.GetAttribute("text").Trim();

            _context.HomePage.ScrollToElement(product);
            Thread.Sleep(1000);
            product.Click();
        }
        #endregion
    }
}
