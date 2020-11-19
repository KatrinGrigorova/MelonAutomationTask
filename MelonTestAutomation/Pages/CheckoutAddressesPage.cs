using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class CheckoutAddressesPage : BasePage
    {      
        public CheckoutAddressesPage(IWebDriver driver) : base(driver) { }

        public IWebElement FirstName => Driver.FindElement(By.Name("firstName"));

        //public IWebElement FirstNameError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalFirstNameInputErrorRequired']"));

        public IWebElement LastName => Driver.FindElement(By.Name("lastName"));

        public IWebElement LastNameError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalLastNameInputErrorRequired']"));

        public IWebElement StreetAndHouseNumber => Driver.FindElement(By.Name("street1"));

        public IWebElement StreetAndHouseNumberError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalStreet1InputErrorRequired']"));

        public IWebElement PostCode => Driver.FindElement(By.Name("zip"));

        public IWebElement PostCodeError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalZipInputErrorRequired']"));

        public IWebElement City => Driver.FindElement(By.Name("city"));

        //public IWebElement CityError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalFirstNameInputErrorRequired']"));

        public IWebElement PhoneNumber => Driver.FindElement(By.Name("phone"));

        public IWebElement PhoneNumberError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalPhoneInputErrorRequired']"));

        public IReadOnlyList<IWebElement> InputErrorsList => Driver.FindElements(By.XPath("//div[contains(@data-qa, 'InputErrorRequired')]")).ToList();

        public IWebElement SaveAddressButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='checkoutAddressesModalAddAddressBtn']")));

        public IWebElement CreateNewAddressButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='profileAddressesNewAddressModalOpen']")));

        public IWebElement CheckoutAddNewAddressesForm => Driver.FindElement(By.XPath("//form[@data-qa='addressAddEditForm']"));

        public IWebElement CheckoutAddressesHeading => Driver.FindElement(By.XPath("//h2[@data-qa='checkoutAddressesHeading']"));
    }
}
