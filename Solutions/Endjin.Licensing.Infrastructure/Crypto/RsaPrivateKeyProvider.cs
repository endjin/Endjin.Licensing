namespace Endjin.Licensing.Infrastructure.Crypto
{
    #region Using Directives

    using System.Security.Cryptography;

    using Endjin.Licensing.Infrastructure.Contracts.Crypto;

    #endregion

    public sealed class RsaPrivateKeyProvider : IPrivateKeyProvider
    {
        private RSACryptoServiceProvider cryptoServiceProvider;

        public IPrivateCryptoKey Create()
        {
            using (var rsaCryptoServiceProvider = new RSACryptoServiceProvider(4096))
            {
                try
                {
                    return new PrivateCryptoKey { Contents = rsaCryptoServiceProvider.ToXmlString(true) };
                }
                finally
                {
                    rsaCryptoServiceProvider.PersistKeyInCsp = false;
                    rsaCryptoServiceProvider.Clear();
                }
            }
        }

        public void Dispose()
        {
            this.cryptoServiceProvider.Clear();
            this.cryptoServiceProvider.Dispose();
        }

        public AsymmetricAlgorithm Recreate(IPrivateCryptoKey privateKey)
        {
            this.cryptoServiceProvider = new RSACryptoServiceProvider { PersistKeyInCsp = false };
            this.cryptoServiceProvider.FromXmlString(privateKey.Contents);

            return this.cryptoServiceProvider;
        }
    }
}