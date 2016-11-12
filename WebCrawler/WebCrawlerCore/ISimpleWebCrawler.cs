using System.Threading.Tasks;

namespace WebCrawlerCore
{
    public interface ISimpleWebCrawler
    {
        Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls);
    }
}