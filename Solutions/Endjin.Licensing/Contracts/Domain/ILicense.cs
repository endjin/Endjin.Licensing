namespace Endjin.Licensing.Contracts.Domain
{
    using Endjin.Licensing.Domain;

    /// <summary>
    /// Represents a license, the data contained within it and 
    /// the public and private keys used to generate it.
    /// </summary>
    public interface ILicense : IClientLicense
    {
        LicenseCriteria Criteria { get; set; }
    }
}