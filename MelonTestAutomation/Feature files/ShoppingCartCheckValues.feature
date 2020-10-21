@shoppingCart
Feature: ShoppingCartCheckValues
	In order to buy products
	As a user
	I want the values of the items added to the shopping cart to be correct

Scenario: Add three random products to the shopping cart and check the values
	Given I am on the Home Page
	And I press Accept cookies
	When I press All categories dropdown menu
	And I press All categories link
	Then The page with all categories is loaded
	When I open random category
	Then The correct category is loaded
	When I add three random available products to the shopping cart
	And I go to the shopping cart
	Then The correct products are added to the shopping cart
	And The total price of each item in the cart and their total sum are correct
	When I increase the product quantity
	Then The total price of the product is correct