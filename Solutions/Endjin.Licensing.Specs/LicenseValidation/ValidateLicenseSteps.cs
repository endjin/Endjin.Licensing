namespace Endjin.Licensing.Specs.LicenseValidation
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Endjin.Licensing.Contracts.Crypto;
    using Endjin.Licensing.Contracts.Validation;
    using Endjin.Licensing.Domain;
    using Endjin.Licensing.Exceptions;
    using Endjin.Licensing.Extensions;
    using Endjin.Licensing.Infrastructure.Contracts.Crypto;
    using Endjin.Licensing.Infrastructure.Contracts.Generators;
    using Endjin.Licensing.Infrastructure.Crypto;
    using Endjin.Licensing.Infrastructure.Generators;
    using Endjin.Licensing.Specs.Extensions;
    using Endjin.Licensing.Specs.Shared;
    using Endjin.Licensing.Validation;
    using Endjin.Licensing.Validation.Rules;

    using Should;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class ValidateLicenseSteps
    {
        private readonly IServerLicenseGenerator serverLicenseGenerator;
        private readonly IPrivateKeyProvider privateKeyProvider;
        private readonly ILicenseValidator licenseValidator;

        public ValidateLicenseSteps()
        {
            this.serverLicenseGenerator = new ServerLicenseGenerator();
            this.privateKeyProvider = new RsaPrivateKeyProvider();
            this.licenseValidator = new LicenseValidator();
        }

        [Given(@"they want a '(.*)' license")]
        public void GivenTheyWantALicense(string licenseType)
        {
            ScenarioContext.Current.Set(licenseType, ContextKey.LicenseType);
        }

        [Given(@"their license was issued today")]
        public void GivenTheirLicenseWasIssuedToday()
        {
            ScenarioContext.Current.Set(DateTimeOffset.UtcNow, ContextKey.LicenseIssueDate);
        }

        [Given(@"their license was issued a month ago")]
        public void GivenTheirLicenseWasIssuedAMonthAgo()
        {
            ScenarioContext.Current.Set(DateTimeOffset.UtcNow.AddMonths(-1), ContextKey.LicenseIssueDate);
        }

        [Given(@"I generate their license")]
        public void GivenIGenerateTheirLicense()
        {
            var subscriptionStartDate = ScenarioContext.Current.Get<DateTimeOffset>(ContextKey.LicenseIssueDate);
            var licenseType = ScenarioContext.Current.Get<string>(ContextKey.LicenseType);

            Dictionary<string, string> metadata;

            if (!ScenarioContext.Current.TryGetValue(ContextKey.LicenseMetadata, out metadata))
            {
                metadata = new Dictionary<string, string>();
            }

            var licenseCriteria = new LicenseCriteria
            {
                ExpirationDate = subscriptionStartDate.LastDayOfMonth().EndOfDay(),
                IssueDate = subscriptionStartDate,
                Id = Guid.NewGuid(),
                MetaData = metadata,
                Type = licenseType
            };

            var privateKey = this.privateKeyProvider.Create();
            var serverLicense = this.serverLicenseGenerator.Generate(privateKey, licenseCriteria);
            var clientLicense = serverLicense.ToClientLicense();

            ScenarioContext.Current.Set(serverLicense, ContextKey.ServerLicense);
            ScenarioContext.Current.Set(privateKey.ExtractPublicKey(), ContextKey.PublicKey);
            ScenarioContext.Current.Set(clientLicense, ContextKey.ClientLicense);
        }

        [Given(@"the user tampers with the expiration date of the license")]
        public void GivenTheUserTampersWithTheExpirationDateOfTheLicense()
        {
            var clientLicense = ScenarioContext.Current.Get<ClientLicense>(ContextKey.ClientLicense);

            clientLicense.Content = clientLicense.Tamper(LicenseElements.ExpirationDate, DateTimeOffset.MaxValue.ToString("o"));
        }

        [Given(@"the user tampers with the issue date of the license")]
        public void GivenTheUserTampersWithTheIssueDateOfTheLicense()
        {
            var clientLicense = ScenarioContext.Current.Get<ClientLicense>(ContextKey.ClientLicense);

            clientLicense.Content = clientLicense.Tamper(LicenseElements.IssueDate, DateTimeOffset.MinValue.ToString("o"));
        }

        [Given(@"the user tampers with the type of the license")]
        public void GivenTheUserTampersWithTheTypeOfTheLicense()
        {
            var clientLicense = ScenarioContext.Current.Get<ClientLicense>(ContextKey.ClientLicense);

            clientLicense.Content = clientLicense.Tamper(LicenseElements.Type, "Enterprise");
        }

        [Given(@"the user tampers with the ID of the license")]
        public void GivenTheUserTampersWithTheIDOfTheLicense()
        {
            var clientLicense = ScenarioContext.Current.Get<ClientLicense>(ContextKey.ClientLicense);

            clientLicense.Content = clientLicense.Tamper(LicenseElements.Id, Guid.NewGuid().ToString());
        }

        [Given(@"the user is issued with a valid '(.*)' license")]
        public void GivenTheUserIsIssuedWithAValidLicense(string licenseType)
        {
            this.GivenTheyWantALicense(licenseType);
            this.GivenTheirLicenseWasIssuedToday();
            this.GivenIGenerateTheirLicense();
        }

        [Given(@"their username is '(.*)'")]
        public void GivenTheirUsernameIs(string username)
        {
            Dictionary<string, string> metadata;

            if (!ScenarioContext.Current.TryGetValue(ContextKey.LicenseMetadata, out metadata))
            {
                metadata = new Dictionary<string, string>();
            }

            metadata.Add("Username", username);

            ScenarioContext.Current.Set(metadata, ContextKey.LicenseMetadata);
        }

        [Given(@"their email address is '(.*)'")]
        public void GivenTheirEmailAddressIs(string emailAddress)
        {
            Dictionary<string, string> metadata;

            if (!ScenarioContext.Current.TryGetValue(ContextKey.LicenseMetadata, out metadata))
            {
                metadata = new Dictionary<string, string>();
            }

            metadata.Add("EmailAddress", emailAddress);

            ScenarioContext.Current.Set(metadata, ContextKey.LicenseMetadata);
        }

        [Given(@"the user tampers with the username of the license and sets it to ""(.*)""")]
        public void GivenTheUserTampersWithTheUsernameOfTheLicenseAndSetsItTo(string newValue)
        {
            var clientLicense = ScenarioContext.Current.Get<ClientLicense>(ContextKey.ClientLicense);

            clientLicense.Content = clientLicense.Tamper("Username", newValue);
        }

        [When(@"they validate their license")]
        public void WhenTheyValidateTheirLicense()
        {
            var clientLicense = ScenarioContext.Current.Get<ClientLicense>(ContextKey.ClientLicense);
            var publicKey =  ScenarioContext.Current.Get<ICryptoKey>(ContextKey.PublicKey);

            try
            {
                this.licenseValidator.Validate(clientLicense, publicKey, new List<ILicenseValidationRule> { new LicenseHasNotExpiredRule() });
            }
            catch (Exception ex)
            {
                ScenarioContext.Current.Set(ex.GetBaseException(), ContextKey.LicenseValidatorException);
            }

            ScenarioContext.Current.Set(this.licenseValidator.LicenseCriteria, ContextKey.ClientLicenseCriteria);
        }

        [Then(@"it should be a valid license")]
        public void ThenItShouldBeAValidLicense()
        {
            Exception exception;

            ScenarioContext.Current.TryGetValue(ContextKey.LicenseValidatorException, out exception);

            exception.ShouldBeNull();
        }

        [Then(@"it should have been issued today")]
        public void ThenItShouldHaveBeenIssuedToday()
        {
            var clientLicenseCriteria = ScenarioContext.Current.Get<LicenseCriteria>(ContextKey.ClientLicenseCriteria);

            clientLicenseCriteria.IssueDate.Date.ShouldEqual(DateTimeOffset.UtcNow.Date);
        }

        [Then(@"it should have been issued last month")]
        public void ThenItShouldHaveBeenIssuedLastMonth()
        {
            var clientLicenseCriteria = ScenarioContext.Current.Get<LicenseCriteria>(ContextKey.ClientLicenseCriteria);

            clientLicenseCriteria.IssueDate.Date.ShouldEqual(DateTimeOffset.UtcNow.AddMonths(-1).Date);
        }


        [Then(@"it should expire on the last day of this month")]
        public void ThenItShouldExpireOnTheLastDayOfThisMonth()
        {
            var clientLicenseCriteria = ScenarioContext.Current.Get<LicenseCriteria>(ContextKey.ClientLicenseCriteria);

            clientLicenseCriteria.ExpirationDate.ShouldEqual(DateTimeOffset.UtcNow.LastDayOfMonth().EndOfDay());
        }

        [Then(@"it should be an invalid license")]
        public void ThenItShouldBeAnInvalidLicense()
        {
            var exception = ScenarioContext.Current.Get<Exception>(ContextKey.LicenseValidatorException);

            exception.ShouldNotBeNull();
            exception.ShouldBeType<InvalidLicenseException>();
        }

        [Then(@"it should be an expired license")]
        public void ThenItShouldBeAnExpiredLicense()
        {
            var exception = ScenarioContext.Current.Get<Exception>(ContextKey.LicenseValidatorException);

            exception.ShouldNotBeNull();
            exception.ShouldBeType<LicenseExpiredException>();
        }

        [Then(@"the license should have expired on the last day of last month")]
        public void ThenTheLicenseShouldHaveExpiredOnTheLastDayOfLastMonth()
        {
            var exception = ScenarioContext.Current.Get<Exception>(ContextKey.LicenseValidatorException);

            var licenseExpiredException = exception as LicenseExpiredException;

            licenseExpiredException.ShouldNotBeNull();
            licenseExpiredException.LicenseCriteria.ExpirationDate.ShouldEqual(DateTimeOffset.UtcNow.AddMonths(-1).LastDayOfMonth().EndOfDay());
        }
    }
}