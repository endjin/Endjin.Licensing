namespace Endjin.Licensing.Domain
{
    #region Using Directives

    using Endjin.Licensing.Contracts.Crypto;

    #endregion

    /// <summary>
    /// Represents the public key used to verify that the validity of the license.
    /// </summary>
    /// <remarks>
    /// The end user has access to the public key as this is required to validate
    /// the validity of the license. The private key, however, must be securely stored
    /// to ensure the end user does not gain aceess.
    /// </remarks>
    public sealed class PublicCryptoKey : ICryptoKey
    {
        public string Contents { get; set; }

        private bool Equals(PublicCryptoKey other)
        {
            return string.Equals(this.Contents, other.Contents);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj is PublicCryptoKey && Equals((PublicCryptoKey)obj);
        }

        public override int GetHashCode()
        {
            return (this.Contents != null ? this.Contents.GetHashCode() : 0);
        }

        public static bool operator ==(PublicCryptoKey left, PublicCryptoKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PublicCryptoKey left, PublicCryptoKey right)
        {
            return !Equals(left, right);
        }
    }
}