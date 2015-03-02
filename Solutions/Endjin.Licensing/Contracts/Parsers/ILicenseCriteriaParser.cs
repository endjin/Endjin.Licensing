namespace Endjin.Licensing.Contracts.Parsers
{
    #region Using Directives

    using Endjin.Licensing.Contracts.Domain;
    using Endjin.Licensing.Domain;

    #endregion

    public interface ILicenseCriteriaParser
    {
        /// <summary>
        /// Convert from a <see cref="IClientLicense"/> to a <see cref="LicenseCriteria"/> object.
        /// </summary>
        /// <param name="clientLicense">Client License to parse</param>
        /// <returns>License Criteria domain object</returns>
        LicenseCriteria Parse(IClientLicense clientLicense);
    }
}