using System.Threading.Tasks;
using log4net;
using WebCrawler.ViewModel;
using WebCrawlerCore;

namespace WebCrawler.Model
{
    internal class WebCrawlerModel
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(WebCrawlerModel));
        private readonly WebCrawlerCore.WebCrawler _webCrawler;

        internal WebCrawlerModel()
        {
            _webCrawler = new WebCrawlerCore.WebCrawler(4, _logger);
        }

        //internal Task<CrawlResult> ExecuteCrawling()
        //{
        //    return _webCrawler.PerformCrawlingAsync();
        //}

    }
}