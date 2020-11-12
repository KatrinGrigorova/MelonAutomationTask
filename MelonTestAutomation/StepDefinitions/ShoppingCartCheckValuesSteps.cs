using MelonTestAutomation.Drivers;
using MelonTestAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _context.HomePage.CookieButton.Click();
        }

        [AfterScenario("@shoppingCart")]
        public void DesposeWebDriver()
        {
            _context.Driver.Dispose();
        }
        #endregion

        [When(@"I press All categories dropdown menu")]
        public void WhenIPressAllCategoriesDropdownMenu()
        {
            _context.HomePage.AllCategoriesDropDown.Click();
        }

        [When("I press All categories title")]
        public void WhenIPressAllCategoriesTitle()
        {
            _context.HomePage.CategoryTreeTitle("1").Click();
        }

        [When(@"I open random category")]
        public void WhenIOpenRandomCategory()
        {
            Random random = new Random();

            int randomCategory = random.Next(_context.ProductsPage.AllCategoriesPageCategoryNameList.Count);
            IWebElement category = _context.ProductsPage.AllCategoriesPageCategoryNameList[randomCategory];
            categoryLink = category.GetAttribute("href");
            _context.HomePage.ScrollToElement(category);
            category.Click();
        }

        [When(@"I add (.*) random available products to the shopping cart")]
        public void WhenIAddRandomAvailableProductsToTheShoppingCart(int productsNumber)
        {
            int shoppingCartQuantity = 0;
            productList = new List<string>();

            while (shoppingCartQuantity < productsNumber)
            {
                int randomProduct = Enumerable.Range(1, _context.ProductsPage.CategoryProductList.ToList().Count).OrderBy(o => (new Random()).Next()).Take(1).FirstOrDefault();

                IWebElement product = _context.ProductsPage.CategoryProductList[randomProduct - 1];

                string productName = product.GetAttribute("text").Trim();

                _context.HomePage.ScrollToElement(product);
                product.Click();

                bool isAddToCartButtonEnalbed = _context.ProductsPage.AddProductToCartButton.Enabled;

                if (isAddToCartButtonEnalbed == true)
                {
                    _context.ProductsPage.AddProductToCartButton.Click();

                    _context.ShoppingCartPage.ShoppingCartItemQuantity((shoppingCartQuantity + 1).ToString());

                    shoppingCartQuantity++;

                    productList.Add(productName);

                }


                if (shoppingCartQuantity < productsNumber)
                {
                    _context.Driver.Navigate().Back();
                }
            }
        }

        [When(@"I go to the shopping cart")]
        public void WhenIGoToTheShoppingCart()
        {
            _context.ProductsPage.GoToCartButton.Click();
        }

        [When(@"I increase the product quantity")]
        public void WhenIIncreaseTheProductQuantity()
        {
            _context.ShoppingCartPage.IncreaseItemQuantityButton[0].Click();
        }

        [Then(@"The page with all categories is loaded")]
        public void ThenThePageWithAllCategoriesIsLoaded()
        {
            Uri currentUrl = new Uri(_context.Driver.Url);
            string urlDirectory = currentUrl.Segments.LastOrDefault();

            Assert.AreEqual("categories", urlDirectory, "Wrong page is loaded.");
        }

        [Then(@"The correct category is loaded")]
        public void ThenTheCorrectCategoryIsLoaded()
        {
            string currentUrl = _context.Driver.Url;

            Assert.AreEqual(categoryLink, currentUrl, "Wrong category is loaded.");
        }

        [Then(@"The correct products are added to the shopping cart")]
        public void ThenTheCorrectProductsAreAddedToTheShoppingCart()
        {
            List<string> shoppingCartProductNames = _context.ShoppingCartPage.ShoppingCartProductsNames.Select(p => p.Text).ToList();

            CollectionAssert.AreEquivalent(productList, shoppingCartProductNames, "The products in the shopping cart don't match the added ones.");
        }


        [Then(@"The total price of each item in the cart and their total sum are correct")]
        public void ThenTheTotalPriceOfEachItemInTheCartAndTheirTotalSumAreCorrect()
        {
            var firstProductCartItem = new
            {
                Amount = Decimal.Parse(_context.ShoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-item-price")),
                Quantity = int.Parse(_context.ShoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-quantity")),
                TotalAmount = Decimal.Parse(_context.ShoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-total-price"))
            };

            var secondProductCartItem = new
            {
                Amount = Decimal.Parse(_context.ShoppingCartPage.CartPageItemPriceList[1].GetAttribute("data-qa-item-price")),
                Quantity = int.Parse(_context.ShoppingCartPage.CartPageItemPriceList[1].GetAttribute("data-qa-quantity")),
                TotalAmount = Decimal.Parse(_context.ShoppingCartPage.CartPageItemPriceList[1].GetAttribute("data-qa-total-price"))
            };

            var thirdProductCartItem = new
            {
                Amount = Decimal.Parse(_context.ShoppingCartPage.CartPageItemPriceList[2].GetAttribute("data-qa-item-price")),
                Quantity = int.Parse(_context.ShoppingCartPage.CartPageItemPriceList[2].GetAttribute("data-qa-quantity")),
                TotalAmount = Decimal.Parse(_context.ShoppingCartPage.CartPageItemPriceList[2].GetAttribute("data-qa-total-price"))
            };

            decimal totalAmount = firstProductCartItem.TotalAmount + secondProductCartItem.TotalAmount + thirdProductCartItem.TotalAmount;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(firstProductCartItem.Amount * firstProductCartItem.Quantity, firstProductCartItem.TotalAmount, "The first item total price is not correct.");
                Assert.AreEqual(secondProductCartItem.Amount * secondProductCartItem.Quantity, secondProductCartItem.TotalAmount, "The second item total price is not correct.");
                Assert.AreEqual(thirdProductCartItem.Amount * thirdProductCartItem.Quantity, thirdProductCartItem.TotalAmount, "The third item total price is not correct.");
                Assert.AreEqual(totalAmount, Decimal.Parse(_context.ShoppingCartPage.ShoppingCartTotalAmount.GetAttribute("data-qa-price-total")), "Total amount is not correct.");
            });
        }

        [Then(@"The total price of the product is correct")]
        public void ThenTheTotalPriceOfThePdoductIsCorrect()
        {
            var firstProductCartItem = new
            {
                Amount = Decimal.Parse(_context.ShoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-item-price")),
                Quantity = int.Parse(_context.ShoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-quantity")),
                TotalAmount = Decimal.Parse(_context.ShoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-total-price"))
            };

            var secondProductTotalAmount = Decimal.Parse(_context.ShoppingCartPage.CartPageItemPriceList[1].GetAttribute("data-qa-total-price"));
            var thirdProductTotalAmount = Decimal.Parse(_context.ShoppingCartPage.CartPageItemPriceList[2].GetAttribute("data-qa-total-price"));

            decimal totalAmount = firstProductCartItem.TotalAmount + secondProductTotalAmount + thirdProductTotalAmount;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(firstProductCartItem.Amount * firstProductCartItem.Quantity, firstProductCartItem.TotalAmount, "The first item total price is not correct.");
                Assert.AreEqual(totalAmount, Decimal.Parse(_context.ShoppingCartPage.ShoppingCartTotalAmount.GetAttribute("data-qa-price-total")), "Total amount is not correct.");
            });
        }       
    }
}
