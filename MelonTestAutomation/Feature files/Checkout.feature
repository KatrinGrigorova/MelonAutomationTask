@checkout
Feature: Checkout
	In order to finish the order
	As a user
	I need to fill the checkout informtion

@newDeliveryAddressWithInvalidInput
Scenario Outline: Create a new delivery address with invalid input
	Given I am on page <page>
	When I log in with the following <email> and <password>
	Then I am logged in
	When I add random product to the shopping cart and navigate to checkout
	Then I am on page checkout
	When I click on Manage your addresses button
	Then I am on page customerAddresses
	When I click Add new address button
	Then Add new address form is displayed
	When I fill out the following address form data: <firstName>, <lastName>, <city>, <street>, <houseNumber>, <postCode>, <phoneNumber>
	And I click Save address button
	Then Validation error messages are displayed below the mandatory fields which are empty

	Examples:
		| email          | password    | page     | firstName | lastName | city | street | houseNumber | postCode | phoneNumber |
		| anabern@abv.bg | anaCa$hback | homePage | Ana       | Bern     |      |        |             |          |             |

@newDeliveryAddressWithValidInput
Scenario Outline: Create a new delivery address with valid input
	Given I am on page <page>
	When I log in with the following <email> and <password>
	Then I am logged in
	When I add random product to the shopping cart and navigate to checkout
	Then I am on page checkout
	When I click on Manage your addresses button
	Then I am on page customerAddresses
	When I click Add new address button
	Then Add new address form is displayed
	When I fill out the following address form data: <firstName>, <lastName>, <city>, <street>, <houseNumber>, <postCode>, <phoneNumber>
	And I click Save address button
	Then The address is added to the list with available addresses

	Examples:
		| email          | password    | page     | firstName | lastName | city    | street | houseNumber | postCode | phoneNumber |
		| anabern@abv.bg | anaCa$hback | homePage | Ana       | Bern     | Plovdiv | Street | 420         | 4000     | 0888111111  |