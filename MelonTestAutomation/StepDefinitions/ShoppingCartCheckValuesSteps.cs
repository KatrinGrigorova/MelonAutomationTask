using MelonTestAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace MelonTestAutomation.StepDefinitions
{
    [Binding]
    public class ShoppingCartCheckValuesSteps
    {
        #region Local Variables, Before and After scenario
        private string categoryLink;
        private List<string> productList;
        private readonly WebDriverContext _context;
        private List<decimal> productsAmount;

        public string CategoryLink => categoryLink;
        public List<string> ProductList => productList;

        public ShoppingCartCheckValuesSteps(WebDriverContext context)
        {
            _context = context;
        }

        string url = "https://de.myworld.com/";

        [BeforeScenario("@shoppingCart")]
        public void BeforeScenario()
        {
            _context.Driver.Navigate().GoToUrl(url);
            _context.Driver.Manage().Window.Maximize();
            _context.HomePage.AcceptCookiesButton.Click();
        }

        [AfterScenario("@shoppingCart")]
        public void DesposeWebDriver()
        {
            _context.Driver.Dispose();
        }
        #endregion

        [When(@"I click All categories dropdown menu")]
        public void WhenIClickAllCategoriesDropdownMenu()
        {
            Actions action = new Actions(_context.Driver);
            action.MoveToElement(_context.HomePage.AllCategoriesDropDown).Perform();
            _context.HomePage.AllCategoriesDropDown.Click();
        }

        [When(@"I open random category type")]
        public void WhenIOpenRandomCategory()
        {
            Random random = new Random();

            int randomCategory = random.Next(_context.HomePage.CategoriesList.Count);
            IWebElement category = _context.HomePage.CategoriesList[randomCategory];
            categoryLink = category.GetAttribute("href");
            _context.HomePage.ScrollToElement(category);
            category.Click();

            bool doesBenefitStoreFilterNotExist = _context.ProductsPage.BenefitStoreFilter == null;

            while (doesBenefitStoreFilterNotExist != false)
            {
                _context.HomePage.AllCategoriesDropDown.Click();
                randomCategory = random.Next(_context.HomePage.CategoriesList.Count);
                category = _context.HomePage.CategoriesList[randomCategory];
                categoryLink = category.GetAttribute("href");
                _context.HomePage.ScrollToElement(category);
                category.Click();

                doesBenefitStoreFilterNotExist = _context.ProductsPage.BenefitStoreFilter == null;
            }
        }

        [When(@"I add (.*) random available products to the shopping cart")]
        public void WhenIAddMultipleRandomAvailableProductsToTheShoppingCart(int productsNumber)
        {
            int shoppingCartQuantity = 0;
            productList = new List<string>();
            productsAmount = new List<decimal>();

            Thread.Sleep(1000);
            _context.ProductsPage.BenefitStoreFilter.Click();
            _context.ProductsPage.BenefitStoreFilterTrue.Click();
            _context.ProductsPage.ApplyFilterButton.Click();

            string categoryPage = _context.Driver.Url;

            while (shoppingCartQuantity < productsNumber)
            {
                int randomProduct = Enumerable.Range(1, _context.ProductsPage.CategoryProductsList.ToList().Count).OrderBy(o => (new Random()).Next()).Take(1).FirstOrDefault();

                IWebElement product = _context.ProductsPage.CategoryProductsList[randomProduct - 1];

                string productName = product.GetAttribute("text").Trim();

                _context.HomePage.ScrollToElement(product);
                Thread.Sleep(2000);
                product.Click();

                bool isAddToCartButtonEnalbed = _context.ProductDetailsPage.AddProductToCartButton.Enabled;
                Thread.Sleep(1000);

                if (isAddToCartButtonEnalbed == true)
                {
                    decimal productAmount = decimal.Parse(_context.ProductDetailsPage.ProductAmount.Text.Trim().Split('€').LastOrDefault());
                    productsAmount.Add(productAmount);

                    _context.ProductDetailsPage.AddProductToCartButton.Click();

                    _context.ShoppingCartPage.ShoppingCartItemQuantity((shoppingCartQuantity + 1).ToString());

                    shoppingCartQuantity++;

                    productList.Add(productName);
                }


                if (shoppingCartQuantity < productsNumber)
                {
                    _context.Driver.Navigate().GoToUrl(categoryPage);
                }
            }
        }

        [When(@"I go to the shopping cart")]
        public void WhenIGoToTheShoppingCart()
        {
            _context.HomePage.ShoppingCartIcon.Click();
        }

        [When(@"I increase the product quantity")]
        public void WhenIIncreaseTheProductQuantity()
        {
            _context.ShoppingCartPage.IncreaseItemQuantityButton[0].Click();
            Thread.Sleep(1000);
        }

        [Then(@"The correct category is loaded")]
        public void ThenTheCorrectCategoryIsLoaded()
        {
            string currentUrl = _context.Driver.Url;

            Assert.AreEqual(categoryLink, currentUrl, "Wrong category is loaded.");
        }

        //[Then(@"The correct products are added to the shopping cart")]
        //public void ThenTheCorrectProductsAreAddedToTheShoppingCart()
        //{
        //    List<string> shoppingCartProductNames = _context.ShoppingCartPage.ShoppingCartProductsNames.Select(p => p.Text).ToList();

        //    CollectionAssert.AreEquivalent(productList, shoppingCartProductNames, "The products in the shopping cart don't match the added ones.");
        //}


        [Then(@"The total amount is correct")]
        public void ThenTheTotalAmountIsCorrect()
        {
            List<IWebElement> shoppingCartProductsAmount = _context.ShoppingCartPage.ShoppingCartProductsAmount.ToList();
            decimal productsSum = shoppingCartProductsAmount.Select(e => decimal.Parse(e.Text.Trim().Split('€').LastOrDefault())).Sum();
            decimal cartTotalAmount = Decimal.Parse(_context.ShoppingCartPage.ShoppingCartTotalAmount.Text.Split('€').LastOrDefault());

            Assert.AreEqual(productsSum, cartTotalAmount, "Total amount is not correct.");
        }

        [Then(@"The total price of the product is correct")]
        public void ThenTheTotalPriceOfThePdoductIsCorrect()
        {
            decimal increasedProductAmount = decimal.Parse(_context.ShoppingCartPage.ShoppingCartProductsAmount[0].Text.Trim().Split('€').LastOrDefault());
            var productQuantity = Int32.Parse(_context.ShoppingCartPage.ProductsQuantity[0].GetAttribute("value"));

            List<IWebElement> shoppingCartProductsAmount = _context.ShoppingCartPage.ShoppingCartProductsAmount.ToList();
            decimal productsSum = shoppingCartProductsAmount.Select(e => decimal.Parse(e.Text.Trim().Split('€').LastOrDefault())).Sum();
            decimal cartTotalAmount = Decimal.Parse(_context.ShoppingCartPage.ShoppingCartTotalAmount.Text.Split('€').LastOrDefault());

            Assert.Multiple(() =>
            {
                Assert.AreEqual(productsAmount[0] * productQuantity, increasedProductAmount, "The total price of the product is not correct.");
                Assert.AreEqual(productsSum, cartTotalAmount, "Total amount is not correct.");
            });
        }
    }
}
