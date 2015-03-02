namespace Endjin.Licensing.Infrastructure.Contracts.Storage
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Endjin.Licensing.Contracts.Domain;

    #endregion

    /// <summary>
    /// Responsible for storing and retrieving the <seealso cref="ILicense"/>
    /// </summary>
    public interface ILicenseRepository
    {
        Task<ILicense> GetAsync(Guid id);

        Task PersistAsync(ILicense license);
    }
}