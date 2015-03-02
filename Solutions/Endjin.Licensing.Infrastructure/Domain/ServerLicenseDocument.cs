namespace Endjin.Licensing.Infrastructure.Domain
{
    using Endjin.Licensing.Domain;

    public sealed class ServerLicenseDocument
    {
        public string PrivateKey { get; set; }

        public LicenseCriteria Criteria { get; set; }
    }
}