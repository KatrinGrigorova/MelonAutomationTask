using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class ProductsPage : BasePage
    {
        
        public ProductsPage(IWebDriver driver) : base(driver) { }       

        public IReadOnlyList<IWebElement> AllCategoriesPageCategoryNameList => Wait.Until(d => d.FindElements(By.XPath("//h3[@data-qa='allCategoriesPageCategoryName']//a"))).ToList();

        public IReadOnlyList<IWebElement> CategoryProductList => Wait.Until(d => d.FindElements(By.XPath("//a[@data-qa='searchResultPageProductLink']"))).ToList();

        public IWebElement AddProductToCartButton => Wait.Until(d => d.FindElement(By.XPath("//button[@data-qa='productDetailspageSideBoxBtnsAddToCart']")));

        public IWebElement ProductAmount => Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'pdp-productDetails__price')]")));

        public IWebElement GoToCartButton => Wait.Until(d => d.FindElement(By.XPath("//button[@data-qa='productDetailspageSideBoxBtnsGoToCart']")));
    }
}
