using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class ShoppingCartPage : BasePage
    {        
        public ShoppingCartPage(IWebDriver driver) : base(driver) { }

        public IReadOnlyList<IWebElement> CartPageItemPriceList => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("h2[data-qa='cartPageItem__price']"))).ToList();

        public IWebElement ShoppingCartTotalAmount => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".ch-sideBox__lineValue--total")));

        public IReadOnlyList<IWebElement> IncreaseItemQuantityButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//button[@data-qa='cartPageItemQuantity__increase']"))).ToList();

        public bool ShoppingCartItemQuantity(string quantity) 
        {
            return Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//span[@data-qa='cartPopupItemsQty']"), quantity));
        }

        public IReadOnlyList<IWebElement> ShoppingCartProductsNames => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//a[contains(@data-qa, 'shoppingCartProductLinkDesktop')]"))).ToList();

        public IReadOnlyList<IWebElement> RemoveItemFromTheCart => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//button[contains(@data-qa, 'shoppingCartViewItemRemoveBtn')]"))).ToList();

        public IWebElement ShoppingCartCheckoutButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-qa='checkoutSideboxNextStepBtn']")));

        public IWebElement EmptyShoppingCart => Driver.FindElements(By.XPath("//section[@data-qa='shoppingCartEmptyCont']")).FirstOrDefault();
    }
}
