@signinFeature
Feature: SignIn
	In order to access my account
	As a registered user
	I want to log into myworld successfully

Scenario Outline: Sign in with valid account
	Given I am on page <page>
	When I press MyAccount
	Then The Sign In page is loaded
	When I fill <email> and <password> with <account_type>
	And I press Login button in <account_type> SignIn form
	Then I am logged with my <account_type> account

	Examples:
		| email               | password    | account_type | page     |
		| waldenschmid@abv.bg | walden666   | myworld      | homePage |
		| anabern@abv.bg      | anaCa$hback | cashback     | homePage |

@signinFromAnyPage
Scenario Outline: Successful login starts and ends on the same page
	Given I am on page <page>
	When I press MyAccount
	Then The Sign In page is loaded
	When I fill <email> and <password> with <account_type>
	And I press Login button in <account_type> SignIn form
	Then I am logged with my <account_type> account
	And I am on page <page>

	Examples:
		| email               | password    | account_type | page                     |
		| waldenschmid@abv.bg | walden666   | myworld      | homePage                 |
		| anabern@abv.bg      | anaCa$hback | cashback     | categoriesPage           |
		| waldenschmid@abv.bg | walden666   | myworld      | randomCategoryPage       |
		| anabern@abv.bg      | anaCa$hback | cashback     | shoppingCartPage         |
		| waldenschmid@abv.bg | walden666   | myworld      | randomProductDetailsPage |