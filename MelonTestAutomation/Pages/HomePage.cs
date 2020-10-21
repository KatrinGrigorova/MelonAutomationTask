using OpenQA.Selenium;

namespace MelonTestAutomation.Pages
{
    public class HomePage : BasePage
    {
        
        public HomePage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchBar => Driver.FindElement(By.Id("hd-search__input"));

        public IWebElement SearchButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hd-search__btnSubmit")));

        public IWebElement CookieButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".c-cookiesDisclaimer__button")));

        public IWebElement AllCategoriesDropDown => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='headerCategoriesOpenBtnDesktop']")));

        public IWebElement AllCategories => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-qa='headerCategoriesTreeTitleLink']")));
    }
}
