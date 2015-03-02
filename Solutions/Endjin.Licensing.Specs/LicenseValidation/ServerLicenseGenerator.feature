Feature: Server License Generator
	In order to 
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Generate a 'Subscription' License
	Given they want a 'Subscription' license
	And their license was issued today
	And I generate their license
	When they validate their license
	Then it should be a valid license
	And it should have been issued today
	And it should expire on the last day of this month
