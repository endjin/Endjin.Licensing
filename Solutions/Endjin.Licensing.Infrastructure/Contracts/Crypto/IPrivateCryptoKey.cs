namespace Endjin.Licensing.Infrastructure.Contracts.Crypto
{
    #region Using Directives

    using Endjin.Licensing.Contracts.Crypto;

    #endregion

    /// <summary>
    /// Represents the private key used to sign the License XML document
    /// </summary>
    public interface IPrivateCryptoKey : ICryptoKey
    {
        ICryptoKey ExtractPublicKey();
    }
}