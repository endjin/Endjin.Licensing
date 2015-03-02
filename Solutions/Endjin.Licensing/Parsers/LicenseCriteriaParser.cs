namespace Endjin.Licensing.Parsers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using Endjin.Licensing.Contracts.Domain;
    using Endjin.Licensing.Contracts.Parsers;
    using Endjin.Licensing.Domain;

    #endregion

    public sealed class LicenseCriteriaParser : ILicenseCriteriaParser
    {
        public LicenseCriteria Parse(IClientLicense clientLicense)
        {
            var license = XDocument.Parse(clientLicense.Content.OuterXml).Root;

            if (license == null)
            {
                throw new InvalidDataException("Document is invalid. A root node was expected");
            }

            var licenseDetails = license.Elements()
                                        .Where(element => element.Name.LocalName != LicenseElements.Signature)
                                        .Select(element => new KeyValuePair<string, string>(element.Name.LocalName, element.Value))
                                        .ToDictionary(pair => pair.Key, pair => pair.Value);

            var licenseCriteria = new LicenseCriteria
            {
                ExpirationDate = DateTimeOffset.Parse(licenseDetails[LicenseElements.ExpirationDate]),
                IssueDate = DateTimeOffset.Parse(licenseDetails[LicenseElements.IssueDate]),
                Id = Guid.Parse(licenseDetails[LicenseElements.Id]),
                Type = licenseDetails[LicenseElements.Type]
            };
            
            licenseDetails.Remove(LicenseElements.ExpirationDate);
            licenseDetails.Remove(LicenseElements.Id);
            licenseDetails.Remove(LicenseElements.IssueDate);
            licenseDetails.Remove(LicenseElements.Type);

            licenseCriteria.MetaData = licenseDetails;

            return licenseCriteria;
        }
    }
}