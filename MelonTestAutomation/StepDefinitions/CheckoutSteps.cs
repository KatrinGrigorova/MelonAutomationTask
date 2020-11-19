using MelonTestAutomation.Data;
using MelonTestAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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

        string url = "https://de.myworld.com/";

        public CheckoutSteps(WebDriverContext context)
        {
            _context = context;
        }

        [BeforeScenario("@newDeliveryAddressWithInvalidInput")]
        public void BeforeScenario()
        {
            _context.Driver.Navigate().GoToUrl(url);
            _context.Driver.Manage().Window.Maximize();
            _context.HomePage.CookieButton.Click();

            getCategoriesSteps = new ShoppingCartCheckValuesSteps(_context) { };
            getSignInSteps = new SignInSteps(_context, _scenarioContext) { };
        }

        [AfterScenario("@newDeliveryAddressWithInvalidInput")]
        public void DesposeWebDriver()
        {
            _context.Driver.Dispose();
        }
        #endregion


        [When(@"I log in with the following (.*), (.*) and (.*)")]
        public void WhenILogIn(string accountType, string email, string password)
        {
            getSignInSteps.WhenIPressMyAccount();
            getSignInSteps.WhenIFillTheCredentialFields(email, password, accountType);
            getSignInSteps.WhenIPressLoginButton(accountType);
        }

        [When(@"I add random product to the shopping cart and navigate to checkout")]
        public void WhenIAddRandomProductToTheShoppingCartAndNavigateToCheckout()
        {
            getSignInSteps.OpenRandomProductDetails();
            _context.ProductDetailsPage.AddProductToCartButton.Click();
            getCategoriesSteps.WhenIGoToTheShoppingCart();

            _context.ShoppingCartPage.ShoppingCartCheckoutButton.Click();
        }

        [When(@"I press Create new address button")]
        public void WhenIPressCreateNewAddressButton()
        {
            _context.CheckoutAddressesPage.CreateNewAddressButton.Click();
        }

        [When("I fill the following address form data: (.*), (.*)")]
        public void WhenIFillTheFollowingAddressFormData(string firstName, string city)
        {
            _context.CheckoutAddressesPage.FirstName.SendKeys(firstName);
            _context.CheckoutAddressesPage.City.SendKeys(city);
        }

        [When(@"I press Save address button")]
        public void WhenIPressSaveAddressButton()
        {
            _context.CheckoutAddressesPage.SaveAddressButton.Click();
        }

        [Then(@"Add new address form is displayed")]
        public void ThenAddNewAddressFormIsDisplayed()
        {
            bool isAddNewAddressesFormDisplayed = _context.CheckoutAddressesPage.CheckoutAddNewAddressesForm.Displayed;

            Assert.IsTrue(isAddNewAddressesFormDisplayed, "Add new addresses form is not displayed.");
        }

        [Then(@"Validation error messages are displayed below the mandatory fields which are empty")]
        public void ThenValidationErrorMessagesAreDisplayedBelowTheMandatoryFieldsWhichAreEmpty()
        {
            List<string> expectedInputErrors = new List<string>()
            {
                _context.CheckoutAddressesPage.LastNameError.Text,
                _context.CheckoutAddressesPage.StreetAndHouseNumberError.Text,
                _context.CheckoutAddressesPage.PostCodeError.Text,
                _context.CheckoutAddressesPage.PhoneNumberError.Text
            };

            List<string> inputErrors = _context.CheckoutAddressesPage.InputErrorsList.Where(e => e.Displayed).Select(e => e.Text).ToList();

            CollectionAssert.AreEquivalent(expectedInputErrors, inputErrors, "Wrong input error messages.");
        }
    }
}
