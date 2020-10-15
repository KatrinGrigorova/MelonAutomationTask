Feature: PaginationCheck
	In order to review the search result list
	As a user
	I want the correct pages to be loaded

Scenario: Correct page is loaded after search
	Given I am on the Home Page
	When I enter 'book' to the Search bar
	And I press the Search button
	And I scroll to the bottom of search result list
	And I press Accept cookies button
	Then First page is loaded

Scenario: Correct page is loaded after click on second page
	Given I am on the Home Page
	When I enter 'book' to the Search bar
	And I press the Search button
	And I scroll to the bottom of search result list
	And I press Accept cookies button
	And I press second page
	Then Second page is loaded

Scenario: Correct page is loaded after click on third page
	Given I am on the Home Page
	When I enter 'book' to the Search bar
	And I press the Search button
	And I scroll to the bottom of search result list
	And I press Accept cookies button
	And I press second page
	And I scroll to the bottom of search result list
	And I press third page
	Then Third page is loaded

Scenario: Correct page is loaded after click on forth page
	Given I am on the Home Page
	When I enter 'book' to the Search bar
	And I press the Search button
	And I scroll to the bottom of search result list
	And I press Accept cookies button
	And I press second page
	And I scroll to the bottom of search result list
	And I press third page
	And I scroll to the bottom of search result list
	And I press forth page
	Then Forth page is loaded

Scenario: Correct page is loaded after click on next page
	Given I am on the Home Page
	When I enter 'book' to the Search bar
	And I press the Search button
	And I scroll to the bottom of search result list
	And I press Accept cookies button
	And I press Next page
	Then Second page is loaded

Scenario: Correct page is loaded after click on previous page
	Given I am on the Home Page
	When I enter 'book' to the Search bar
	And I press the Search button
	And I scroll to the bottom of search result list
	And I press Accept cookies button
	And I press second page
	And I scroll to the bottom of search result list
	And I press third page
	And I scroll to the bottom of search result list
	And I press previous page
	Then Second page is loaded

Scenario: Correct page is loaded after click on the second ellipsis
	Given I am on the Home Page
	When I enter 'book' to the Search bar
	And I press the Search button
	And I scroll to the bottom of search result list
	And I press Accept cookies button
	And I press second page
	And I scroll to the bottom of search result list
	And I press third page
	And I scroll to the bottom of search result list
	And I press forth page
	And I scroll to the bottom of search result list
	And I press second ellipsis
	Then Fifth page is loaded

Scenario: Correct page is loaded after click on the first ellipsis
	Given I am on the Home Page
	When I enter 'book' to the Search bar
	And I press the Search button
	And I scroll to the bottom of search result list
	And I press Accept cookies button
	And I press second page
	And I scroll to the bottom of search result list
	And I press third page
	And I scroll to the bottom of search result list
	And I press forth page
	And I scroll to the bottom of search result list
	And I press first ellipsis
	Then Third page is loaded

Scenario: Correct page is loaded after click on first page
	Given I am on the Home Page
	When I enter 'book' to the Search bar
	And I press the Search button
	And I scroll to the bottom of search result list
	And I press Accept cookies button
	And I press second page
	And I scroll to the bottom of search result list
	And I press third page
	And I scroll to the bottom of search result list
	And I press first page
	Then First page is loaded

Scenario: Correct page is loaded after click on the last page
	Given I am on the Home Page
	When I enter 'book' to the Search bar
	And I press the Search button
	And I scroll to the bottom of search result list
	And I press Accept cookies button
	And I press the last page	
	Then The last page is loaded 