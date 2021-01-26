@filters
Feature: Filters
	In order to search for products by certain characteristics
	As a user
	I want to be able to apply filters correctly

@thirdLevelCategoryTree
Scenario Outline: Choose category and apply filters on the Catalog page
	Given I am on domain <domain> Home page
	When I click All categories dropdown menu
	And I open Home % Garden category
	Then Correct page is loaded
	When I sort by <sortBy>
	And I apply Brand filter
	Then Brand filter is applyed correctly
	When I apply Colour filter
	Then Colour filter is applyed correctly

	Examples:
		| domain | sortBy         |
		| de     | priceAscending |
		| en     | priceAscending |