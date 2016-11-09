using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawlerCore
{
    public class WebCrawler
    {
        private IHtmlUrlExtracter _htmlUrlExtracter;
        private int _crawlDepth = 0;

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
            string[] urlsToCrawl = config.GetRootUrls();
            _crawlDepth = config.GetCrawlDepth();
            return await AsyncCrawlUrls(urlsToCrawl);
        }

        private async Task<CrawlResult> AsyncCrawlUrls(string[] urlsToCrawl)
        {
            Dictionary<string, CrawlResult> crawledUrls = new Dictionary<string, CrawlResult>();

            for (int i = 0; i < urlsToCrawl.Length; i++)
            {
                string[] nestedUrls = await LoadPageAndExtractUniqueUrls(urlsToCrawl[i]);
                if (nestedUrls != null)
                {
                    CrawlResult nestedResult = await CrawlNestedUrls(0, nestedUrls);
                    crawledUrls.Add(urlsToCrawl[i], nestedResult);
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


           
            }


            return new CrawlResult(crawledUrls);
        }

        private async Task<string[]> LoadPageAndExtractUniqueUrls(string url)
        {
            string[] result = null;
            string page = await LoadPage(url);
            if (page != null)
            {
                result = _htmlUrlExtracter.ExtractUrls(page)
                    .Select(x => GetAbsoluteUrl(url, x))
                    .Distinct()
                    .ToArray();
            }
            return result;
        }


        private static async Task<string> LoadPage(string url)
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

        private static string GetAbsoluteUrl(string parentUrl, string url)
        {
            return !string.IsNullOrEmpty(parentUrl)
                ? new Uri(new Uri(parentUrl), url).AbsoluteUri
                : url;
        }
    }
}