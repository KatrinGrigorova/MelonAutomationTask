using OpenQA.Selenium;

namespace MelonTestAutomation.Pages
{
    public class CashbackSigninPage : BasePage
    {
        
        public CashbackSigninPage(IWebDriver driver) : base(driver) { }

        public IWebElement LoginInputEmail => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("FormModel_Username")));

        public IWebElement LoginInputPassword => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("FormModel_Password")));

        public IWebElement LoginSubmitButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".btn-primary")));       
    }
}
