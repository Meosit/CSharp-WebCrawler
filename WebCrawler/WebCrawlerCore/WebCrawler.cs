using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawlerCore
{
    public class WebCrawler
    {

        private IHtmlUrlExtracter _htmlUrlExtracter;

        public WebCrawler(IHtmlUrlExtracter extracter)
        {
            if (extracter == null)
            {
                throw new ArgumentNullException();
            }
            _htmlUrlExtracter = extracter;
        }

        public WebCrawler()
        {
            _htmlUrlExtracter = new AngleSharpUrlExtracter();
        }

        public async Task<CrawlResult> CrawlUrls(ICrawlerConfig config)
        {
            Dictionary<string, CrawlResult> crawledUrls = new Dictionary<string, CrawlResult>();
            string[] urlsToCrawl = config.GetRootUrls();
            int crawlDepth = config.GetCrawlDepth();

            for(int i = 0; i < urlsToCrawl.Length; i++)
            {
                
            }




            return new CrawlResult(crawledUrls);
        }

        private async Task<CrawlResult> CrawlNestedUrls(int currentDepth, int maxDepth, string[] urlsToCrawl)
        {
            Dictionary<string, CrawlResult> crawledUrls = new Dictionary<string, CrawlResult>();


            return new CrawlResult(crawledUrls);
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
                // logginig here
            }
            return result;
        }

    }
}