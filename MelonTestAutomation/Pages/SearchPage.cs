using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class SearchPage : BasePage
    {
        public SearchPage(IWebDriver driver) : base(driver) { }

        public IWebElement Pagination => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".c-pagination")));

        public IWebElement SelectedPage => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[@data-qa='paginationLinkActive']")));

        public IReadOnlyList<IWebElement> PageNumberList => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//nav[@data-qa='pagination']//a[@data-qa='paginationLink']"))).ToList();

        public IWebElement NextPageButton => Wait.Until(d => d.FindElements(By.LinkText("Weiter"))).FirstOrDefault();

        public IWebElement PrevPageButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("Zurück")));
    }
}
