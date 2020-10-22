using OpenQA.Selenium;

namespace MelonTestAutomation.Pages
{
    public class MyworldSigninPage : BasePage
    {        
        public MyworldSigninPage(IWebDriver driver) : base(driver) { }

        public IWebElement LoginInputEmail => Driver.FindElement(By.Name("email"));

        public IWebElement LoginInputPassword => Driver.FindElement(By.Name("password"));

        public IWebElement LoginSubmitButton => Driver.FindElement(By.XPath("//button[@data-qa='loginBtnSubmit']"));

        public IWebElement LoginCashBackSubmitButton => Driver.FindElement(By.XPath("//a[@data-qa='loginCashbackBtnSubmit']"));
        
    }
}
