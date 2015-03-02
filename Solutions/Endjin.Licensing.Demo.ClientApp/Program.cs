namespace Endjin.Licensing.Demo.ClientApp
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Endjin.Licensing.Contracts.Validation;
    using Endjin.Licensing.Demo.ClientApp.Validation.Rules;
    using Endjin.Licensing.Domain;
    using Endjin.Licensing.Exceptions;
    using Endjin.Licensing.Validation;
    using Endjin.Licensing.Validation.Rules;

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            var publicKeyPath = @"..\..\..\..\LicenseData\PublicKey.xml".ResolveBaseDirectory();
            var licensePath = @"..\..\..\..\LicenseData\License.xml".ResolveBaseDirectory();

            if (!File.Exists(publicKeyPath) || !File.Exists(licensePath))
            {
                Console.WriteLine(Messages.RunServerAppFirst);
                Console.WriteLine(Messages.PressAnyKey);
                Console.ReadKey();

                Environment.Exit(-1);
            }

            ValidateLicense(publicKeyPath, licensePath);

            Console.WriteLine(Messages.NoLicenseViolations);
            Console.WriteLine(Messages.PressAnyKey);

            Console.ReadKey();
        }

        private static void ValidateLicense(string publicKeyPath, string licensePath)
        {
            var publicKey = new PublicCryptoKey { Contents = File.ReadAllText(publicKeyPath) };
            var clientLicense = ClientLicense.Create(File.ReadAllText(licensePath));

            var violations = new List<string>();

            try
            {
                var licenseValidationRules = new List<ILicenseValidationRule>
                {
                    new LicenseHasNotExpiredRule(),
                    new ValidNumberOfCoresLicenseRule()
                };

                new LicenseValidator().Validate(clientLicense, publicKey, licenseValidationRules);
            }
            catch (InvalidLicenseException exception)
            {
                violations.Add(exception.Message);
            }
            catch (AggregateException ex)
            {
                var innerExceptions = ex.InnerExceptions;

                foreach (var exception in innerExceptions)
                {
                    if (exception is LicenseViolationException)
                    {
                        violations.Add(exception.Message);
                    }
                }

                if (!violations.Any())
                {
                    throw;
                }
            }
            catch (Exception)
            {
                violations.Add(Messages.UnknownLicenseError);
            }

            if (violations.Any())
            {
                Console.WriteLine(Messages.LicenseViolationsEncountered);
                foreach (var violation in violations)
                {
                    Console.WriteLine(" - " + violation);
                }

                Console.WriteLine(Messages.PressAnyKey);
                Console.ReadKey();

                Environment.Exit(-1);
            }
        }
    }
}