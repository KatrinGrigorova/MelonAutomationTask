@filters
Feature: Filters
	In order to search for products by certain characteristics
	As a user
	I want to be able to apply filters correctly

@thirdLevelCategoryTree
Scenario Outline: Choose category from third level category tree and apply filters on the Catalog page
	Given I am on domain <domain> Home page
	When I press All categories dropdown menu
	Then First level category tree is displayed
	When I choose DIY & Garden from the First level category tree
	Then Second level category tree is displayed
	When I choose Do It Yourself (DIY) from the Second level category tree
	Then Third level category tree is displayed
	When I press Do It Yourself (DIY) link from the Third level category tree
	Then Correct page is loaded
	When I sort by <sortBy>
	And I apply Brand filter
	Then Brand filter is applyed correctly
	When I apply Colour filter
	Then Colour filter is applyed correctly

	Examples:
		| domain | sortBy         |
		| de     | priceAscending |
		| at     | priceAscending |
		| ch     | priceAscending |
		| it     | priceAscending |
		| hu     | priceAscending |
		| cz     | priceAscending |
		| sk     | priceAscending |
		| si     | priceAscending |
		| se     | priceAscending |
		| pl     | priceAscending |
		| no     | priceAscending |
		| pt     | priceAscending |