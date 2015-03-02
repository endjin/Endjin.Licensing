namespace Endjin.Licensing.Domain
{
    #region Using Directives

    using System.Xml;

    using Endjin.Licensing.Contracts.Crypto;
    using Endjin.Licensing.Contracts.Domain;

    #endregion

    public class License : ILicense
    {
        public XmlDocument Content { get; set; }

        public LicenseCriteria Criteria { get; set; }

        public ICryptoKey PublicKey { get; set; }
    }
}