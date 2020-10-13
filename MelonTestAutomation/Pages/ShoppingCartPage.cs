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

        public IReadOnlyList<IWebElement> IncreaseItemQuantityButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@data-qa='shoppingCartViewCont']//li//button[@data-qa='cartPageItemQuantity__increase']"))).ToList();
    }
}
