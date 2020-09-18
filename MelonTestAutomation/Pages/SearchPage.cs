using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class SearchPage : BasePage
    {
        
        public SearchPage(IWebDriver driver) : base(driver) { }      

        public IWebElement Pagination => Wait.Until(d => d.FindElement(By.XPath("//nav[@data-qa='pagination']")));

        public IWebElement SelectedPage => Wait.Until(d => d.FindElement(By.XPath("//a[@data-qa='paginationLinkActive']")));

        public IReadOnlyList<IWebElement> PageNumberList => Wait.Until(d => d.FindElements(By.XPath("//nav[@data-qa='pagination']//a[@data-qa='paginationLink']"))).ToList();

        public IWebElement NextPageButton => Wait.Until(d => d.FindElements(By.XPath("//nav[@data-qa='pagination']//a[@data-qa='paginationLinkNext']"))).FirstOrDefault();

        public IWebElement PrevPageButton => Wait.Until(d => d.FindElement(By.XPath("//nav[@data-qa='pagination']//a[@data-qa='paginationLinkPrev']")));                
    }
}
