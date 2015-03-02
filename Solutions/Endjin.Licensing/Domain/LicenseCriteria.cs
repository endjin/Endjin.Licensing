namespace Endjin.Licensing.Domain
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Endjin.Licensing.Contracts.Domain;

    #endregion

    public sealed class LicenseCriteria
    {
        public DateTimeOffset ExpirationDate { get; set; }

        public Guid Id { get; set; }

        public DateTimeOffset IssueDate { get; set; }

        public Dictionary<string, string> MetaData { get; set; }

        public string Type { get; set; }
    }
}