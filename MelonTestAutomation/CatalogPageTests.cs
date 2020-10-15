using MelonTestAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

namespace MelonTestAutomation
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Edge)]
    [TestFixture(BrowserType.Firefox)]
    public class CatalogPageTests : WebDriverFactory
    {
        private IWebDriver driver;
        private BrowserType browserType;
        private HomePage homePage;
        private SearchPage searchPage;
        private IWebElement page;
        private string selectedPage;

        public CatalogPageTests(BrowserType browser)
            : base()
        {
            browserType = browser;
        }

        [SetUp]
        public void SetUp()
        {
            driver = WebDriver(browserType);
            driver.Navigate().GoToUrl("https://de.myworld.com/");
            driver.Manage().Window.Maximize();

            homePage = new HomePage(driver);
            searchPage = new SearchPage(driver);

            homePage.SearchBar.SendKeys("book");

            homePage.SearchButton.Click();

            homePage.CookieButton.Click();

            homePage.ScrollToElement(searchPage.Pagination);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }


        [Test]
        public void When_SearchIsPerformed_Then_PaginationIsAvailableAndCorrectPageIsLoaded()
        {
            //Act
            bool isPaginationDisplayed = searchPage.Pagination.Displayed;

            selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.Multiple(() =>
            {
               Assert.IsTrue(isPaginationDisplayed, "Pagination is not available.");
               Assert.AreEqual("1", selectedPage, "Incorrect page is loaded.");
            });

        }

        [Test]
        public void When_SecondPageIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            page = searchPage.PageNumberList.Where(e => e.Text == "2").FirstOrDefault();
            page.Click();

            selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("2", selectedPage, "Incorrect page is loaded.");
        }

        [Test]
        public void When_ThirdPageIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act         
            page = searchPage.PageNumberList.Where(e => e.Text == "2").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            page = searchPage.PageNumberList.Where(e => e.Text == "3").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("3", selectedPage, "Incorrect page is loaded.");
        }

        [Test]
        public void When_ForthPageIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            page = searchPage.PageNumberList.Where(e => e.Text == "2").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            page = searchPage.PageNumberList.Where(e => e.Text == "3").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            page = searchPage.PageNumberList.Where(e => e.Text == "4").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("4", selectedPage, "Incorrect page is loaded.");
        }

        [Test]
        public void When_NextButtonIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            searchPage.NextPageButton.Click();

            selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("2", selectedPage, "Incorrect page is loaded.");
        }

        [Test]
        public void When_PreviousButtonIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            page = searchPage.PageNumberList.Where(e => e.Text == "2").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            page = searchPage.PageNumberList.Where(e => e.Text == "3").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            searchPage.PrevPageButton.Click();

            selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("2", selectedPage, "Incorrect page is loaded.");
        }

        [Test]
        public void When_SecondEllipsisButtonIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act   
            page = searchPage.PageNumberList.Where(e => e.Text == "2").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            page = searchPage.PageNumberList.Where(e => e.Text == "3").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            page = searchPage.PageNumberList.Where(e => e.Text == "4").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            page = searchPage.PageNumberList.Where(e => e.Text == "...").LastOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("5", selectedPage, "Incorrect page is loaded.");
        }

        [Test]
        public void When_FirstEllipsisButtonIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            page = searchPage.PageNumberList.Where(e => e.Text == "2").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            page = searchPage.PageNumberList.Where(e => e.Text == "3").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            page = searchPage.PageNumberList.Where(e => e.Text == "4").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            page = searchPage.PageNumberList.Where(e => e.Text == "...").FirstOrDefault();
            page.Click();
            homePage.ScrollToElement(searchPage.Pagination);

            selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("3", selectedPage, "Incorrect page is loaded.");
        }

        [Test]
        public void When_FirstPageIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act   
            page = searchPage.PageNumberList.Where(e => e.Text == "2").FirstOrDefault();
            page.Click();

            page = searchPage.PageNumberList.Where(e => e.Text == "3").FirstOrDefault();
            page.Click();

            page = searchPage.PageNumberList.Where(e => e.Text == "1").FirstOrDefault();
            page.Click();

            selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("1", selectedPage, "Incorrect page is loaded.");
        }

        [Test]
        public void When_LastPageIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            page = searchPage.PageNumberList.LastOrDefault();
            page.Click();

            homePage.ScrollToElement(searchPage.Pagination);

            bool nextPageButtonIsNotExist = searchPage.NextPageButton == null;
            
            //Assert
            Assert.IsTrue(nextPageButtonIsNotExist, "Incorrect page is loaded.");
        }
    }
}
