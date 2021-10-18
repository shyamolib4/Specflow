Feature: Amazon
	
@mytag
Scenario: Add to cart
	Given the user Shyamoli is logged in
	And the search term is Television
	When Add to cart button is clicked
	Then the selected item should be added to cart