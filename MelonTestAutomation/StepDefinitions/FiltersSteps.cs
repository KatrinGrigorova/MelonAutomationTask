using MelonTestAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace MelonTestAutomation.StepDefinitions
{
    [Binding]
    public class FiltersSteps
    {
        #region Local Variables, Before and After scenario
        private WebDriverContext _context;
        string categoryName;
        string categoryLink;
        string url;

        public FiltersSteps(WebDriverContext context)
        {
            _context = context;
        }

        [AfterScenario("@filters")]
        public void DesposeWebDriver()
        {
            _context.Driver.Dispose();
        }
        #endregion

        [Given("I am on domain (.*) Home page")]
        public void GivenIAmOnDomainHomePage(string domain)
        {
            switch (domain)
            {
                case "de":
                    url = "https://de.myworld.com/";
                    break;
                case "at":
                    url = "https://at.myworld.com/";
                    break;
                case "ch":
                    url = "https://ch.myworld.com/";
                    break;
                case "it":
                    url = "https://it.myworld.com/";
                    break;
                case "hu":
                    url = "https://hu.myworld.com/";
                    break;
                case "cz":
                    url = "https://cz.myworld.com/";
                    break;
                case "sk":
                    url = "https://sk.myworld.com/";
                    break;
                case "si":
                    url = "https://si.myworld.com/";
                    break;
                case "se":
                    url = "https://se.myworld.com/";
                    break;
                case "pl":
                    url = "https://pl.myworld.com/";
                    break;
                case "no":
                    url = "https://no.myworld.com/";
                    break;
                case "pt":
                    url = "https://pt.myworld.com/";
                    break;
                default:
                    break;
            }

            _context.Driver.Navigate().GoToUrl(url);
            _context.Driver.Manage().Window.Maximize();
            _context.HomePage.CookieButton.Click();
        }

        [When("I choose (.*) from the (.*) level category tree")]
        public void WhenIChooseFromTheLevelCategoryTree(string categoryName, string levelNumber)
        {
            IWebElement category;

            switch (levelNumber)
            {
                case "First":
                    if (categoryName == "DIY & Garden")
                    {
                        category = _context.HomePage.CategoryTreeList("1").Where(c => c.GetAttribute("data-id") == "HJ1RB5E4NB").FirstOrDefault();
                        category.Click();
                    }
                    break;
                case "Second":
                    if (categoryName == "Do It Yourself (DIY)")
                    {
                        category = _context.HomePage.CategoryTreeList("2").Where(c => c.GetAttribute("data-id") == "SD0DTMVMR3").FirstOrDefault();
                        category.Click();
                    }
                    break;
                default:
                    break;
            }
        }

        [When("I press (.*) link from the (.*) level category tree")]
        public void WhenIPressLink(string categoryTreeTitle, string levelNumber)
        {
            switch (levelNumber)
            {
                case "Third":
                    if (categoryTreeTitle == "Do It Yourself (DIY)")
                    {
                        categoryName = _context.HomePage.CategoryTreeTitle("3").GetAttribute("text");
                        categoryLink = _context.HomePage.CategoryTreeTitle("3").GetAttribute("href");
                        _context.HomePage.CategoryTreeTitle("3").Click();
                    }
                    break;
                default:
                    break;
            }
        }

        [When("I sort by (.*)")]
        public void WhenISortBy(string sortBy)
        {
            switch (sortBy)
            {
                case "priceAscending":
                    _context.ProductsPage.SortProductsBy.Click();
                    _context.ProductsPage.SortProductsOptions.Where(o => o.GetAttribute("value") == "cheapest").FirstOrDefault().Click();
                    break;
                default:
                    break;
            }
        }

        [When("I apply (.*) (.*) filter")]
        public void WhenIApplyFilter(string filterType, string filter)
        {
            switch (filterType)
            {
                case "Brand":
                    _context.ProductsPage.FilterFromShowMoreButton.Click();
                    _context.ProductsPage.FilterFrom.Where(f => f.GetAttribute("title") == filter).FirstOrDefault().Click();
                    break;
                case "Colour":
                    _context.ProductsPage.FilterColourShowMoreButton.Click();
                    _context.ProductsPage.FilterColour.Where(f => f.GetAttribute("title") == filter).FirstOrDefault().Click();
                    break;
                default:
                    break;
            }
        }

        [Then("(.*) level category tree is displayed")]
        public void ThenLevelCategoryTreeIsDisplayed(string levelNumber)
        {
            switch (levelNumber)
            {
                case "First":
                    bool isFirstLevelCategoryTreeDisplayed = _context.HomePage.CategoryTreeTitle("1").Displayed;

                    Assert.IsTrue(isFirstLevelCategoryTreeDisplayed, "First level category tree is not displayed.");
                    break;
                case "Second":
                    bool isSecondLevelCategoryTreeDisplayed = _context.HomePage.CategoryTreeTitle("2").Displayed;

                    Assert.IsTrue(isSecondLevelCategoryTreeDisplayed, "First level category tree is not displayed.");
                    break;
                case "Third":
                    bool isThirdLevelCategoryTreeDisplayed = _context.HomePage.CategoryTreeTitle("3").Displayed;

                    Assert.IsTrue(isThirdLevelCategoryTreeDisplayed, "First level category tree is not displayed.");
                    break;
                default:
                    break;
            }
        }

        [Then(@"Correct page is loaded")]
        public void ThenCorrectPageIsLoaded()
        {
            string currentPageCategoryName = _context.ProductsPage.PageTitle.Text;
            string currentUrl = _context.Driver.Url;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(categoryName, currentPageCategoryName, "Correct page is loaded");
                Assert.AreEqual(categoryLink, currentUrl, "Correct page is loaded");
            });
        }

        [Then("(.*) filter is applyed correctly")]
        public void ThenFilterIsApplyedCorrectly(string filter)
        {
            Uri currentUrl = new Uri(_context.Driver.Url);
            string urlDirectory = currentUrl.Segments.LastOrDefault();
            bool isURLContainsFilter = urlDirectory.Contains(filter);

            Assert.IsTrue(isURLContainsFilter);
        }
    }
}
