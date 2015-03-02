namespace Endjin.Licensing.Contracts.Validation
{
    #region Using Directives

    using System.Collections.Generic;

    using Endjin.Licensing.Contracts.Crypto;
    using Endjin.Licensing.Contracts.Domain;
    using Endjin.Licensing.Domain;

    #endregion

    public interface ILicenseValidator
    {
        /// <summary>
        /// Validate the collection of <see cref="ILicenseValidationRule"/> against the supplied <see cref="IClientLicense"/> and ensure license has
        /// not been tampered with using the <see cref="ICryptoKey">Public Key</see>.
        /// </summary>
        /// <param name="clientLicense"><see cref="IClientLicense">Client License</see> to validate</param>
        /// <param name="publicKey"><see cref="ICryptoKey">Public Key</see> to determine whether the license has been tampered with</param>
        /// <param name="validationRules">Collection of <see cref="ILicenseValidationRule">Validation Rules</see> to evaulate against the <see cref="IClientLicense">Client License</see></param>
        void Validate(IClientLicense clientLicense, ICryptoKey publicKey, IEnumerable<ILicenseValidationRule> validationRules);

        /// <summary>
        /// The <see cref="LicenseCriteria">License Criteria</see> extracted from the <see cref="IClientLicense">Client License</see>
        /// </summary>
        /// <remarks>Should be null, if the license is invalid</remarks>
        LicenseCriteria LicenseCriteria { get; }
    }
}