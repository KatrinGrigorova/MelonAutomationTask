@signin
Feature: SignIn
	In order to access my account
	As a registered user
	I want to log into myworld successfully

Background:
	Given I am on the Home page
	When I press MyAccount
	Then The Sign In page is loaded

@myworld
Scenario: Sign in with valid myworld account
	When I enter email address waldenschmid@abv.bg
	And I enter passworld walden666
	And I press Login button
	Then I am logged with my myworld account

@cashback
Scenario: Sign in with valid cashback account
	When I press Cashback world button
	Then I am redirected to Cashback world sign in page
	When I enter email address anabern@abv.bg
	And I enter passworld anaCa$hback
	And I press Login button
	Then I am redirected back to myworld website
	And I am logged with my cashback account