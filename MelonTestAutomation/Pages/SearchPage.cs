using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class SearchPage : BasePage
    {
        public SearchPage(IWebDriver driver) : base(driver) { }

        public IWebElement Pagination => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//nav[@class='pagination']")));

        public IWebElement SelectedPage => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[@class='pagination__step pagination__step--current']")));

        public IReadOnlyList<IWebElement> PageNumberList => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//a[@class='pagination__step']"))).ToList();

        public IWebElement NextPageButton => Wait.Until(d => d.FindElements(By.XPath("//a[@class='pagination__step pagination__step--next']"))).FirstOrDefault();

        public IWebElement PrevPageButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[@class='pagination__step pagination__step--previous']")));
    }
}
