Feature: Inventory Page Functionality
  E-Commerce Website Functionality
  As a user, I want to add products to the cart and interact with the filters and cart.

Background:
	Given I am on the login page
	And I log in with valid credentials

Scenario: Add product to cart
	Given I am on the inventory page
	When I add a product to the cart
	Then the product should be added to the cart

Scenario: Apply filters
	Given I am on the inventory page
	When I open the filter menu
	And I select "Name (A to Z)" filter
	Then the products should be sorted by name in ascending order

	When I open the filter menu
	And I select "Name (Z to A)" filter
	Then the products should be sorted by name in descending order

	When I open the filter menu
	And I select "Price (low to high)" filter
	Then the products should be sorted by price in ascending order

	When I open the filter menu
	And I select "Price (high to low)" filter
	Then the products should be sorted by price in descending order

Scenario: Burger menu check about page
	Given I am on the inventory page
	When I open the burger menu
	And I select the about page
	Then I should be on the about page

Scenario: Burger menu check App state
	Given I am on the inventory page
	When I add a product to the cart
	Then the product should be added to the cart
	When I open the burger menu
	And I select the reset app state
	Then the cart should be empty