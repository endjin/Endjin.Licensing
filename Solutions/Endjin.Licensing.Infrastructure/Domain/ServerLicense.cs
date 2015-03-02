namespace Endjin.Licensing.Infrastructure.Domain
{
    #region Using Directives

    using System.Xml;

    using Endjin.Licensing.Contracts.Crypto;
    using Endjin.Licensing.Domain;
    using Endjin.Licensing.Infrastructure.Contracts.Domain;

    #endregion

    public sealed class ServerLicense : License, IServerLicense
    {
        public ICryptoKey PrivateKey { get; set; }

        public ClientLicense ToClientLicense()
        {
            var licenseDocument = new XmlDocument();

            var licenseElement = licenseDocument.CreateElement("License");
            licenseElement.InnerXml = this.Content.FirstChild.InnerXml;
            licenseDocument.AppendChild(licenseElement);

            return new ClientLicense { Content = licenseDocument };
        }
    }
}