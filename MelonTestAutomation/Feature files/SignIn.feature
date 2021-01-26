@signinFeature
Feature: SignIn
	In order to access my account
	As a registered user
	I want to log into myworld successfully

Scenario Outline: Sign in with valid account
	Given I am on page homePage
	When I go to User Account and press Cashback button
	When I fill <email> and <password>
	And I click Login button
	Then I am logged in

	Examples:
		| email          | password    |
		| anabern@abv.bg | anaCa$hback |

@signinFromAnyPage
Scenario Outline: Successful login starts and ends on the same page
	Given I am on page <page>
	When I go to User Account and press Cashback button
	When I fill <email> and <password>
	And I click Login button
	Then I am logged in
	And I am on page <page>

	Examples:
		| email          | password    | page                     |
		| anabern@abv.bg | anaCa$hback | homePage                 |
		| anabern@abv.bg | anaCa$hback | randomCategoryPage       |
		| anabern@abv.bg | anaCa$hback | shoppingCartPage         |
		| anabern@abv.bg | anaCa$hback | randomProductDetailsPage |
		| anabern@abv.bg | anaCa$hback | checkout                 |