Feature: Login Page Functionality
  As a user, I want to be able to log in to the application.

Scenario: Successful login with standard user
	Given I am on the login page
	When I enter the username "standard_user"
	And I enter the password "secret_sauce"
	And I click on the login button
	Then I should be redirected to the inventory page

Scenario: Successful login with standard user and logout
	Given I am on the login page
	When I enter the username "standard_user"
	And I enter the password "secret_sauce"
	And I click on the login button
	Then I should be redirected to the inventory page
	When I click on the menu button
	And I click on the logout button
	Then I should be redirected to the login page

Scenario: Unsuccessful login with locked out user
	Given I am on the login page
	When I enter the username "locked_out_user"
	And I enter the password "secret_sauce"
	And I click on the login button
	Then I should see an error message indicating a locked account

Scenario: Unsuccessful login with problem user
	Given I am on the login page
	When I enter the username "problem_user"
	And I enter the password "incorrect_password"
	And I click on the login button
	Then I should see an error message indicating an incorrect password

Scenario: Unsuccessful login with performance glitch user
	Given I am on the login page
	When I enter the username "performance_glitch_user"
	And I enter the password "secret_sauce"
	And I click on the login button multiple times
	Then I should see an error message indicating a performance issue

Scenario: Unsuccessful login with invalid username
	Given I am on the login page
	When I enter an invalid username "invalid_user"
	And I enter the password "secret_sauce"
	And I click on the login button
	Then I should see an error message indicating an invalid username

Scenario: Unsuccessful login with blank credentials
	Given I am on the login page
	When I leave both username and password fields empty
	And I click on the login button
	Then I should see an error message indicating missing credentials

Scenario: Unsuccessful login with blank password
	Given I am on the login page
	When I enter the username "standard_user"
	And I leave the password field empty
	And I click on the login button
	Then I should see an error message indicating a missing password

Scenario: Unsuccessful login with blank username
	Given I am on the login page
	When I leave the username field empty
	And I enter the password "secret_sauce"
	And I click on the login button
	Then I should see an error message indicating a missing username
