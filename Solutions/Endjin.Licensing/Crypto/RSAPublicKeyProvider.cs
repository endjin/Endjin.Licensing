namespace Endjin.Licensing.Crypto
{
    #region Using Directives

    using System.Security.Cryptography;

    using Endjin.Licensing.Contracts.Crypto;

    #endregion

    public sealed class RsaPublicKeyProvider : IPublicKeyProvider
    {
        private RSACryptoServiceProvider cryptoServiceProvider;
        
        public void Dispose()
        {
            this.cryptoServiceProvider.Clear();
            this.cryptoServiceProvider.Dispose();
        }

        public AsymmetricAlgorithm Create(ICryptoKey publicKey)
        {
            this.cryptoServiceProvider = new RSACryptoServiceProvider { PersistKeyInCsp = false };
            this.cryptoServiceProvider.FromXmlString(publicKey.Contents);

            return this.cryptoServiceProvider;
        }
    }
}