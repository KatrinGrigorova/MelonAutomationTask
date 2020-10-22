using MelonTestAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        private readonly ScenarioContext _scenarioContext;

        public SignInSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

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
        
        [When(@"I enter email address (.*)")]
        public void WhenIEnterValidEmailAddress(string email)
        {
            if (_scenarioContext.ScenarioInfo.Tags.Contains("cashback"))
            {
                cashbackSigninPage.LoginInputEmail.SendKeys(email);
            }
            else
            {
                myworldSigninPage.LoginInputEmail.SendKeys(email);
            }
        }
        
        [When("I enter passworld (.*)")]
        public void WhenIEnterValidPassworld(string password)
        {
            if (_scenarioContext.ScenarioInfo.Tags.Contains("cashback"))       
            {
                cashbackSigninPage.LoginInputPassword.SendKeys(password);
            }
            else
            {
                myworldSigninPage.LoginInputPassword.SendKeys(password);
            }
        }        
        
        [When(@"I press Cashback world button")]
        public void WhenIPressCashbackWorldButton()
        {
            myworldSigninPage.LoginCashBackSubmitButton.Click();
        }
        
        [When(@"I press Login button")]
        public void WhenIPressLoginButton()
        {
            if (_scenarioContext.ScenarioInfo.Tags.Contains("cashback"))
            {
                cashbackSigninPage.LoginSubmitButton.Click();
            }
            else
            {
                myworldSigninPage.LoginSubmitButton.Click();
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
        public void ThenIAmLoggedWithAccount(string account)
        {
            bool isLoggedIn;
            bool isCashBackIconDisplayed;

            if (account == "myworld")
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
        }
       
        [Then(@"I am redirected to Cashback world sign in page")]
        public void ThenIAmRedirectedToCashbackWorldSignInPage()
        {
            string cashbackSignInUrlTitle = "Cashback World | Cashback: Money back with every purchase";
            string currentUrl = driver.Title;

            Assert.AreEqual(cashbackSignInUrlTitle, currentUrl, "Wrong redirection");
        }
        
        [Then(@"I am redirected back to myworld website")]
        public void ThenIAmRedirectedBackToMyworldWebsite()
        {
            Assert.AreEqual(url, driver.Url, "Wrong redirection.");
        }
       
        [AfterScenario("@signin")]
        public void DesposeWebDriver()
        {
            driver.Dispose();
        }
    }
}
