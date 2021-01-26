using MelonTestAutomation.Drivers;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class ProductsPage : BasePage
    {        
        public ProductsPage(IWebDriver driver) : base(driver) { }

        public IReadOnlyList<IWebElement> CategoryProductsList => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//a[contains(@class, 'product-item__name')]"))).ToList();

        public IWebElement SortProductsBy => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//custom-select[@data-qa='component custom-select sort']")));

        public IWebElement SortProductsByPriceAsc => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//li[contains(@id, 'price_asc')]")));

        public IWebElement FilterBrandDropDown => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//h4[@data-toggle-target-class-name='js-filter-section__target--brand']")));

        public IReadOnlyList<IWebElement> BrandsFilter => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//span[@data-qa='component checkbox brand[]']//span[@class='checkbox__label checkbox__label--expand']"))).ToList();

        public IReadOnlyList<IWebElement> ColoursFilter => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//li[contains(@class,'filter-color__item')]"))).ToList();

        public IWebElement FilterColourDropDown => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//h4[@data-toggle-target-class-name='js-filter-section__target--color']")));

        public IWebElement PageTitle => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@data-qa='component sort']//h3")));

        public IWebElement BenefitStoreFilter => Driver.FindElements(By.XPath("//h4[contains(@data-toggle-target-class-name, 'benefit_store')]")).FirstOrDefault();

        //public IReadOnlyList<IWebElement> FilterCategoriesList => Driver.FindElements(By.XPath("//button[contains(@class, 'filter-category')]")).ToList();

        public IWebElement BenefitStoreFilterTrue => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[@data-qa='component radio benefit_store']")));

        public IWebElement ApplyFilterButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='filter-section__actions']//button[contains(@class,'button--expand')]")));
        
    }
}
