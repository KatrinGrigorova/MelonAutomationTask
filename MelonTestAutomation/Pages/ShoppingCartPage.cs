using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class ShoppingCartPage : BasePage
    {        
        public ShoppingCartPage(IWebDriver driver) : base(driver) { }

        public IReadOnlyList<IWebElement> CartPageItemPriceList => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("h2[data-qa='cartPageItem__price']"))).ToList();

        public IWebElement ShoppingCartTotalAmount => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//li[contains(@class,'cart-summary__total')]")));

        public IReadOnlyList<IWebElement> IncreaseItemQuantityButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='button button--quantity js-quantity-counter__incr']"))).ToList();

        public bool ShoppingCartItemQuantity(string quantity)
        {
            return Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//nav[@class='navigation-top']//span[contains(@class, 'cart-counter__quantity')]"), quantity));
        }
        
        public IReadOnlyList<IWebElement> ShoppingCartProductsNames => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='page-layout-cart__items-wrap']//a[contains(@class, 'product-card-item__title')]"))).ToList();

        public IReadOnlyList<IWebElement> RemoveItemFromTheCart => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//button[contains(@data-qa, 'shoppingCartViewItemRemoveBtn')]"))).ToList();

        public IWebElement ShoppingCartCheckoutButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='cart-summary']//a[@data-qa='cart-go-to-checkout']")));

        public IWebElement EmptyShoppingCart => Driver.FindElements(By.XPath("//section[@data-qa='shoppingCartEmptyCont']")).FirstOrDefault();

        public IReadOnlyList<IWebElement> ShoppingCartProductsAmount => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//article[@class= 'product-card-item product-card-item--cart']//span[@class='money-price__amount ']"))).ToList();

        public IReadOnlyList<IWebElement> ProductsQuantity => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//input[@data-qa='quantity-input']"))).ToList();
    }
}
