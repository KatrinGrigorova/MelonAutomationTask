using MelonTestAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace MelonTestAutomation
{
    [TestFixture]
    public class CatalogPageTests
    {
        private IWebDriver driver;
        private HomePage homePage;
        private SearchPage searchPage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Navigate().GoToUrl("https://de.myworld.com/");
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }


        [Test, Order(1)]
        public void When_SearchIsPerformed_Then_PaginationIsAvailableAndCorrectPageIsLoaded()
        {
            //Arrange
            homePage = new HomePage(driver);
            searchPage = new SearchPage(driver);

            //Act
            homePage.SearchBar.SendKeys("book");

            homePage.SearchButton.Click();

            homePage.CookieButton.Click();

            var isPaginationDisplayed = searchPage.Pagination.Displayed;

            var selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.Multiple(() =>
            {
               Assert.IsTrue(isPaginationDisplayed, "Pagination is not available.");
               Assert.AreEqual("1", selectedPage, "Incorrect page is loaded.");
            });

        }

        [Test, Order(2)]
        public void When_SecondPageIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            homePage.ScrollToElement(searchPage.Pagination);
            Thread.Sleep(4000);

            var page = searchPage.PageNumberList.Where(e => e.Text == "2").FirstOrDefault();
            page.Click();

            var selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("2", selectedPage, "Incorrect page is loaded.");
        }

        [Test, Order(3)]
        public void When_ThirdPageIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            homePage.ScrollToElement(searchPage.Pagination);
            Thread.Sleep(4000);

            var page = searchPage.PageNumberList.Where(e => e.Text == "3").FirstOrDefault();
            page.Click();

            var selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("3", selectedPage, "Incorrect page is loaded.");
        }

        [Test, Order(4)]
        public void When_ForthPageIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            homePage.ScrollToElement(searchPage.Pagination);
            Thread.Sleep(4000);

            var page = searchPage.PageNumberList.Where(e => e.Text == "4").FirstOrDefault();
            page.Click();

            var selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("4", selectedPage, "Incorrect page is loaded.");
        }

        [Test, Order(5)]
        public void When_NextButtonIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            homePage.ScrollToElement(searchPage.Pagination);
            Thread.Sleep(3000);

            searchPage.NextPageButton.Click();

            var selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("5", selectedPage, "Incorrect page is loaded.");
        }

        [Test, Order(6)]
        public void When_PreviousButtonIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            homePage.ScrollToElement(searchPage.Pagination);
            Thread.Sleep(3000);

            searchPage.PrevPageButton.Click();

            var selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("4", selectedPage, "Incorrect page is loaded.");
        }

        [Test, Order(7)]
        public void When_SecondEllipsisButtonIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            homePage.ScrollToElement(searchPage.Pagination);
            Thread.Sleep(3000);

            var page = searchPage.PageNumberList.Where(e => e.Text == "...").LastOrDefault();
            page.Click();

            var selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("5", selectedPage, "Incorrect page is loaded.");
        }

        [Test, Order(8)]
        public void When_FirstEllipsisButtonIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            homePage.ScrollToElement(searchPage.Pagination);
            Thread.Sleep(3000);

            var page = searchPage.PageNumberList.Where(e => e.Text == "...").FirstOrDefault();
            page.Click();

            var selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("4", selectedPage, "Incorrect page is loaded.");
        }

        [Test, Order(9)]
        public void When_FirstPageIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            homePage.ScrollToElement(searchPage.Pagination);
            Thread.Sleep(3000);

            var page = searchPage.PageNumberList.Where(e => e.Text == "1").FirstOrDefault();
            page.Click();

            var selectedPage = searchPage.SelectedPage.Text;

            //Assert
            Assert.AreEqual("1", selectedPage, "Incorrect page is loaded.");
        }

        [Test, Order(10)]
        public void When_LastPageIsClicked_Then_CorrectPageIsLoaded()
        {
            //Act          
            homePage.ScrollToElement(searchPage.Pagination);
            Thread.Sleep(3000);

            var page = searchPage.PageNumberList.LastOrDefault();
            page.Click();

            bool nextPageButtonIsNotExist = searchPage.NextPageButton == null;

            //Assert
            Assert.IsTrue(nextPageButtonIsNotExist, "Incorrect page is loaded.");
        }
    }
}
