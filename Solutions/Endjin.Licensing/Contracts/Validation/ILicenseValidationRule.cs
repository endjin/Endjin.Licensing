namespace Endjin.Licensing.Contracts.Validation
{
    #region Using Directives

    using Endjin.Licensing.Contracts.Domain;
    using Endjin.Licensing.Domain;
    using Endjin.Licensing.Exceptions;

    #endregion

    /// <summary>
    /// Extensiblity point for writing your own License Validation Rules
    /// </summary>
    /// <remarks>
    /// To implement your own license validation rule, implmement this interface, throw an exception (based on <see cref="LicenseViolationException"/>)
    /// with details of how the license validation rules have been violated. Pass the rule into the <see cref="ILicenseValidator.Validate"/> method to evaluate.
    /// </remarks>
    /// <exception cref="InvalidLicenseException">License was invalid</exception>
    /// <exception cref="LicenseExpiredException">License has expired</exception>
    /// <exception cref="LicenseViolationException">Custom license violation exception</exception>
    public interface ILicenseValidationRule
    {
        void Validate(LicenseCriteria licenseCriteria);
    }
}