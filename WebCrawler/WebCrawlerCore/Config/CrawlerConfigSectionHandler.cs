using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;

namespace WebCrawlerCore.Config
{
    public class CrawlerConfigSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            if (section == null)
                throw new ArgumentNullException(
                    "'section' is null. Check your app.config or web.config exists and is valid.");
            try
            {
                int depth;
                List<string> rootUrls = new List<string>();
                // Security
                XmlNode depthNode = section.SelectSingleNode("/crawler/depth");
                if (depthNode == null)
                    throw new Exception("No depth node could be found in the app.config");
                try
                {
                    depth = Convert.ToInt32(depthNode.InnerText);
                }
                catch (FormatException)
                {
                    throw new Exception("Depth node must contain only int value");
                }
                // Username
                XmlNodeList urlNodes = section.SelectNodes("/crawler/rootResources/resource");
                if (urlNodes == null)
                    throw new Exception("No rootResources/resource node could be found in the app.config");

                foreach (XmlNode node in urlNodes)
                {
                    if (node == null)
                        throw new Exception("No resource node could be found in the app.config");
                    rootUrls.Add(node.InnerText);
                }

                return new AppCrawlerConfig(depth, rootUrls.ToArray());
            }
            catch (XPathException ex)
            {
                // Catch From SelectSingleNode,SelectNodes
                throw new Exception("XPathException caught when reading config file", ex);
            }
        }
    }
}