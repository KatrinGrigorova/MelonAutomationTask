using MelonTestAutomation.Drivers;
using OpenQA.Selenium;
using System.Linq;

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

        public IWebElement MyAccountNotLoggedIn => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-qa='headerUserNotLoggedIn']")));

        public IWebElement MyAccountLoggedIn => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='headerUserLoggedIn']")));

        public IWebElement MyAccountCashbackIcon => Driver.FindElements(By.XPath("//button[@data-qa='headerUserLoggedIn']//i[contains(@class, 'icon-cashback')]")).FirstOrDefault();
    }
}
