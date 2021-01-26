using OpenQA.Selenium;

namespace MelonTestAutomation.Pages
{
    public class ProductDetailsPage : BasePage
    {        
        public ProductDetailsPage(IWebDriver driver) : base(driver) { }

        public IWebElement AddProductToCartButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='container__inner']//button[@data-qa='add-to-cart-button']")));

        //public IWebElement GoToCartButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-qa='productDetailspageSideBoxBtnsGoToCart']")));

        public IWebElement ProductDetailsQuantityBox => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@data-qa='quantity-input']")));

        public IWebElement ProductDetailsIncreaseQuantity => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class='counter__incr')]")));

        public IWebElement ProductAmount => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='money-price money-price--pdp']//span[@class='money-price__amount ']")));
    }
}