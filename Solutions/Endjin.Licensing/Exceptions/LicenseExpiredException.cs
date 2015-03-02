namespace Endjin.Licensing.Exceptions
{
    using System;

    public sealed class LicenseExpiredException : LicenseViolationException
    {
        public LicenseExpiredException(DateTimeOffset currentDateTime)
        {
            this.CurrentDateTime = currentDateTime;
        }

        public LicenseExpiredException(string message, DateTimeOffset currentDateTime) : base(message)
        {
            this.CurrentDateTime = currentDateTime;
        }

        public DateTimeOffset CurrentDateTime { get; set; }
    }
}