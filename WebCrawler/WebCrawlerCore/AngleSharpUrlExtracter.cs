using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

namespace WebCrawlerCore
{
    public class AngleSharpUrlExtracter : IHtmlUrlExtracter
    {
        public string[] ExtractUrls(string htmlContent)
        {
            List<string> hrefTags = new List<string>();
            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.Parse(htmlContent);
            return document.QuerySelectorAll("a")
                .Select(x => x.GetAttribute("href"))
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();
        }
    }
}