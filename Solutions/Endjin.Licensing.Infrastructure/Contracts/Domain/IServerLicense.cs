namespace Endjin.Licensing.Infrastructure.Contracts.Domain
{
    #region Using Directives

    using Endjin.Licensing.Contracts.Crypto;
    using Endjin.Licensing.Contracts.Domain;
    using Endjin.Licensing.Domain;

    #endregion

    public interface IServerLicense : ILicense
    {
        ICryptoKey PrivateKey { get; set; }

        ClientLicense ToClientLicense();
    }
}