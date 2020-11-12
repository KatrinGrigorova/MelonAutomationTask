@pagination
Feature: PaginationCheck
	In order to review the search result list
	As a user
	I want the correct pages to be loaded

Scenario: Catalog page full pagination check
	Given I am on page <page>
	When I enter book to the Search bar
	And I press the Search button
	Then The book search list is displayed
	When I scroll to the bottom of search result list
	Then Pagination is available
	When I press page 2
	Then Page 2 is loaded
	When I scroll to the bottom of search result list
	And I press page 3
	Then Page 3 is loaded
	When I scroll to the bottom of search result list
	And I press page 4
	Then Page 4 is loaded
	When I press Next page
	Then Page 5 is loaded
	When I press previous page
	Then Page 4 is loaded
	When I scroll to the bottom of search result list
	And I press second ellipsis
	Then Page 5 is loaded
	When I scroll to the bottom of search result list
	And I press first ellipsis
	Then Page 4 is loaded
	When I scroll to the bottom of search result list
	And I press page 1
	Then Page 1 is loaded
	When I press the last page
	Then The last page is loaded

	Examples:
		| page     |
		| homePage |