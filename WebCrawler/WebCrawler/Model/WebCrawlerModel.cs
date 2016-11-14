using System.Threading.Tasks;
using log4net;
using WebCrawler.ViewModel;
using WebCrawlerCore;

namespace WebCrawler.Model
{
    internal class WebCrawlerModel
    {
        private const string ConfigFilename = "config.xml";
        private readonly ILog _logger = LogManager.GetLogger(typeof(WebCrawlerModel));
        private readonly WebCrawlerCore.WebCrawler _webCrawler;
        private readonly XmlCrawlerConfig _config;

        internal WebCrawlerModel()
        {
            _config = new XmlCrawlerConfig(ConfigFilename);
            _webCrawler = new WebCrawlerCore.WebCrawler(_config.GetCrawlDepth(), _logger);
        }

        internal Task<CrawlResult> ExecuteCrawlingAsync()
        {
            return _webCrawler.PerformCrawlingAsync(_config.GetRootUrls());
        }

    }
}