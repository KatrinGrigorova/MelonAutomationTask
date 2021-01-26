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

        public IWebElement SearchBar => Driver.FindElement(By.XPath("//div[@data-search-id='desktop']//input[contains(@class, 'input')]"));

        //public IWebElement SearchButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hd-search__btnSubmit")));

        public IWebElement AcceptCookiesButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("cookie-notification__accept")));

        public IWebElement AllCategoriesDropDown => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'header__categories-label')]")));

        public IWebElement CategoryTreeTitle(string level)
        {
            return Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath($"//div[@data-qa='{level}']//a[@data-qa='headerCategoriesTreeTitleLink']")));
        }

        public IReadOnlyList<IWebElement> CategoriesList => Driver.FindElements(By.XPath($"//nav[@class='navigation-header']//a[contains(@class, 'navigation-multilevel-node__link--lvl-1')]")).ToList();

        public List<IWebElement> CategoryTreeList(string level)
        {
            return Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath($"//div[@tabindex='{level}']//a[@data-qa='headerCategoriesTreeItemLink']"))).ToList();
        }

        public IWebElement MyWorldMainLogo => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class, 'header__navigation-main')]//div[@data-qa='component logo']")));

        public IReadOnlyList<IWebElement> RemoveItemFromTheCart => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//main[@class='page-layout-cart']//button[@data-qa='add-to-cart-button']"))).ToList();

        public IWebElement ShoppingCartIcon => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//nav[@data-qa='component navigation-top']//cart-counter[@data-qa='component cart-counter']")));

        public IWebElement ShoppingCartQuantity => Driver.FindElement(By.XPath("//nav[@data-qa='component navigation-top']//span[contains(@class, 'cart-counter__quantity')]"));

        public IWebElement UserAccountIconLoggedOut => Driver.FindElement(By.XPath("//li[@class='navigation-top__item']//span[contains(@class, 'navigation-top__link')]"));

        public IWebElement UserAccountIconLoggedIn => Driver.FindElement(By.XPath("//li[@class='navigation-top__item']//a[contains(@href, 'overview')]"));

        public IWebElement CashBackLoginButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@title='Login']")));

        public IWebElement LogoutButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@data-qa='component user-block']//a[@href='https://www.marketplace.myworld.com/logout']")));
    }
}
