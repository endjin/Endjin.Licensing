namespace Endjin.Licensing.Validation
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Endjin.Licensing.Contracts.Crypto;
    using Endjin.Licensing.Contracts.Domain;
    using Endjin.Licensing.Contracts.Parsers;
    using Endjin.Licensing.Contracts.Validation;
    using Endjin.Licensing.Domain;
    using Endjin.Licensing.Exceptions;
    using Endjin.Licensing.Extensions;
    using Endjin.Licensing.Parsers;

    #endregion

    public sealed class LicenseValidator : ILicenseValidator
    {
        private readonly ILicenseCriteriaParser licenseCriteriaParser;

        public LicenseValidator()
        {
            this.licenseCriteriaParser = new LicenseCriteriaParser();
        }

        public LicenseCriteria LicenseCriteria { get; set; }

        /// <summary>
        /// Validates the supplied client license against the private key and the set of validation rules. Violations are exposed via exceptions or aggregate exceptions.
        /// </summary>
        /// <param name="clientLicense">License to validate</param>
        /// <param name="publicKey">Public Key to validate the license with</param>
        /// <param name="validationRules">List of validation rules to examine</param>
        /// <exception cref="InvalidLicenseException">Indicates that the license is invalid / corrupt / empty.</exception>
        /// <exception cref="LicenseViolationException">Indicates that a license validation rule has been violated.</exception>
        /// <exception cref="AggregateException">Indicates that one or more license validation rules have been violated.</exception>
        public void Validate(IClientLicense clientLicense, ICryptoKey publicKey, IEnumerable<ILicenseValidationRule> validationRules)
        {
            if (!LicenseSignatureValidator.ValidateSignature(clientLicense, publicKey))
            {
                throw new InvalidLicenseException(clientLicense);
            }

            this.LicenseCriteria = this.licenseCriteriaParser.Parse(clientLicense);

            validationRules.ForEachFailEnd(x => x.Validate(this.LicenseCriteria));
        }
    }
}