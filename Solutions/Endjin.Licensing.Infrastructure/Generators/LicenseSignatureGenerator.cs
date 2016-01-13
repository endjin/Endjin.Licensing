namespace Endjin.Licensing.Infrastructure.Generators
{
    #region Using Directives

    using System.Security.Cryptography.Xml;
    using System.Xml;

    using Endjin.Licensing.Infrastructure.Contracts.Crypto;
    using Endjin.Licensing.Infrastructure.Crypto;

    #endregion

    internal static class LicenseSignatureGenerator
    {
        public static XmlElement GenerateSignature(XmlDocument licenseDocument, IPrivateCryptoKey privateKey)
        {
            using (var privateKeyProvider = new RsaPrivateKeyProvider())
            {
                var reference = new Reference { Uri = string.Empty };
                reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
                reference.AddTransform(new XmlDsigExcC14NTransform());
                reference.DigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";

                var signedXml = new SignedXml(licenseDocument) { SigningKey = privateKeyProvider.Recreate(privateKey) };

                signedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
                signedXml.AddReference(reference);
                signedXml.ComputeSignature();

                return signedXml.GetXml();
            }
        }
    }
}