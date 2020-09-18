using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class ShoppingCartPage : BasePage
    {        
        public ShoppingCartPage(IWebDriver driver) : base(driver) { }       

        public IReadOnlyList<IWebElement> CartPageItemPriceList => Wait.Until(d => d.FindElements(By.XPath("//div[@data-qa='shoppingCartViewCont']//h2[@data-qa='cartPageItem__price']"))).ToList();

        public IWebElement ShoppingCartTotalAmount => Wait.Until(d => d.FindElement(By.XPath("//section[@data-qa='cartOrderReview']//span[@data-qa='checkoutSideboxTotal']")));

        public IReadOnlyList<IWebElement> IncreaseItemQuantityButton => Wait.Until(d => d.FindElements(By.XPath("//div[@data-qa='shoppingCartViewCont']//li//button[@data-qa='cartPageItemQuantity__increase']"))).ToList();
    }
}
