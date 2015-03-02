namespace Endjin.Licensing.Exceptions
{
    #region Using Directives

    using System;
    using System.Runtime.Serialization;

    using Endjin.Licensing.Contracts.Domain;
    using Endjin.Licensing.Domain;

    #endregion

    public class LicenseViolationException : Exception
    {
        public LicenseViolationException()
        {
        }

        public LicenseViolationException(string message) : base(message)
        {
        }

        public LicenseViolationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public LicenseViolationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }

        public LicenseCriteria LicenseCriteria { get; set; }
    }
}