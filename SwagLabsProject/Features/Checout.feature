Feature: Checkout Page Functionality

Background:
	Given I am on the login page
	Given I log in with valid credentials
	Then I am on the inventory page
	When I add a product to the cart
	And  I click to the cart icon
	Then I am on the cart page
	When I click on the checkout button
	Then I should be on the checkout page

Scenario: Checkout page first name field
	When I enter "" into the first name field
	And the last name field should contain "LastNameTest"
	And I enter postal code "12345"
	When I click continue button
	Then I should see first name field required warning

Scenario: Checkout page last name field
	When I enter "Test" into the first name field
	And the last name field should contain ""
	And I enter postal code "12345"
	When I click continue button
	Then I should see last name field required warning

Scenario: Checkout page zip code field
	When I enter "Test" into the first name field
	And the last name field should contain "LastNameTest"
	And I enter postal code ""
	When I click continue button
	Then I should see zip code field required warning

Scenario: Checkout page fill all fields
	When I enter "Test" into the first name field
	And the last name field should contain "LastNameTest"
	And I enter postal code "12345"
	When I click continue button
	Then I am on the checout step two page

Scenario: Checkout step two page price field
	When I enter "Test" into the first name field
	And the last name field should contain "LastNameTest"
	And I enter postal code "12345"
	When I click continue button
	Then I am on the checout step two page
	And I should see the price field fill


Scenario: Checkout step two page tax field
	When I enter "Test" into the first name field
	And the last name field should contain "LastNameTest"
	And I enter postal code "12345"
	When I click continue button
	Then I am on the checout step two page
	And I should see the tax field fill

Scenario: Checkout step two page total field
	When I enter "Test" into the first name field
	And the last name field should contain "LastNameTest"
	And I enter postal code "12345"
	When I click continue button
	Then I am on the checout step two page
	And I should see the total field is correct

Scenario: Checkout complete
	When I enter "Test" into the first name field
	And the last name field should contain "LastNameTest"
	And I enter postal code "12345"
	When I click continue button
	Then I am on the checout step two page
	And I should see the total field is correct
	Then I click the finish button
	Then I should be on the checkout complete page
	And I should see the thank you message
	Then I click back to home button
	Then I am on the inventory page