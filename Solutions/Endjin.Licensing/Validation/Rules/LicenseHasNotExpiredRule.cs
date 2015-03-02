namespace Endjin.Licensing.Validation.Rules
{
    #region Using Directives

    using System;

    using Endjin.Licensing.Contracts.Validation;
    using Endjin.Licensing.Domain;
    using Endjin.Licensing.Exceptions;

    #endregion

    public sealed class LicenseHasNotExpiredRule : ILicenseValidationRule
    {
        public void Validate(LicenseCriteria licenseCriteria)
        {
            // TODO: Get from a time server.
            if (DateTimeOffset.UtcNow > licenseCriteria.ExpirationDate)
            {
                string message = string.Format("License expired on {0}", licenseCriteria.ExpirationDate.ToString("O"));

                throw new LicenseExpiredException(message, DateTimeOffset.UtcNow)
                {
                    LicenseCriteria = licenseCriteria
                };
            }
        }
    }
}