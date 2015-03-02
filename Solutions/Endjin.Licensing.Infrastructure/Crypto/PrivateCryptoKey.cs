namespace Endjin.Licensing.Infrastructure.Crypto
{
    #region Using Directives

    using Endjin.Licensing.Contracts.Crypto;
    using Endjin.Licensing.Domain;
    using Endjin.Licensing.Infrastructure.Contracts.Crypto;

    #endregion

    public sealed class PrivateCryptoKey : IPrivateCryptoKey
    {
        public string Contents { get; set; }

        public ICryptoKey ExtractPublicKey()
        {
            using (var privateKeyProvider = new RsaPrivateKeyProvider())
            {
                var cryptoProvider = privateKeyProvider.Recreate(this);

                return new PublicCryptoKey { Contents = cryptoProvider.ToXmlString(false) };
            }
        }
    }
}