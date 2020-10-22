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

        public IWebElement AddProductToCartButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("button[data-qa='productDetailspageSideBoxBtnsAddToCart']")));

        public IWebElement ProductAmount => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("pdp-productDetails__price")));

        public IWebElement GoToCartButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-qa='productDetailspageSideBoxBtnsGoToCart']")));
    }
}
