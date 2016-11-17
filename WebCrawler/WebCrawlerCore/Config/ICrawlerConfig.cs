namespace WebCrawlerCore
{
    public interface ICrawlerConfig
    {
        int GetCrawlDepth();
        string[] GetRootUrls();
    }
}