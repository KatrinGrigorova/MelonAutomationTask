using MelonTestAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Web;
using TechTalk.SpecFlow;

namespace MelonTestAutomation.StepDefinitions
{
    [Binding]
    public class PaginationCheckSteps
    {
        #region Local Variables, Before and After scenario
        private IWebElement page;
        private string selectedPage;
        private string urlPageParam;
        private WebDriverContext _context;

        string url = "https://de.myworld.com/";

        public PaginationCheckSteps(WebDriverContext context)
        {
            _context = context;
        }

        [BeforeScenario("@pagination")]
        public void BeforeScenario()
        {
            _context.Driver.Navigate().GoToUrl(url);
            _context.Driver.Manage().Window.Maximize();
            _context.HomePage.AcceptCookiesButton.Click();
        }

        [AfterScenario("@pagination")]
        public void DesposeWebDriver()
        {
            _context.Driver.Dispose();
        }
        #endregion

        [When("I enter (.*) to the Search bar")]
        public void GivenIEnterToTheSearchBar(string product)
        {
            _context.HomePage.SearchBar.SendKeys(product);
        }

        [When(@"I click the Search button")]
        public void GivenIClickTheSearchButton()
        {
            _context.HomePage.SearchBar.SendKeys(Keys.Enter);
        }

        [When(@"I scroll to the bottom of search result list")]
        public void WhenIScrollToTheBottomOfSearchResultList()
        {
            _context.HomePage.ScrollToElement(_context.SearchPage.Pagination);
        }

        [When("I click page (.*)")]
        public void WhenIClickPage(string pageNumber)
        {
            page = _context.SearchPage.PageNumberList.Where(e => e.Text == pageNumber).FirstOrDefault();
            page.Click();
        }

        [When(@"I click Next page")]
        public void WhenIClickNextPage()
        {
            _context.SearchPage.NextPageButton.Click();
        }

        [When(@"I click previous page")]
        public void WhenIClickPreviousPage()
        {
            _context.SearchPage.PrevPageButton.Click();
        }

        [When(@"I click the last page")]
        public void WhenIClickTheLastPage()
        {
            page = _context.SearchPage.PageNumberList.LastOrDefault();
            page.Click();
        }

        [Then("The (.*) search list is displayed")]
        public void ThenTheSearchListIsDisplayed(string expectedSearchItem)
        {
            Uri currentUrl = new Uri(_context.Driver.Url);
            urlPageParam = HttpUtility.ParseQueryString(currentUrl.Query).Get("q");

            Assert.AreEqual(expectedSearchItem, urlPageParam, "The url search item parameter value is not correct.");
        }


        [Then(@"Page (.*) is loaded")]
        public void ThenPageIsLoaded(string expectedPage)
        {
            Uri currentUrl = new Uri(_context.Driver.Url);
            urlPageParam = HttpUtility.ParseQueryString(currentUrl.Query).Get("page");

            selectedPage = _context.SearchPage.SelectedPage.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedPage, selectedPage, "Incorrect page is loaded.");
                Assert.AreEqual(int.Parse(expectedPage), int.Parse(urlPageParam), "The url page parameter value is not correct.");
            });
        }

        [Then(@"Pagination is available")]
        public void ThenPaginationIsAvailable()
        {
            bool isPaginationDisplayed = _context.SearchPage.Pagination.Displayed;

            Assert.IsTrue(isPaginationDisplayed, "Pagination is not available.");
        }

        [Then(@"The last page is loaded")]
        public void ThenTheLastPageIsLoaded()
        {
            bool nextPageButtonIsNotExist = _context.SearchPage.NextPageButton == null;

            Assert.IsTrue(nextPageButtonIsNotExist, "Incorrect page is loaded.");
        }      
    }
}
