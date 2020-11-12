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
            _context.HomePage.CookieButton.Click();
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

        [When(@"I press the Search button")]
        public void GivenIPressTheSearchButton()
        {
            _context.HomePage.SearchButton.Click();
        }

        [When(@"I scroll to the bottom of search result list")]
        public void WhenIScrollToTheBottomOfSearchResultList()
        {
            _context.HomePage.ScrollToElement(_context.SearchPage.Pagination);
        }

        [When("I press page (.*)")]
        public void WhenIPressPage(string pageNumber)
        {
            page = _context.SearchPage.PageNumberList.Where(e => e.Text == pageNumber).FirstOrDefault();
            page.Click();
        }

        [When(@"I press Next page")]
        public void WhenIPressNextPage()
        {
            _context.SearchPage.NextPageButton.Click();
        }

        [When(@"I press previous page")]
        public void WhenIPressPreviousPage()
        {
            _context.SearchPage.PrevPageButton.Click();
        }

        [When(@"I press second ellipsis")]
        public void WhenIPressSecondEllipsis()
        {
            page = _context.SearchPage.PageNumberList.Where(e => e.Text == "...").LastOrDefault();
            page.Click();
        }

        [When(@"I press first ellipsis")]
        public void WhenIPressFirstEllipsis()
        {
            page = _context.SearchPage.PageNumberList.Where(e => e.Text == "...").FirstOrDefault();
            page.Click();
        }

        [When(@"I press the last page")]
        public void WhenIPressTheLastPage()
        {
            page = _context.SearchPage.PageNumberList.LastOrDefault();
            page.Click();
        }

        [Then("The (.*) search list is displayed")]
        public void ThenTheSearchListIsDisplayed(string expectedSearchItem)
        {
            Uri currentUrl = new Uri(_context.Driver.Url);
            urlPageParam = HttpUtility.ParseQueryString(currentUrl.Query).Get("s");

            Assert.AreEqual(expectedSearchItem, urlPageParam, "The url search item parameter value is not correct.");
        }


        [Then(@"Page (.*) is loaded")]
        public void ThenPageIsLoaded(string expectedPage)
        {
            Uri currentUrl = new Uri(_context.Driver.Url);
            urlPageParam = HttpUtility.ParseQueryString(currentUrl.Query).Get("f");

            selectedPage = _context.SearchPage.SelectedPage.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedPage, selectedPage, "Incorrect page is loaded.");
                Assert.AreEqual(int.Parse(expectedPage) - 1, int.Parse(urlPageParam), "The url page parameter value is not correct.");
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
