namespace Endjin.Licensing.Contracts.Domain
{
    using System.Xml;

    public interface IClientLicense
    {
        XmlDocument Content { get; set; }
    }
}