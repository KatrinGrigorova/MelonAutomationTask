using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class HomePage : BasePage
    {
        
        public HomePage(IWebDriver driver) : base(driver) { }

        public IWebElement SearchBar => Wait.Until(d => d.FindElement(By.Id("hd-search__input")));

        public IWebElement SearchButton => Wait.Until(d => d.FindElement(By.XPath("//button[@data-qa ='headerSearchBtn']")));        

        public IWebElement CookieButton => Wait.Until(d => d.FindElement(By.XPath("//button[@data-qa='cookiesAgreementAcceptBtn']")));

        public IWebElement AllCategoriesDropDown => Wait.Until(d => d.FindElement(By.XPath("//button[@data-qa='headerCategoriesOpenBtnDesktop']")));

        public IWebElement AllCategories => Wait.Until(d => d.FindElement(By.XPath("//div[@data-qa='headerCategoriesTreeLevelOne']//a[@data-qa='headerCategoriesTreeTitleLink']")));

        //public IReadOnlyList<IWebElement> AllCategoriesPageCategoryNameList => Wait.Until(d => d.FindElements(By.XPath("//h3[@data-qa='allCategoriesPageCategoryName']//a"))).ToList();

        //public IReadOnlyList<IWebElement> CategoryProductList => Wait.Until(d => d.FindElements(By.XPath("//a[@data-qa='searchResultPageProductLink']"))).ToList();

        //public IWebElement AddProductToCartButton => Wait.Until(d => d.FindElement(By.XPath("//button[@data-qa='productDetailspageSideBoxBtnsAddToCart']")));

        //public IWebElement ProductAmount => Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'pdp-productDetails__price')]")));

        //public IWebElement GoToCartButton => Wait.Until(d => d.FindElement(By.XPath("//button[@data-qa='productDetailspageSideBoxBtnsGoToCart']")));

        //public IReadOnlyList<IWebElement> CartPageItemPriceList => Wait.Until(d => d.FindElements(By.XPath("//div[@data-qa='shoppingCartViewCont']//h2[@data-qa='cartPageItem__price']"))).ToList();


    }
}
