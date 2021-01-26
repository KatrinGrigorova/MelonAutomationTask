@pagination
Feature: PaginationCheck
	In order to review the search result list
	As a user
	I want the correct pages to be loaded

Scenario: Catalog page full pagination check
	Given I am on page <page>
	When I enter toy to the Search bar
	And I click the Search button
	Then The toy search list is displayed
	When I scroll to the bottom of search result list
	Then Pagination is available
	When I click page 2
	Then Page 2 is loaded
	When I scroll to the bottom of search result list
	And I click page 3
	Then Page 3 is loaded
	When I scroll to the bottom of search result list
	And I click page 4
	Then Page 4 is loaded
	When I click Next page
	Then Page 5 is loaded
	When I click previous page
	Then Page 4 is loaded
	When I scroll to the bottom of search result list
	And I click page 1
	Then Page 1 is loaded
	When I click the last page
	Then The last page is loaded

	Examples:
		| page     |
		| homePage |