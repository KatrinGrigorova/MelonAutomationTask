Feature: Checkout
	In order to finish the order
	As a user
	I need to fill the checkout informtion

@newDeliveryAddressWithInvalidInput
Scenario Outline: Create a new delivery address with invalid input
	Given I am on page <page>
	When I log in with the following <account_type>, <email> and <password>
	Then I am logged with my <account_type> account
	When I add random product to the shopping cart and navigate to checkout
	Then I am on page checkout
	When I press Create new address button
	Then Add new address form is displayed
	When I fill the following address form data: <firstName>, <city>
	And I press Save address button
	Then Validation error messages are displayed below the mandatory fields which are empty

	Examples:
		| email               | password    | account_type | page     | firstName | city    |
		| waldenschmid@abv.bg | walden666   | myworld      | homePage | Walden    | Sofia   |
		| anabern@abv.bg      | anaCa$hback | cashback     | homePage | Ana       | Plovdiv |