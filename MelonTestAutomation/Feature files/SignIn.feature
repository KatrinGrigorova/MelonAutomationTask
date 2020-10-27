@signinFeature
Feature: SignIn
	In order to access my account
	As a registered user
	I want to log into myworld successfully

Background:
	Given I am on the Home page

Scenario Outline: Sign in with valid account
	When I press MyAccount
	Then The Sign In page is loaded
	When I fill <email> and <password> with <account_type>
	And I press Login button in <account_type> SignIn form
	Then I am logged with my <account_type> account

	Examples:
		| email               | password    | account_type |
		| waldenschmid@abv.bg | walden666   | myworld      |
		| anabern@abv.bg      | anaCa$hback | cashback     |

@signinThroughShoppingCart
Scenario Outline: Successful login starts and ends on the same page
	Given I press Accept cookies
	When I press All categories dropdown menu
	And I press All categories link
	Then The page with all categories is loaded
	When I open random category
	Then The correct category is loaded
	When I add 1 random available products to the shopping cart
	And I go to the shopping cart
	Then The correct products are added to the shopping cart
	When I press MyAccount
	Then The Sign In page is loaded
	When I fill <email> and <password> with <account_type>
	And I press Login button in <account_type> SignIn form
	Then I am logged with my <account_type> account
	And I am on the Shopping cart page
	And The correct products are added to the shopping cart

	Examples:
		| email               | password    | account_type |
		| waldenschmid@abv.bg | walden666   | myworld      |
		| anabern@abv.bg      | anaCa$hback | cashback     |