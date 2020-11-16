using MelonTestAutomation.Drivers;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class ProductDetailsPage : BasePage
    {        
        public ProductDetailsPage(IWebDriver driver) : base(driver) { }

        public IWebElement AddProductToCartButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("button[data-qa='productDetailspageSideBoxBtnsAddToCart']")));

        //public IWebElement ProductAmount => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("pdp-productDetails__price")));

        public IWebElement GoToCartButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-qa='productDetailspageSideBoxBtnsGoToCart']")));

        public IWebElement ProductDetailsQuantityBox => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@data-qa='productDetailsPageQtyInput']")));

    }
}
