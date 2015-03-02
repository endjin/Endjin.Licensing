namespace Endjin.Licensing.Contracts.Crypto
{
    /// <summary>
    /// Represents a public or private crypto key.
    /// </summary>
    public interface ICryptoKey
    {
        string Contents { get; set; } 
    }
}