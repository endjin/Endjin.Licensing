namespace Endjin.Licensing.Demo.ClientApp.Exceptions
{
    #region Using Directives

    using Endjin.Licensing.Exceptions;

    #endregion

    public class LicensedCoresExceededException : LicenseViolationException
    {
        public LicensedCoresExceededException(int actualCoreCount)
        {
            this.ActualCoreCount = actualCoreCount;
        }

        public LicensedCoresExceededException(string message, int actualCoreCount) : base(message)
        {
            this.ActualCoreCount = actualCoreCount;
        }

        public int ActualCoreCount { get; set; }
    }
}