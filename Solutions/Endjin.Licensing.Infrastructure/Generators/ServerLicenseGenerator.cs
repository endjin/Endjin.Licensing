namespace Endjin.Licensing.Infrastructure.Generators
{
    #region Using Directives

    using System.Xml;

    using Endjin.Licensing.Contracts.Domain;
    using Endjin.Licensing.Domain;
    using Endjin.Licensing.Infrastructure.Contracts.Crypto;
    using Endjin.Licensing.Infrastructure.Contracts.Domain;
    using Endjin.Licensing.Infrastructure.Contracts.Generators;
    using Endjin.Licensing.Infrastructure.Domain;

    #endregion

    public sealed class ServerLicenseGenerator : IServerLicenseGenerator
    {
        public IServerLicense Generate(IPrivateCryptoKey privateKey, LicenseCriteria licenseCriteria)
        {
            var licenseDocument = this.CreateLicenseDocument(licenseCriteria);

            var signature = LicenseSignatureGenerator.GenerateSignature(licenseDocument, privateKey);
            licenseDocument.FirstChild.AppendChild(licenseDocument.ImportNode(signature, true));

            return new ServerLicense
            {
                Content = licenseDocument, 
                Criteria = licenseCriteria,
                PrivateKey = privateKey,
                PublicKey = privateKey.ExtractPublicKey()
            };
        }

        private XmlDocument CreateLicenseDocument(LicenseCriteria licenseCriteria)
        {
            var licenseDocument = new XmlDocument();

            var licenseElement = licenseDocument.CreateElement(LicenseElements.License);
            licenseDocument.AppendChild(licenseElement);

            var id = licenseDocument.CreateElement(LicenseElements.Id);
            id.InnerText = licenseCriteria.Id.ToString();
            licenseElement.AppendChild(id);

            var expirationDate = licenseDocument.CreateElement(LicenseElements.ExpirationDate);
            expirationDate.InnerText = licenseCriteria.ExpirationDate.ToString("o");
            licenseElement.AppendChild(expirationDate);

            var issueDate = licenseDocument.CreateElement(LicenseElements.IssueDate);
            issueDate.InnerText = licenseCriteria.IssueDate.ToString("o");
            licenseElement.AppendChild(issueDate);

            var type = licenseDocument.CreateElement(LicenseElements.Type);
            type.InnerText = licenseCriteria.Type;
            licenseElement.AppendChild(type);

            foreach (var metaData in licenseCriteria.MetaData)
            {
                var element = licenseDocument.CreateElement(metaData.Key);
                element.InnerText = metaData.Value;
                licenseElement.AppendChild(element);
            }

            return licenseDocument;
        }
    }
}