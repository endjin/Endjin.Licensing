namespace Endjin.Licensing.Contracts.Crypto
{
    #region Using Directives

    using System;
    using System.Security.Cryptography;

    #endregion

    public interface IPublicKeyProvider : IDisposable
    {
        AsymmetricAlgorithm Create(ICryptoKey publicKey);
    }
}