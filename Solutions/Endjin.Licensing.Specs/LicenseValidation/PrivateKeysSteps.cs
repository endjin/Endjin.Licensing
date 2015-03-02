namespace Endjin.Licensing.Specs.LicenseValidation
{
    #region Using Directives

    using System.Security.Cryptography;

    using Endjin.Licensing.Infrastructure.Contracts.Crypto;
    using Endjin.Licensing.Infrastructure.Crypto;
    using Endjin.Licensing.Specs.Shared;

    using Should;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class PrivateKeysSteps
    {
        [Given(@"I have a Private Key Provider")]
        public void GivenIHaveAPrivateKeyProvider()
        {
            ScenarioContext.Current.Set(new RsaPrivateKeyProvider(), ContextKey.PrivateKeyProvider);
        }

        [Given(@"I create a private key")]
        [When(@"I create a private key")]
        public void WhenICreateAPrivateKey()
        {
            var privateKeyProvider = ScenarioContext.Current.Get<RsaPrivateKeyProvider>(ContextKey.PrivateKeyProvider);

            var privateKey = privateKeyProvider.Create();

            ScenarioContext.Current.Set(privateKey, ContextKey.PrivateKey);
        }

        [Then(@"I should have a valid private key")]
        public void ThenIShouldHaveAValidPrivateKey()
        {
            var privateKey = ScenarioContext.Current.Get<IPrivateCryptoKey>(ContextKey.PrivateKey);

            privateKey.Contents.ShouldNotBeEmpty();
        }

        [Given(@"I store it as a string")]
        public void GivenICreateAPrivateKeyAndStoreItAsAString()
        {
            var privateKey = ScenarioContext.Current.Get<IPrivateCryptoKey>(ContextKey.PrivateKey);

            ScenarioContext.Current.Set(privateKey.Contents, ContextKey.PrivateKeyAsString);
        }

        [When(@"I recreate the private key from the string")]
        public void WhenIRecreateThePrivateKeyFromTheString()
        {
            var privateKeyAsString = ScenarioContext.Current.Get<string>(ContextKey.PrivateKeyAsString);

            var privateKey = new PrivateCryptoKey { Contents = privateKeyAsString };

            ScenarioContext.Current.Set(privateKey, ContextKey.RecreatedPrivateKey);
        }

        [Then(@"it should be the same as the original private key")]
        public void ThenItShouldBeTheSameAsTheOriginalPrivateKey()
        {
            var privateKey = ScenarioContext.Current.Get<IPrivateCryptoKey>(ContextKey.RecreatedPrivateKey);
            var originalPrivateKey = ScenarioContext.Current.Get<IPrivateCryptoKey>(ContextKey.PrivateKey); 
            
            originalPrivateKey.ExtractPublicKey().ShouldEqual(privateKey.ExtractPublicKey());
        }
    }
}