using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class CustomerAddressesPage : BasePage
    {      
        public CustomerAddressesPage(IWebDriver driver) : base(driver) { }

        public IWebElement CustomerAddresses => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//ul[@class='menu menu--customer-navigation']//a[@data-id='sidebar-address']")));

        public IWebElement FirstName => Driver.FindElement(By.Id("addressForm_first_name"));

        public IWebElement LastName => Driver.FindElement(By.Id("addressForm_last_name"));

        public IWebElement Street => Driver.FindElement(By.Id("addressForm_address1"));

        public IWebElement HouseNumber => Driver.FindElement(By.Id("addressForm_address2"));

        public IWebElement PostCode => Driver.FindElement(By.Id("addressForm_zip_code"));

        public IWebElement PostCodeError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalZipInputErrorRequired']"));

        public IWebElement City => Driver.FindElement(By.Id("addressForm_city"));

        public IWebElement Country => Driver.FindElement(By.Id("select2-addressForm_iso2_code-container"));

        public IWebElement PhoneNumber => Driver.FindElement(By.Id("addressForm_phone"));

        public IWebElement PhoneNumberError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalPhoneInputErrorRequired']"));

        public IReadOnlyList<IWebElement> InputErrorsList => Driver.FindElements(By.XPath("//form[@name='addressForm']//input[contains(@class, 'input--error')]")).ToList();

        public IWebElement SubmitButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='submit-button']")));

        public IWebElement AddNewAddressButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-qa='customer-add-new-address']")));

        public IWebElement AddNewAddressForm => Driver.FindElement(By.Name("addressForm"));

        public IReadOnlyList<IWebElement> AddressDetails => Driver.FindElements(By.XPath("//ul[@class='display-address menu menu--customer-account']//li")).ToList();

        public IWebElement DeleteAddress => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//form[@name='customer_address_delete_form']//button")));
    }
}
