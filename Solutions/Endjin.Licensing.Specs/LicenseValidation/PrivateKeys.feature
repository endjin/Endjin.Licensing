Feature: PrivateKeyProvider
	In order to create licenses 
	As a software provider
	I want create private and public keys

Scenario: Create a Private Key
	Given I have a Private Key Provider
	When I create a private key
	Then I should have a valid private key

Scenario: Recreate a Private Key
	Given I have a Private Key Provider
	And I create a private key 
	And I store it as a string
	When I recreate the private key from the string
	Then it should be the same as the original private key