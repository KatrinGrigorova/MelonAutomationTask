using MelonTestAutomation.Drivers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace MelonTestAutomation.StepDefinitions
{
    [Binding]
    public class CheckoutSteps
    {
        #region Local Variables, Before and After scenario
        private WebDriverContext _context;
        private ScenarioContext _scenarioContext;
        private ShoppingCartCheckValuesSteps getCategoriesSteps;
        private SignInSteps getSignInSteps;
        private string addressFormFirstName;
        private string addressFormLastName;
        private string addressFormStreet;
        private string addressFormHouseNumber;
        private string addressFormCity;
        private string addressFormPostCode;
        private string addressFormPhoneNumber;
        private string addressFormCountry;

        string url = "https://de.myworld.com/";

        public CheckoutSteps(WebDriverContext context, ScenarioContext scenarioContext)
        {
            _context = context;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario("@checkout")]
        public void BeforeScenario()
        {
            _context.Driver.Navigate().GoToUrl(url);
            _context.Driver.Manage().Window.Maximize();
            _context.HomePage.AcceptCookiesButton.Click();

            getCategoriesSteps = new ShoppingCartCheckValuesSteps(_context) { };
            getSignInSteps = new SignInSteps(_context, _scenarioContext) { };
        }

        [AfterScenario("@checkout")]
        public void DesposeWebDriver()
        {
            if (_scenarioContext.ScenarioInfo.Tags.Contains("newDeliveryAddressWithValidInput"))
            {
                DeleteTheLastAddedAddress();
                getSignInSteps.DeleteProductsFromShoppingCart();
                _context.Driver.Dispose();
            }
            else
            {
                getSignInSteps.DeleteProductsFromShoppingCart();
                _context.Driver.Dispose();
            }
        }
        #endregion


        [When(@"I log in with the following (.*) and (.*)")]
        public void WhenILogIn(string email, string password)
        {
            getSignInSteps.WhenIGoToUserAccountAndPressCashbackButton();
            getSignInSteps.WhenIFillTheCredentialFields(email, password);
            getSignInSteps.WhenIClickLoginButton();
        }

        [When(@"I add random product to the shopping cart and navigate to checkout")]
        public void WhenIAddRandomProductToTheShoppingCartAndNavigateToCheckout()
        {
            getSignInSteps.OpenRandomProductDetails();
            _context.ProductDetailsPage.AddProductToCartButton.Click();
            getCategoriesSteps.WhenIGoToTheShoppingCart();

            _context.ShoppingCartPage.ShoppingCartCheckoutButton.Click();
        }

        [When(@"I click on Manage your addresses button")]
        public void WhenIClickOnManageYourAddressesButton()
        {
            _context.CheckoutAddressesPage.ManageYourAddressesButton.Click();
        }


        [When(@"I click Add new address button")]
        public void WhenIClickAddNewAddressButton()
        {
            _context.CustomerAddressesPage.AddNewAddressButton.Click();
        }

        [When("I fill out the following address form data: (.*), (.*), (.*), (.*), (.*), (.*), (.*)")]
        public void WhenIFillOutTheFollowingAddressFormData(string firstName, string lastName, string city, string street, string houseNumber, string postCode, string phoneNumber)
        {
            //_context.CustomerAddressesPage.FirstName.SendKeys(firstName);
            //_context.CustomerAddressesPage.LastName.SendKeys(lastName);
            _context.CustomerAddressesPage.City.SendKeys(city);
            _context.CustomerAddressesPage.Street.SendKeys(street);
            _context.CustomerAddressesPage.HouseNumber.SendKeys(houseNumber);
            _context.CustomerAddressesPage.PostCode.SendKeys(postCode);
            _context.CustomerAddressesPage.PhoneNumber.SendKeys(phoneNumber);

            addressFormFirstName = firstName;
            addressFormLastName = lastName;
            addressFormStreet = street;
            addressFormHouseNumber = houseNumber;
            addressFormCity = city;
            addressFormPostCode = postCode;
            addressFormPhoneNumber = phoneNumber;
            addressFormCountry = _context.CustomerAddressesPage.Country.Text;
        }

        [When(@"I click Save address button")]
        public void WhenIClickSaveAddressButton()
        {
            _context.CustomerAddressesPage.SubmitButton.Click();
        }

        [Then(@"Add new address form is displayed")]
        public void ThenAddNewAddressFormIsDisplayed()
        {
            bool isAddNewAddressesFormDisplayed = _context.CustomerAddressesPage.AddNewAddressForm.Displayed;
            Uri currentUrl = new Uri(_context.Driver.Url);
            string urlDirectory = currentUrl.AbsolutePath;

            Assert.Multiple(() =>
            {
                Assert.True(urlDirectory.Contains("customer/address/new"), "Wrong page is loaded.");
                Assert.IsTrue(isAddNewAddressesFormDisplayed, "Add new addresses form is not displayed.");
            });

        }

        [Then(@"Validation error messages are displayed below the mandatory fields which are empty")]
        public void ThenValidationErrorMessagesAreDisplayedBelowTheMandatoryFieldsWhichAreEmpty()
        {
            List<string> expectedInputErrors = new List<string>()
            {
                _context.CustomerAddressesPage.City.GetAttribute("id"),
                _context.CustomerAddressesPage.Street.GetAttribute("id"),
                _context.CustomerAddressesPage.HouseNumber.GetAttribute("id"),
                _context.CustomerAddressesPage.PostCode.GetAttribute("id"),
                _context.CustomerAddressesPage.PhoneNumber.GetAttribute("id")
            };

            List<string> inputErrors = _context.CustomerAddressesPage.InputErrorsList.Select(e => e.GetAttribute("id")).ToList();

            CollectionAssert.AreEquivalent(expectedInputErrors, inputErrors, "Wrong fields got an error messages.");
        }

        [Then(@"The address is added to the list with available addresses")]
        public void ThenTheAddressIsAddedToTheListWithAvailableAddresses()
        {
            string firstAndLastName = _context.CustomerAddressesPage.AddressDetails[0].Text;
            string streetAndNumber = _context.CustomerAddressesPage.AddressDetails[1].Text;
            string postCodeCityAndCountry = _context.CustomerAddressesPage.AddressDetails[3].Text;
            string phoneNumber = _context.CustomerAddressesPage.AddressDetails[4].Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(addressFormFirstName + " " + addressFormLastName, firstAndLastName, "The name is not correct.");
                Assert.AreEqual(addressFormStreet + " " + addressFormHouseNumber, streetAndNumber, "The street and number are not correct.");
                Assert.AreEqual(addressFormPostCode + " " + addressFormCity + ", " + addressFormCountry, postCodeCityAndCountry, "The post code/city/country are not correct.");
                Assert.AreEqual(addressFormPhoneNumber, phoneNumber, "The phone number is not correct.");
            });
        }


        #region Internal methods
        public void DeleteTheLastAddedAddress()
        {
            _context.HomePage.UserAccountIconLoggedIn.Click();
            _context.CustomerAddressesPage.CustomerAddresses.Click();
            _context.CustomerAddressesPage.DeleteAddress.Click();
        }
        #endregion
    }
}