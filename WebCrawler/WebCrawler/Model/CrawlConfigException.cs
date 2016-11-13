using System;
using System.Runtime.Serialization;

namespace WebCrawler.Model
{
    [Serializable]
    internal class CrawlConfigException : Exception
    {
        public CrawlConfigException()
        {
        }

        public CrawlConfigException(string message) : base(message)
        {
        }

        public CrawlConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CrawlConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}