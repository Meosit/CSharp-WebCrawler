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
            try
            {
                _config = (ICrawlerConfig) ConfigurationManager.GetSection("crawler");
            }
            catch (Exception e)
            {
                _logger.Error("Config error", e);
                throw new CrawlConfigException("", e);
            }
            
            _webCrawler = new WebCrawlerCore.WebCrawler(_config.GetCrawlDepth(), _logger);
        }

        internal Task<CrawlResult> ExecuteCrawlingAsync()
        {
            return _webCrawler.PerformCrawlingAsync(_config.GetRootUrls());
        }

    }
}