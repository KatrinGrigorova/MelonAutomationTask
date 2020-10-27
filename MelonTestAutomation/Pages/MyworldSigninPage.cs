using OpenQA.Selenium;

namespace MelonTestAutomation.Pages
{
    public class MyworldSigninPage : BasePage
    {        
        public MyworldSigninPage(IWebDriver driver) : base(driver) { }

        public IWebElement LoginInputEmail => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("email")));

        public IWebElement LoginInputPassword => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("password")));

        public IWebElement LoginSubmitButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='loginBtnSubmit']")));

        public IWebElement LoginCashBackSubmitButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-qa='loginCashbackBtnSubmit']")));
        
    }
}
