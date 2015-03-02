namespace Endjin.Licensing.Demo.ClientApp.Validation.Rules
{
    #region Using Directives

    using System;

    using Endjin.Licensing.Contracts.Validation;
    using Endjin.Licensing.Demo.ClientApp.Exceptions;
    using Endjin.Licensing.Domain;

    #endregion

    public class ValidNumberOfCoresLicenseRule : ILicenseValidationRule
    {
        public void Validate(LicenseCriteria licenseCriteria)
        {
            int licensedCores = 0;

            if (licenseCriteria.MetaData.ContainsKey("LicensedCores"))
            {
                licensedCores = Convert.ToInt32(licenseCriteria.MetaData["LicensedCores"]);
            }

            if (Environment.ProcessorCount > licensedCores)
            {
                string message = string.Format("This license is only valid for {0} cores.", licensedCores);

                throw new LicensedCoresExceededException(message, Environment.ProcessorCount)
                {
                    LicenseCriteria = licenseCriteria
                };
            }
        }
    }
}