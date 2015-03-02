namespace Endjin.Licensing.Validation
{
    #region Using Directives

    using System.Security.Cryptography.Xml;
    using System.Xml;

    using Endjin.Licensing.Contracts.Crypto;
    using Endjin.Licensing.Contracts.Domain;
    using Endjin.Licensing.Crypto;

    #endregion

    internal static class LicenseSignatureValidator
    {
        public static bool ValidateSignature(IClientLicense license, ICryptoKey publicKey)
        {
            var namespaceManager = new XmlNamespaceManager(license.Content.NameTable);
            namespaceManager.AddNamespace("sig", "http://www.w3.org/2000/09/xmldsig#");

            var signature = (XmlElement)license.Content.SelectSingleNode("//sig:Signature", namespaceManager);

            if (signature == null)
            {
                return false;
            }

            var signedXml = new SignedXml(license.Content);
            signedXml.LoadXml(signature);

            using (var publicKeyProvider = new RsaPublicKeyProvider())
            {
                return signedXml.CheckSignature(publicKeyProvider.Create(publicKey));
            }
        }
    }
}