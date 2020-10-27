using MelonTestAutomation.Drivers;
using MelonTestAutomation.Pages;
using Microsoft.Edge.SeleniumTools;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace MelonTestAutomation.Feature_files
{
    [Binding]
    public class SignInSteps
    {
        private MyworldSigninPage myworldSigninPage;
        private CashbackSigninPage cashbackSigninPage;

        string url = "https://de.myworld.com/";

        private WebDriverContext _context;
        private readonly ScenarioContext _scenarioContext;

        public SignInSteps(WebDriverContext context, ScenarioContext scenarioContext)
        {
            _context = context;
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the Home page")]
        public void GivenIAmOnTheHomePage()
        {
            _context.Driver.Navigate().GoToUrl(url);
            _context.Driver.Manage().Window.Maximize();

            myworldSigninPage = new MyworldSigninPage(_context.Driver);
            cashbackSigninPage = new CashbackSigninPage(_context.Driver);

            url = _context.Driver.Url;
        }

        [When(@"I press MyAccount")]
        public void WhenIPressMyAccount()
        {
            _context.HomePage.MyAccountNotLoggedIn.Click();
        }

        [When(@"I fill (.*) and (.*) with (.*)")]
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

        [When(@"I press Login button in (.*) SignIn form")]
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
            bool isLoggedIn;
            bool isCashBackIconDisplayed;

            if (accountType == "myworld")
            {
                isLoggedIn = _context.HomePage.MyAccountLoggedIn.Displayed;
                isCashBackIconDisplayed = _context.HomePage.MyAccountCashbackIcon == null;

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(isLoggedIn, "I am not logged in.");
                    Assert.IsTrue(isCashBackIconDisplayed, "The cashback icon appears, but it shouldn't.");
                });
            }
            else
            {
                isLoggedIn = _context.HomePage.MyAccountLoggedIn.Displayed;
                isCashBackIconDisplayed = _context.HomePage.MyAccountCashbackIcon == null;

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(isLoggedIn, "I am not logged in.");
                    Assert.IsFalse(isCashBackIconDisplayed, "I am not logged in with my cashback account.");
                });
            }


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

        [Then(@"I am on the Shopping cart page")]
        public void ThenIAmOnTheShoppingCartPage()
        {
            Uri currentUrl = new Uri(_context.Driver.Url);
            string urlDirectory = currentUrl.Segments.LastOrDefault();

            Assert.AreEqual("cart", urlDirectory, "Wrong page is loaded.");
        }


        [AfterScenario("@signinFeature")]
        public void DesposeWebDriver()
        {
            //Delete all products in the shopping cart
            if (_scenarioContext.ScenarioInfo.Tags.Contains("signinThroughShoppingCart"))
            {
                var removeItemsList = _context.ShoppingCartPage.RemoveItemFromTheCart.ToList();
                var cartProductsNumber = _context.ShoppingCartPage.ShoppingCartProductsNames.Count();

                if (cartProductsNumber != 1)
                {
                    foreach (var item in removeItemsList)
                    {
                        item.Click();
                        _context.ShoppingCartPage.ShoppingCartItemQuantity((cartProductsNumber - 1).ToString());
                    }
                }
            }

            _context.Driver.Dispose();
        }
    }
}
