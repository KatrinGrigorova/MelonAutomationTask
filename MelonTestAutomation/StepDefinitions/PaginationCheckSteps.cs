using MelonTestAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Web;
using TechTalk.SpecFlow;

namespace MelonTestAutomation.StepDefinitions
{
    [Binding]
    public class PaginationCheckSteps
    {
        private IWebDriver driver;
        private HomePage homePage;
        private SearchPage searchPage;
        private IWebElement page;
        private string selectedPage;
        private string urlPageParam;

        [Given(@"I'm on the Home Page")]
        public void GivenIAmOnTheHomePage()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://de.myworld.com/ ");
            driver.Manage().Window.Maximize();

            homePage = new HomePage(driver);
            searchPage = new SearchPage(driver);
        }

        [Given(@"I press Accept cookies button")]
        public void GivenIPressAcceptCookiesButton()
        {
            homePage.CookieButton.Click();
        }

        [When("I enter (.*) to the Search bar")]
        public void GivenIEnterToTheSearchBar(string product)
        {
            homePage.SearchBar.SendKeys(product);
        }

        [When(@"I press the Search button")]
        public void GivenIPressTheSearchButton()
        {
            homePage.SearchButton.Click();
        }

        [When(@"I scroll to the bottom of search result list")]
        public void WhenIScrollToTheBottomOfSearchResultList()
        {
            homePage.ScrollToElement(searchPage.Pagination);
        }

        [When("I press page (.*)")]
        public void WhenIPressPage(string pageNumber)
        {
            page = searchPage.PageNumberList.Where(e => e.Text == pageNumber).FirstOrDefault();
            page.Click();
        }

        [When(@"I press Next page")]
        public void WhenIPressNextPage()
        {
            searchPage.NextPageButton.Click();
        }

        [When(@"I press previous page")]
        public void WhenIPressPreviousPage()
        {
            searchPage.PrevPageButton.Click();
        }

        [When(@"I press second ellipsis")]
        public void WhenIPressSecondEllipsis()
        {
            page = searchPage.PageNumberList.Where(e => e.Text == "...").LastOrDefault();
            page.Click();
        }

        [When(@"I press first ellipsis")]
        public void WhenIPressFirstEllipsis()
        {
            page = searchPage.PageNumberList.Where(e => e.Text == "...").FirstOrDefault();
            page.Click();
        }

        [When(@"I press the last page")]
        public void WhenIPressTheLastPage()
        {
            page = searchPage.PageNumberList.LastOrDefault();
            page.Click();
        }

        [Then("The (.*) search list is displayed")]
        public void ThenTheSearchListIsDisplayed(string expectedSearchItem)
        {
            Uri currentUrl = new Uri(driver.Url);
            urlPageParam = HttpUtility.ParseQueryString(currentUrl.Query).Get("s");

            Assert.AreEqual(expectedSearchItem, urlPageParam, "The url search item parameter value is not correct.");
        }


        [Then(@"Page (.*) is loaded")]
        public void ThenPageIsLoaded(string expectedPage)
        {
            Uri currentUrl = new Uri(driver.Url);
            urlPageParam = HttpUtility.ParseQueryString(currentUrl.Query).Get("f");

            selectedPage = searchPage.SelectedPage.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedPage, selectedPage, "Incorrect page is loaded.");
                Assert.AreEqual(int.Parse(expectedPage) - 1, int.Parse(urlPageParam), "The url page parameter value is not correct.");
            });
        }

        [Then(@"Pagination is available")]
        public void ThenPaginationIsAvailable()
        {
            bool isPaginationDisplayed = searchPage.Pagination.Displayed;

            Assert.IsTrue(isPaginationDisplayed, "Pagination is not available.");
        }

        [Then(@"The last page is loaded")]
        public void ThenTheLastPageIsLoaded()
        {
            bool nextPageButtonIsNotExist = searchPage.NextPageButton == null;

            Assert.IsTrue(nextPageButtonIsNotExist, "Incorrect page is loaded.");
        }

        [AfterScenario("@pagination")]
        public void DesposeWebDriver()
        {
            driver.Dispose();
        }
    }
}
