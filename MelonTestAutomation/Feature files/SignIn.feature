@signin
Feature: SignIn
	In order to access my account
	As a registered user
	I want to log into myworld successfully

Scenario Outline: Sign in with valid account
	Given I am on the Home page
	When I press MyAccount
	Then The Sign In page is loaded
	When I fill <email> and <password> with <account_type>
	And I press Login button in <account_type> SignIn form
	Then I am logged with my <account_type> account

	Examples:
		| email               | password    | account_type |
		| waldenschmid@abv.bg | walden666   | myworld      |
		| anabern@abv.bg      | anaCa$hback | cashback     |