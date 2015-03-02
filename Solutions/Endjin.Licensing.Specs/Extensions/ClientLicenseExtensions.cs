namespace Endjin.Licensing.Specs.Extensions
{
    #region Using Directives

    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    using Endjin.Licensing.Domain;

    #endregion

    public static class ClientLicenseExtensions
    {
        public static XmlDocument Tamper(this ClientLicense clientLicense, string elementName, string newValue)
        {
            var xdoc = clientLicense.Content.ToXDocument();

            var element = xdoc.Root.Descendants().FirstOrDefault(xElement => xElement.Name.LocalName == elementName);

            element.Value = newValue;

            return xdoc.ToXmlDocument();
        }

        private static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                
                return XDocument.Load(nodeReader);
            }
        }

        private static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();

            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            
            return xmlDocument;
        }
    }
}