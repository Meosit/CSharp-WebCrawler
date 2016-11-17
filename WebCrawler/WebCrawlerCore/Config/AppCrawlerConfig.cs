namespace WebCrawlerCore.Config
{
    public class AppCrawlerConfig : ICrawlerConfig
    {
        private readonly int _depth;
        private readonly string[] _rootUrls;

        public AppCrawlerConfig(int depth, string[] rootUrls)
        {
            _depth = depth;
            _rootUrls = rootUrls;
        }


        public int GetCrawlDepth()
        {
            return _depth;
        }

        public string[] GetRootUrls()
        {
            return _rootUrls;
        }
    }
}
