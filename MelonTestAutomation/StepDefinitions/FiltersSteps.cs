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
    public class FiltersSteps
    {
        #region Local Variables, Before and After scenario
        private WebDriverContext _context;
        private string url;
        string categoryLink;
        string currentCategoryName;
        private string brandFilterName;
        private string colourFilterName;

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
                    url = "https://www.marketplace.myworld.com/de";
                    break;
                case "en":
                    url = "https://www.marketplace.myworld.com/en";
                    break;
                default:
                    break;
            }

            _context.Driver.Navigate().GoToUrl(url);
            _context.Driver.Manage().Window.Maximize();
            _context.HomePage.AcceptCookiesButton.Click();
        }

        [When(@"I open (.*) category")]
        public void WhenIOpenCategory(string categoryName)
        {            
            switch (categoryName)
            {
                case "Home % Garden":
                    IWebElement category = _context.HomePage.CategoriesList[5];
                    categoryLink = category.GetAttribute("href");
                    currentCategoryName = category.Text;
                    category.Click();
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
                    _context.ProductsPage.SortProductsByPriceAsc.Click();
                    break;
                default:
                    break;
            }
        }

        [When("I apply (.*) filter")]
        public void WhenIApplyFilter(string filterType)
        {
            switch (filterType)
            {
                case "Brand":
                    _context.ProductsPage.FilterBrandDropDown.Click();
                    _context.ProductsPage.BrandsFilter[0].Click();
                    brandFilterName = _context.ProductsPage.BrandsFilter[0].Text.Split('\r').FirstOrDefault();
                    _context.ProductsPage.ApplyFilterButton.Click();
                    break;
                case "Colour":
                    _context.ProductsPage.FilterColourDropDown.Click();
                    _context.ProductsPage.ColoursFilter[0].Click();
                    colourFilterName = _context.ProductsPage.ColoursFilter[0].Text;
                    _context.ProductsPage.ApplyFilterButton.Click();

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
                Assert.AreEqual(currentCategoryName, currentPageCategoryName, "Correct page is loaded");
                Assert.AreEqual(categoryLink, currentUrl, "Correct page is loaded");
            });
        }

        [Then("(.*) filter is applyed correctly")]
        public void ThenFilterIsApplyedCorrectly(string filterType)
        {
            Uri currentUrl;
            string urlDirectory;

            switch (filterType)
            {
                case "Brand":
                    currentUrl = new Uri(_context.Driver.Url);
                    urlDirectory = HttpUtility.ParseQueryString(currentUrl.Query).Get("brand[]");

                    Assert.AreEqual(brandFilterName, urlDirectory, "Wrong brand filter!");
                    break;
                case "Colour":
                    currentUrl = new Uri(_context.Driver.Url);
                    urlDirectory = HttpUtility.ParseQueryString(currentUrl.Query).Get("color[]");

                    Assert.AreEqual(colourFilterName, urlDirectory, "Wrong color filter!");
                    break;
                default:
                    break;
            }
        }
    }
}
