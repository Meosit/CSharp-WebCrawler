
using System.Collections.Generic;
using System.Net;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;

namespace WebCrawlerCore
{
    public class AngleSharpUrlExtracter : IHtmlUrlExtracter
    {
        public string[] ExtractUrls(string htmlContent)
        {
            List<string> hrefTags = new List<string>();
            var parser = new HtmlParser();
            var document = parser.Parse(htmlContent);
            
            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                hrefTags.Add(element.GetAttribute("href"));
            }

            return hrefTags.ToArray();

        }
    }
}