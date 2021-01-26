@shoppingCart
Feature: ShoppingCartCheckValues
	In order to buy products
	As a user
	I want the values of the items added to the shopping cart to be correct

Scenario: Add n random products to the shopping cart and check the values
	Given I am on page homePage
	When I click All categories dropdown menu
	And I open random category type
	Then The correct category is loaded
	When I add 3 random available products to the shopping cart
	And I go to the shopping cart
	#Then The correct products are added to the shopping cart 
	Then The total amount is correct
	When I increase the product quantity
	Then The total price of the product is correct

	#Examples:
	#	| page     | 
	#	| homePage |