using MelonTestAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace MelonTestAutomation.Feature_files
{
    [Binding]
    public class ShoppingCartCheckValuesSteps
    {
        private IWebDriver driver;
        private HomePage homePage;
        private ProductsPage productsPage;
        private ShoppingCartPage shoppingCartPage;
        private string categoryLink;
        private List<string> productList;

        [Given(@"I am on the Home Page")]
        public void GivenIAmOnTheHomePage()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://de.myworld.com/ ");
            driver.Manage().Window.Maximize();

            homePage = new HomePage(driver);
            productsPage = new ProductsPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);
        }

        [Given(@"I press Accept cookies")]
        public void WhenIPressAcceptCookies()
        {
            homePage.CookieButton.Click();
        }

        [When(@"I press All categories dropdown menu")]
        public void WhenIPressAllCategoriesDropdownMenu()
        {
            homePage.AllCategoriesDropDown.Click();
        }

        [When(@"I press All categories link")]
        public void WhenIPressAllCategoriesLink()
        {
            homePage.AllCategories.Click();
        }

        [When(@"I open random category")]
        public void WhenIOpenRandomCategory()
        {
            Random random = new Random();

            int randomCategory = random.Next(productsPage.AllCategoriesPageCategoryNameList.Count);
            IWebElement category = productsPage.AllCategoriesPageCategoryNameList[randomCategory];
            categoryLink = category.GetAttribute("href");
            homePage.ScrollToElement(category);
            category.Click();
        }

        [When(@"I add three random available products to the shopping cart")]
        public void WhenIAddRandomAvailableProductsToTheShoppingCart()
        {
            int shoppingCartQuantity = 0;
            productList = new List<string>();

            while (shoppingCartQuantity < 3)
            {
                int randomProduct = Enumerable.Range(1, productsPage.CategoryProductList.ToList().Count).OrderBy(o => (new Random()).Next()).Take(1).FirstOrDefault();
                
                IWebElement product = productsPage.CategoryProductList[randomProduct - 1];                

                string productName = product.GetAttribute("text");                

                homePage.ScrollToElement(product);
                product.Click();

                bool isAddToCartButtonEnalbed = productsPage.AddProductToCartButton.Enabled;

                if (isAddToCartButtonEnalbed == true)
                {
                    productsPage.AddProductToCartButton.Click();

                    shoppingCartPage.ShoppingCartItemQuantity((shoppingCartQuantity + 1).ToString());

                    shoppingCartQuantity++;

                    productList.Add(productName);
                
                }


                if (shoppingCartQuantity < 3)
                {
                    driver.Navigate().Back();
                }
            }
        }

        [When(@"I go to the shopping cart")]
        public void WhenIGoToTheShoppingCart()
        {
            productsPage.GoToCartButton.Click();
        }

        [When(@"I add one random available product to the shopping cart")]
        public void WhenIAddRandomAvailableProductToTheShoppingCart()
        {
            int randomProduct = Enumerable.Range(1, productsPage.CategoryProductList.ToList().Count).OrderBy(o => (new Random()).Next()).FirstOrDefault();

            IWebElement item = productsPage.CategoryProductList[randomProduct - 1];

            homePage.ScrollToElement(item);
            item.Click();

            productsPage.AddProductToCartButton.Click();
        }

        [When(@"I increase the product quantity")]
        public void WhenIIncreaseTheProductQuantity()
        {
            shoppingCartPage.IncreaseItemQuantityButton[0].Click();
        }

        [Then(@"The page with all categories is loaded")]
        public void ThenThePageWithAllCategoriesIsLoaded()
        {
            Uri currentUrl = new Uri(driver.Url);
            string urlDirectory= currentUrl.Segments.LastOrDefault();

            Assert.AreEqual("categories", urlDirectory, "Wrong page is loaded.");
        }

        [Then(@"The correct category is loaded")]
        public void ThenTheCorrectCategoryIsLoaded()
        {
            var currentUrl = driver.Url;

            Assert.AreEqual(categoryLink, currentUrl, "Wrong category is loaded.");
        }

        [Then(@"The correct products are added to the shopping cart")]
        public void ThenTheCorrectProductsAreAddedToTheShoppingCart()
        {
            var shoppingCartProductNames = shoppingCartPage.ShoppingCartProductsNames.Select(p => p.Text).ToList();

            CollectionAssert.AreEquivalent(productList, shoppingCartProductNames, "The products in the shopping cart don't match the added ones.");
        }


        [Then(@"The total price of each item in the cart and their total sum are correct")]
        public void ThenTheTotalPriceOfEachItemInTheCartAndTheirTotalSumAreCorrect()
        {
            var firstProductCartItem = new
            {
                Amount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-item-price")),
                Quantity = int.Parse(shoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-quantity")),
                TotalAmount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-total-price"))
            };

            var secondProductCartItem = new
            {
                Amount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[1].GetAttribute("data-qa-item-price")),
                Quantity = int.Parse(shoppingCartPage.CartPageItemPriceList[1].GetAttribute("data-qa-quantity")),
                TotalAmount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[1].GetAttribute("data-qa-total-price"))
            };

            var thirdProductCartItem = new
            {
                Amount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[2].GetAttribute("data-qa-item-price")),
                Quantity = int.Parse(shoppingCartPage.CartPageItemPriceList[2].GetAttribute("data-qa-quantity")),
                TotalAmount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[2].GetAttribute("data-qa-total-price"))
            };

            decimal totalAmount = firstProductCartItem.TotalAmount + secondProductCartItem.TotalAmount + thirdProductCartItem.TotalAmount;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(firstProductCartItem.Amount * firstProductCartItem.Quantity, firstProductCartItem.TotalAmount, "The first item total price is not correct.");
                Assert.AreEqual(secondProductCartItem.Amount * secondProductCartItem.Quantity, secondProductCartItem.TotalAmount, "The second item total price is not correct.");
                Assert.AreEqual(thirdProductCartItem.Amount * thirdProductCartItem.Quantity, thirdProductCartItem.TotalAmount, "The third item total price is not correct.");
                Assert.AreEqual(totalAmount, Decimal.Parse(shoppingCartPage.ShoppingCartTotalAmount.GetAttribute("data-qa-price-total")), "Total amount is not correct.");
            });
        }

        [Then(@"The total price of the product is correct")]
        public void ThenTheTotalPriceOfThePdoductIsCorrect()
        {
            var firstProductCartItem = new
            {
                Amount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-item-price")),
                Quantity = int.Parse(shoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-quantity")),
                TotalAmount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-total-price"))
            };

            var secondProductTotalAmount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[1].GetAttribute("data-qa-total-price"));
            var thirdProductTotalAmount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[2].GetAttribute("data-qa-total-price"));

            decimal totalAmount = firstProductCartItem.TotalAmount + secondProductTotalAmount + thirdProductTotalAmount;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(firstProductCartItem.Amount * firstProductCartItem.Quantity, firstProductCartItem.TotalAmount, "The first item total price is not correct.");
                Assert.AreEqual(totalAmount, Decimal.Parse(shoppingCartPage.ShoppingCartTotalAmount.GetAttribute("data-qa-price-total")), "Total amount is not correct.");
            });
        }

        [AfterScenario ("@shoppingCart")]
        public void DesposeWebDriver()
        {
            driver.Dispose();
        }
    }
}
