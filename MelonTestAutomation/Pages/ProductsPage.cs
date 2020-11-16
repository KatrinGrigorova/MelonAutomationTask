using MelonTestAutomation.Drivers;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class ProductsPage : BasePage
    {        
        public ProductsPage(IWebDriver driver) : base(driver) { }

        public IReadOnlyList<IWebElement> AllCategoriesPageCategoryNameList => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//h3[@data-qa='allCategoriesPageCategoryName']//a"))).ToList();

        public IReadOnlyList<IWebElement> CategoryProductList => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//a[@data-qa='searchResultPageProductLink']"))).ToList();

        public IWebElement SortProductsBy => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("c-sortOrderSelect")));

        public IReadOnlyList<IWebElement> SortProductsOptions => SortProductsBy.FindElements(By.XPath("option[contains(@data-qa, 'searchResultPageContentSortingSelectOption')]")).ToList();

        public IReadOnlyList<IWebElement> FilterFrom => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@id='header_filtersDesktop_from_contributorName']//div[@data-qa='searchResultPageFilterOptionLink__from_contributorName']"))).ToList();

        public IWebElement FilterFromShowMoreButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='header_filtersDesktop_from_contributorName']//button[@data-qa='searchResultPageFilterCategoriesShowMoreBtn']")));

        public IReadOnlyList<IWebElement> FilterColour => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@id='header_filtersDesktop_colour']//div[@data-qa='searchResultPageFilterOptionLink__colour']"))).ToList();

        public IWebElement FilterColourShowMoreButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='header_filtersDesktop_colour']//button[@data-qa='searchResultPageFilterCategoriesShowMoreBtn']")));

        public IWebElement PageTitle => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//h1[contains(@class, 'productsList__title')]")));
    }
}
