using System;
using System.Xml;
using System.Text;

namespace Fussball.SimplePointsSystem
{
    public static class AuditTrailXML
    {
        public static AuditTrail CreateAuditTrailFromXml(string xml)
        {
            var auditTrail = new AuditTrail();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList itemNodes = doc.DocumentElement.ChildNodes;
            foreach (XmlNode node in itemNodes)
            {
                auditTrail.Items.Add(CreateItemFromNode(node));
            }
            return auditTrail;
        }

        private static AuditTrailItem CreateItemFromNode(XmlNode node)
        {
            return new AuditTrailItem()
            {
                When = Common.GetDateFromUnixTime(Convert.ToDouble(node.SelectSingleNode("when").InnerText)),
                What = node.SelectSingleNode("what").InnerText,
                CssAttributes = node.SelectSingleNode("css").InnerText
            };
        }

        public static string ToXml(AuditTrail auditTrail)
        {
            StringBuilder xml = new StringBuilder("<audittrail>");

            foreach (AuditTrailItem item in auditTrail.Items)
            {
                AddItemToXml(item, xml);
            }

            xml.Append("</audittrail>");
            return xml.ToString();
        }

        private static void AddItemToXml(AuditTrailItem item, StringBuilder xml)
        {
            xml.AppendFormat("<item><when>{0}</when><what>{1}</what><css>{2}</css></item>",
                                Common.GetUnixTime(item.When),
                                item.What,
                                item.CssAttributes);
        }
    }
}
