using System;
using System.Configuration;
using System.Threading.Tasks;
using log4net;
using WebCrawlerCore;

namespace WebCrawler.Model
{
    internal class WebCrawlerModel
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(WebCrawlerModel));
        private readonly WebCrawlerCore.WebCrawler _webCrawler;
        private readonly ICrawlerConfig _config;

        internal WebCrawlerModel()
        {
            _webCrawler = new WebCrawlerCore.WebCrawler(WebCrawlerCore.WebCrawler.MaxCrawlDepth, _logger);
        }

        internal Task<CrawlResult> ExecuteCrawlingAsync()
        {
            ICrawlerConfig config = (ICrawlerConfig) ConfigurationManager.GetSection("crawler");
            _webCrawler.CrawlDepth = config.GetCrawlDepth();
            return _webCrawler.PerformCrawlingAsync(config.GetRootUrls());
        }
    }
}