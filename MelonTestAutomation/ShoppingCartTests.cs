using MelonTestAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MelonTestAutomation
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Edge)]
    [TestFixture(BrowserType.Firefox)]
    public class ShoppingCartTests : WebDriverFactory
    {
        private IWebDriver driver;
        private BrowserType browserType;
        private HomePage homePage;
        private ProductsPage productsPage;
        private ShoppingCartPage shoppingCartPage;

        public ShoppingCartTests(BrowserType browser)
            : base()
        {
            browserType = browser;
        }

        [SetUp]
        public void OneTimeSetUp()
        {
            driver = WebDriver(browserType);
            driver.Navigate().GoToUrl("https://de.myworld.com/ ");
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void When_AddProductsToShoppingCart_Then_TotalPricesAreCorrect()
        {
            //Arrange
            homePage = new HomePage(driver);
            productsPage = new ProductsPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);

            var random = new Random();
            var randomProductList = new List<IWebElement>();

            //Act
            homePage.AllCategoriesDropDown.Click();

            homePage.AllCategories.Click();

            homePage.CookieButton.Click();

            var randomCategory = random.Next(productsPage.AllCategoriesPageCategoryNameList.Count);
            var category = productsPage.AllCategoriesPageCategoryNameList[randomCategory];
            homePage.ScrollToElement(category);
            category.Click();

            var randomProducts = Enumerable.Range(1, productsPage.CategoryProductList.ToList().Count).OrderBy(o => (new Random()).Next()).Take(3).ToList();

            var firstProduct = productsPage.CategoryProductList[randomProducts[0]];

            //Add first product
            firstProduct.Click();

            productsPage.AddProductToCartButton.Click();
            Thread.Sleep(3000);

            driver.Navigate().Back();

            //Add second product
            var secondProduct = productsPage.CategoryProductList[randomProducts[1]];

            secondProduct.Click();

            productsPage.AddProductToCartButton.Click();
            Thread.Sleep(3000);

            driver.Navigate().Back();

            //Add third product
            var thirdProduct = productsPage.CategoryProductList[randomProducts[2]];

            thirdProduct.Click();

            productsPage.AddProductToCartButton.Click();
            Thread.Sleep(3000);

            productsPage.GoToCartButton.Click();

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

            var totalAmount = firstProductCartItem.TotalAmount + secondProductCartItem.TotalAmount + thirdProductCartItem.TotalAmount;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(firstProductCartItem.Amount * firstProductCartItem.Quantity, firstProductCartItem.TotalAmount, "The first item total price is not correct.");
                Assert.AreEqual(secondProductCartItem.Amount * secondProductCartItem.Quantity, secondProductCartItem.TotalAmount, "The second item total price is not correct.");
                Assert.AreEqual(thirdProductCartItem.Amount * thirdProductCartItem.Quantity, thirdProductCartItem.TotalAmount, "The third item total price is not correct.");
                Assert.AreEqual(totalAmount, Decimal.Parse(shoppingCartPage.ShoppingCartTotalAmount.GetAttribute("data-qa-price-total")), "Total amount is not correct.");
            });            
        }


        [Test]
        public void When_ChangeItemQuantityInShoppingCart_Then_TotalPricesAreCorrect()
        {
            //Arrange
            homePage = new HomePage(driver);
            productsPage = new ProductsPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);

            var random = new Random();
            var randomProductList = new List<IWebElement>();

            //Act
            homePage.AllCategoriesDropDown.Click();

            homePage.AllCategories.Click();

            homePage.CookieButton.Click();

            var randomCategory = random.Next(productsPage.AllCategoriesPageCategoryNameList.Count);
            var category = productsPage.AllCategoriesPageCategoryNameList[randomCategory];
            homePage.ScrollToElement(category);
            category.Click();

            var randomProduct = Enumerable.Range(1, productsPage.CategoryProductList.ToList().Count).OrderBy(o => (new Random()).Next()).FirstOrDefault();

            var item = productsPage.CategoryProductList[randomProduct];

            //Add product to the cart and get the amount
            homePage.ScrollToElement(item);
            item.Click();

            productsPage.AddProductToCartButton.Click();

            productsPage.GoToCartButton.Click();

            shoppingCartPage.IncreaseItemQuantityButton[0].Click();

            var itemAddedToCart = new
            {
                Amount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-item-price")),
                Quantity = int.Parse(shoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-quantity")),
                TotalAmount = Decimal.Parse(shoppingCartPage.CartPageItemPriceList[0].GetAttribute("data-qa-total-price"))
            };           

            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(itemAddedToCart.Amount * itemAddedToCart.Quantity, itemAddedToCart.TotalAmount, "The first item total price is not correct.");
                Assert.AreEqual(itemAddedToCart.TotalAmount, Decimal.Parse(shoppingCartPage.ShoppingCartTotalAmount.GetAttribute("data-qa-price-total")), "Total amount is not correct.");
            });
        }        
    }
}
