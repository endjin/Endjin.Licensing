namespace Endjin.Licensing.Infrastructure.Contracts.Generators
{
    #region Using Directives

    using Endjin.Licensing.Contracts.Domain;
    using Endjin.Licensing.Domain;
    using Endjin.Licensing.Infrastructure.Contracts.Crypto;
    using Endjin.Licensing.Infrastructure.Contracts.Domain;

    #endregion

    /// <summary>
    /// Responsible for generating the <seealso cref="ILicense"/> based on the <seealso cref="IPrivateCryptoKey"/> and 
    /// the <seealso cref="LicenseCriteria"/> provided.
    /// </summary>
    public interface IServerLicenseGenerator
    {
        IServerLicense Generate(IPrivateCryptoKey privateKey, LicenseCriteria licenseCriteria);
    }
}