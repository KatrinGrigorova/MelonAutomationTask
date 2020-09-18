using MelonTestAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace MelonTestAutomation
{
    [TestFixture]
    public class ShoppingCartTests
    {
        private IWebDriver driver;
        private HomePage homePage;
        private ProductsPage productsPage;
        private ShoppingCartPage shoppingCartPage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Navigate().GoToUrl("https://de.myworld.com/ ");
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test, Order(1)]
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
            Thread.Sleep(1000);

            homePage.CookieButton.Click();
            Thread.Sleep(1000);

            var randomCategory = random.Next(productsPage.AllCategoriesPageCategoryNameList.Count);
            var category = productsPage.AllCategoriesPageCategoryNameList[randomCategory];
            homePage.ScrollToElement(category);
            Thread.Sleep(2000);
            category.Click();

            var randomProducts = Enumerable.Range(1, productsPage.CategoryProductList.ToList().Count).OrderBy(o => (new Random()).Next()).Take(3).ToList();

            var firstProduct = productsPage.CategoryProductList[randomProducts[0]];

            //Add first product and get its amount
            homePage.ScrollToElement(firstProduct);
            Thread.Sleep(3000);
            firstProduct.Click();

            productsPage.AddProductToCartButton.Click();
            Thread.Sleep(2000);

            driver.Navigate().Back();
            Thread.Sleep(2000);

            //Add second product and get its amount
            var secondProduct = productsPage.CategoryProductList[randomProducts[1]];

            homePage.ScrollToElement(secondProduct);
            Thread.Sleep(3000);
            secondProduct.Click();

            productsPage.AddProductToCartButton.Click();
            Thread.Sleep(2000);

            driver.Navigate().Back();
            Thread.Sleep(2000);

            //Add third product and get its amount
            var thirdProduct = productsPage.CategoryProductList[randomProducts[2]];

            homePage.ScrollToElement(thirdProduct);
            Thread.Sleep(3000);
            thirdProduct.Click();

            productsPage.AddProductToCartButton.Click();
            Thread.Sleep(2000);

            productsPage.GoToCartButton.Click();
            Thread.Sleep(2000);

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


        [Test, Order(2)]
        public void When_ChangeItemQuantityInShoppingCart_Then_TotalPricesAreCorrect()
        {
            shoppingCartPage.IncreaseItemQuantityButton[0].Click();

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
    }
}
