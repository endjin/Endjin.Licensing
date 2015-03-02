namespace Endjin.Licensing.Exceptions
{
    #region Using Directives

    using System;

    using Endjin.Licensing.Contracts.Domain;

    #endregion

    public sealed class InvalidLicenseException : Exception
    {
        public InvalidLicenseException(IClientLicense clientLicense)
        {
            this.ClientLicense = clientLicense;
        }

        public IClientLicense ClientLicense { get; set; }
    }
}