using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace gp0.Models
{
    public class CertificateRequest
    {
        public string text { get; set; }
        public bool correct { get; set; }
        public string email { get; set; }
        public static string CheckCert(string xmlText)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlText);
            XmlElement xRoot = xml.DocumentElement;
            XmlNode data = xRoot.LastChild;
            var certdata = data.LastChild;
            var certxml = certdata.LastChild;
            var cert = certxml.LastChild;
            string certstr = cert.InnerText;
            certstr = certstr.Trim();
            return certstr;
        }
    }
}
