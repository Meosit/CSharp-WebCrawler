using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebCrawlerCore;

namespace WebCrawler.Model
{
    class XmlCrawlerConfig : ICrawlerConfig
    {

        private readonly int CrawlDepth;
        private readonly string[] RootUrls;

        public XmlCrawlerConfig(string filename)
        {
            try
            {
                XDocument document = XDocument.Load(filename);
                XElement root = document.Root;

                CrawlDepth = int.Parse(root.Element("depth").Value);
                RootUrls = root.Element("rootResources")
                        .Elements("resource")
                        .Select(x => x.Value)
                        .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            }
            catch (Exception e)
            {
                throw new CrawlConfigException("Something wrong with config.", e);
            }
        }

        public int GetCrawlDepth()
        {
            return CrawlDepth;
        }

        public string[] GetRootUrls()
        {
            return RootUrls;
        }
    }
}
