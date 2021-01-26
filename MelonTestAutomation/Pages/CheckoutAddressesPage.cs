using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MelonTestAutomation.Pages
{
    public class CheckoutAddressesPage : BasePage
    {      
        public CheckoutAddressesPage(IWebDriver driver) : base(driver) { }

        public IWebElement ManageYourAddressesButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='https://www.marketplace.myworld.com/en/customer/address']")));

        //public IWebElement FirstName => Driver.FindElement(By.Name("firstName"));

        //public IWebElement FirstNameError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalFirstNameInputErrorRequired']"));

        //public IWebElement LastName => Driver.FindElement(By.Name("lastName"));

        //public IWebElement LastNameError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalLastNameInputErrorRequired']"));

        //public IWebElement StreetAndHouseNumber => Driver.FindElement(By.Name("street1"));

        //public IWebElement StreetAndHouseNumberError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalStreet1InputErrorRequired']"));

        //public IWebElement PostCode => Driver.FindElement(By.Name("zip"));

        //public IWebElement PostCodeError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalZipInputErrorRequired']"));

        //public IWebElement City => Driver.FindElement(By.Name("city"));

        //public IWebElement CityError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalFirstNameInputErrorRequired']"));

        //public IWebElement PhoneNumber => Driver.FindElement(By.Name("phone"));

        //public IWebElement PhoneNumberError => Driver.FindElement(By.XPath("//div[@data-qa='checkoutAddressesModalPhoneInputErrorRequired']"));

        //public IReadOnlyList<IWebElement> InputErrorsList => Driver.FindElements(By.XPath("//div[contains(@data-qa, 'InputErrorRequired')]")).ToList();

        //public IWebElement SaveAddressButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-qa='checkoutAddressesModalAddAddressBtn']")));

        //public IWebElement AddNewAddressButton => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-qa='customer-add-new-address']")));

        //public IWebElement CheckoutAddNewAddressesForm => Driver.FindElement(By.Name("addressForm"));

        //public IWebElement CheckoutAddressesHeading => Driver.FindElement(By.XPath("//h2[@data-qa='checkoutAddressesHeading']"));

        //public IWebElement CreateAddressModalCloseButton => Driver.FindElement(By.XPath("//div[@data-qa='AddressessModal']//button[@class='btn-reset c-modal__btnClose']"));

        //public IWebElement AlternativeAddressData => Driver.FindElements(By.XPath("//address[@data-qa='addressSectionValues']")).LastOrDefault();

        //public IWebElement AlternativeAddressFirstName => AlternativeAddressData.FindElement(By.XPath("//span[@data-qa='addressSectionValuesFirstName']"));

        //public IWebElement AlternativeAddressLastName => AlternativeAddressData.FindElement(By.XPath("//span[@data-qa='addressSectionValuesLastName']"));

        //public IWebElement AlternativeAddressStreetAndNumber => AlternativeAddressData.FindElement(By.XPath("//div[@data-qa='addressSectionValuesStreetAndNumber']"));

        //public IWebElement AlternativeAddressCity => AlternativeAddressData.FindElement(By.XPath("//div[@data-qa='addressSectionValuesCity']"));

        //public IWebElement AlternativeAddressPostCode => AlternativeAddressData.FindElement(By.XPath("//div[@data-qa='addressSectionValuesZip']"));

        //public IWebElement AlternativeAddressPhoneNumber => AlternativeAddressData.FindElement(By.XPath("//div[@data-qa='addressSectionValuesPhone']"));

    }
}
