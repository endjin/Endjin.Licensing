namespace Endjin.Licensing.Domain
{
    #region Using Directives

    using System.Xml;

    using Endjin.Licensing.Contracts.Domain;

    #endregion

    public sealed class ClientLicense : IClientLicense
    {
        public XmlDocument Content { get; set; }

        public static ClientLicense Create(string content)
        {
            var doc = new XmlDocument();
            doc.LoadXml(content);

            return new ClientLicense { Content = doc };
        }
    }
}