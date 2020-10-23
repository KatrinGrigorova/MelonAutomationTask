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
        private IWebDriver driver;
        private HomePage homePage;
        private MyworldSigninPage myworldSigninPage;
        private CashbackSigninPage cashbackSigninPage;

        string url = "https://de.myworld.com/";

        [Given(@"I am on the Home page")]
        public void GivenIAmOnTheHomePage()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            homePage = new HomePage(driver);
            myworldSigninPage = new MyworldSigninPage(driver);
            cashbackSigninPage = new CashbackSigninPage(driver);

            url = driver.Url;
        }
        
        [When(@"I press MyAccount")]
        public void WhenIPressMyAccount()
        {
            homePage.MyAccountNotLoggedIn.Click();
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
            Uri currentUrl = new Uri(driver.Url);
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
                isLoggedIn = homePage.MyAccountLoggedIn.Displayed;
                isCashBackIconDisplayed = homePage.MyAccountCashbackIcon == null;

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(isLoggedIn, "I am not logged in.");
                    Assert.IsTrue(isCashBackIconDisplayed, "The cashback icon appears, but it shouldn't.");
                });
            }
            else
            {
                isLoggedIn = homePage.MyAccountLoggedIn.Displayed;
                isCashBackIconDisplayed = homePage.MyAccountCashbackIcon == null;

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(isLoggedIn, "I am not logged in.");
                    Assert.IsFalse(isCashBackIconDisplayed, "I am not logged in with my cashback account.");
                });
            }


            switch (accountType)
            {
                case "myworld":
                    isLoggedIn = homePage.MyAccountLoggedIn.Displayed;
                    isCashBackIconDisplayed = homePage.MyAccountCashbackIcon == null;

                    Assert.Multiple(() =>
                    {
                        Assert.IsTrue(isLoggedIn, "I am not logged in.");
                        Assert.IsTrue(isCashBackIconDisplayed, "The cashback icon appears, but it shouldn't.");
                    });
                    break;
                case "cashback":
                    isLoggedIn = homePage.MyAccountLoggedIn.Displayed;
                    isCashBackIconDisplayed = homePage.MyAccountCashbackIcon == null;

                    Assert.Multiple(() =>
                    {
                        Assert.AreEqual(url, driver.Url, "I am not redirected back to myworld.");
                        Assert.IsTrue(isLoggedIn, "I am not logged in.");
                        Assert.IsFalse(isCashBackIconDisplayed, "I am not logged in with my cashback account.");
                    });
                    break;
                default:
                    break;
            }
        }               
       
        [AfterScenario("@signin")]
        public void DesposeWebDriver()
        {
            driver.Dispose();
        }
    }
}
