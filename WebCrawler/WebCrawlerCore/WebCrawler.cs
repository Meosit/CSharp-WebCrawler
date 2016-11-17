using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace WebCrawlerCore
{
    public class WebCrawler : ISimpleWebCrawler
    {
        private readonly ILog _logger;
        private readonly IHtmlUrlExtracter _htmlUrlExtracter;

        public const int MaxCrawlDepth = 4;

        private int _crawlDepth = MaxCrawlDepth;

        public int CrawlDepth
        {
            get { return _crawlDepth; } 
            set
            {
                if (value > MaxCrawlDepth)
                {
                    throw new ArgumentException();
                }
                _crawlDepth = value;
            }
        }

        public WebCrawler(IHtmlUrlExtracter extracter, int crawlDepth, ILog logger)
        {
            if (extracter == null) throw new ArgumentNullException(nameof(extracter));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            CrawlDepth = crawlDepth;
            _logger = logger;
            _htmlUrlExtracter = extracter;
        }

        public WebCrawler(int crawlDepth, ILog logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException();
            }
            CrawlDepth = crawlDepth;
            _logger = logger;
            _htmlUrlExtracter = new AngleSharpUrlExtracter();
        }


        public async Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls)
        {
            return await AsyncCrawlUrls(rootUrls);
        }

        private async Task<CrawlResult> AsyncCrawlUrls(string[] urlsToCrawl)
        {
            Dictionary<string, CrawlResult> crawledUrls = new Dictionary<string, CrawlResult>();

            foreach (string url in urlsToCrawl)
            {
                string[] nestedUrls = await LoadPageAndExtractUniqueUrls(url);
                if (nestedUrls != null)
                {
                    CrawlResult nestedResult = await CrawlNestedUrls(0, nestedUrls);
                    crawledUrls.Add(url, nestedResult);
                }
            }

            return new CrawlResult(crawledUrls);
        }

        private async Task<CrawlResult> CrawlNestedUrls(int currentDepth, string[] urlsToCrawl)
        {
            Dictionary<string, CrawlResult> crawledUrls = null;
            if (currentDepth <= _crawlDepth)
            {
                crawledUrls = new Dictionary<string, CrawlResult>();
                foreach (string url in urlsToCrawl)
                {
                    string[] nestedUrls = await LoadPageAndExtractUniqueUrls(url);
                    if (nestedUrls != null)
                    {
                        CrawlResult nestedResult = await CrawlNestedUrls(currentDepth + 1, nestedUrls);
                        crawledUrls.Add(url, nestedResult);
                    }
                }
            }
            return new CrawlResult(crawledUrls);
        }

        private async Task<string[]> LoadPageAndExtractUniqueUrls(string url)
        {
            string[] result = null;
            string page = await LoadPage(url);
            if (page != null)
            {
                result = _htmlUrlExtracter
                    .ExtractUrls(page)
                    .Select(x => GetAbsoluteUrl(url, x))
                    .Distinct()
                    .ToArray();
            }
            return result;
        }


        private async Task<string> LoadPage(string url)
        {
            string result = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    result = await httpClient.GetStringAsync(url);
                }
            }
            catch (Exception e)
            {
                _logger.Warn("Exception while loading page: " + e.Message);
            }
            return result;
        }

        private string GetAbsoluteUrl(string parentUrl, string url)
        {
            _logger.Debug($"Parent url {parentUrl} || Url {url}");
            string absoluteUrl = !string.IsNullOrEmpty(parentUrl)
                ? new Uri(new Uri(parentUrl), url).AbsoluteUri
                : url;
            return absoluteUrl;
        }
    }
}