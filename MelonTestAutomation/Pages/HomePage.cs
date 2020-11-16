using MelonTestAutomation.Drivers;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MelonTestAutomation.Pages
{
    public class HomePage : BasePage
    {
        
        public HomePage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchBar => Driver.FindElement(By.Id("hd-search__input"));

        public IWebElement SearchButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hd-search__btnSubmit")));

        public IWebElement CookieButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".c-cookiesDisclaimer__button")));

        public IWebElement AllCategoriesDropDown => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='headerCategoriesOpenBtnDesktop']")));

        public IWebElement MyAccountNotLoggedIn => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-qa='headerUserNotLoggedIn']")));

        public IWebElement MyAccountLoggedIn => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='headerUserLoggedIn']")));

        public IWebElement MyAccountCashbackIcon => MyAccountLoggedIn.FindElements(By.XPath("//button[@data-qa='headerUserLoggedIn']//i[contains(@class, 'icon-cashback')]")).FirstOrDefault();

        public IWebElement CategoryTreeTitle(string level)
        {
            return Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath($"//div[@tabindex='{level}']//a[@data-qa='headerCategoriesTreeTitleLink']")));
        }

        public List<IWebElement> CategoryTreeList(string level)
        {
            return Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath($"//div[@tabindex='{level}']//a[@data-qa='headerCategoriesTreeItemLink']"))).ToList();
        }

        public IWebElement MyWorldMainLogo => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-qa='headerMainLogoLink']")));

        public IReadOnlyList<IWebElement> RemoveItemFromTheCart => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//button[contains(@data-qa, 'shoppingCartPopUpRemoveItemBtn')]"))).ToList();

        public IWebElement ShoppingCartIcon => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-qa='headerShoppingCartLink']"))); 
    }
}
