@container_feature_setup @container_feature_teardown
Feature: Validate License
	In order to allow users to only use my software with a valid license
	As a software provider
	I want to be able to validate the license based on its criteria

Scenario: Validate a 'Subscription' License that was issued today
	Given they want a 'Subscription' license
	And their license was issued today
	And I generate their license
	When they validate their license
	Then it should be a valid license
	And it should have been issued today
	And it should expire on the last day of this month

Scenario: Validate a 'Subscription' License that expired last month
	Given they want a 'Subscription' license
	And their license was issued a month ago
	And I generate their license
	When they validate their license
	Then it should be an expired license
	And it should have been issued last month
	And the license should have expired on the last day of last month

Scenario: Tamper with the expiration date of a license
	Given the user is issued with a valid 'Subscription' license
	And the user tampers with the expiration date of the license
	When they validate their license
	Then it should be an invalid license

Scenario: Tamper with the issue date of a license
	Given the user is issued with a valid 'Subscription' license
	And the user tampers with the issue date of the license
	When they validate their license
	Then it should be an invalid license

Scenario: Tamper with the type of a license
	Given the user is issued with a valid 'Subscription' license
	And the user tampers with the type of the license
	When they validate their license
	Then it should be an invalid license

Scenario: Tamper with the ID of a license
	Given the user is issued with a valid 'Subscription' license
	And the user tampers with the ID of the license
	When they validate their license
	Then it should be an invalid license

Scenario: Tamper with the metadata of a license
	Given they want a 'Subscription' license
	And their license was issued a month ago
	And their username is 'Kurt.Vonnegut'
	And their email address is 'Kurt.Vonnegut@example.com'
	And I generate their license
	And the user tampers with the username of the license and sets it to "Thomas Pynchon"
	When they validate their license
	Then it should be an invalid license
