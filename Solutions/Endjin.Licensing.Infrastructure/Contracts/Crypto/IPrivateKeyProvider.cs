namespace Endjin.Licensing.Infrastructure.Contracts.Crypto
{
    #region Using Directives

    using System;
    using System.Security.Cryptography;

    #endregion

    /// <summary>
    /// Responsible for creating and recreating the private key.
    /// </summary>
    /// <remarks>
    /// This is used to abstract the method of private key generation so that we have 
    /// a single location for deciding which <seealso cref="AsymmetricAlgorithm"/> to use
    /// for generating the public / private key pairs.
    /// </remarks>
    public interface IPrivateKeyProvider : IDisposable
    {
        IPrivateCryptoKey Create();

        AsymmetricAlgorithm Recreate(IPrivateCryptoKey privateKey);
    }
}