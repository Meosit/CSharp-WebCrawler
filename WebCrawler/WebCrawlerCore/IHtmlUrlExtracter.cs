namespace WebCrawlerCore
{
    public interface IHtmlUrlExtracter
    {
        string[] ExtractUrls(string htmlContent);
    }
}