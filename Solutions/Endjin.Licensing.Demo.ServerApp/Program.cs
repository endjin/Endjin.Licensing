namespace Endjin.Licensing.Demo.ServerApp
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.IO;

    using Endjin.Licensing.Domain;
    using Endjin.Licensing.Extensions;
    using Endjin.Licensing.Infrastructure.Crypto;
    using Endjin.Licensing.Infrastructure.Generators;

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            var dataDirectory = @"..\..\..\..\LicenseData".ResolveBaseDirectory();
            var publicKeyPath = @"..\..\..\..\LicenseData\PublicKey.xml".ResolveBaseDirectory();
            var licensePath = @"..\..\..\..\LicenseData\License.xml".ResolveBaseDirectory();

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            var licenseCriteria = new LicenseCriteria
            {
                ExpirationDate = DateTimeOffset.UtcNow.LastDayOfMonth().EndOfDay(),
                IssueDate = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                MetaData = new Dictionary<string, string> { { "LicensedCores", "2" } },
                Type = "Subscription"
            };

            var privateKey = new RsaPrivateKeyProvider().Create();
            var serverLicense = new ServerLicenseGenerator().Generate(privateKey, licenseCriteria);
            var clientLicense = serverLicense.ToClientLicense();

            // In a real implementation, you would embedd the public key into the assembly, via a resource file
            File.WriteAllText(publicKeyPath, privateKey.ExtractPublicKey().Contents);

            // In a real implementation you would implement ILicenseRepository
            File.WriteAllText(licensePath, clientLicense.Content.InnerXml);

            Console.WriteLine(Messsages.LicenseGenerated, dataDirectory);
            Console.WriteLine(Messsages.PressAnyKey);
            
            Console.ReadKey();
        }
    }
}