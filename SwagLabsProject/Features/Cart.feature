Feature: Cart Page Functionality
  E-Commerce Cart Functionality
  

Background:
	Given I am on the login page
	And I log in with valid credentials
	Given I am on the inventory page
	When I add a product to the cart
	Then the product should be added to the cart
	When I click cart icon
	Given I am on the cart page
	Then I should see the total number of items in the cart

Scenario: Check cart on item
	When I remove an item from the cart
	Then the item should be removed from the cart

Scenario: Check cart on item and back inventory page
	When I back to the inventory page
	Given I am on the inventory page

Scenario: Checkout cart
	When I click on the checkout button
	Then I should be on the checkout page