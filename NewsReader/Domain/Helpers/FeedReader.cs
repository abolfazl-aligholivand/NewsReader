using NewsReader.Domain.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Xml;
using System.Xml.Linq;
#nullable disable

namespace NewsReader.Domain.Helpers
{
    public class FeedReader
    {
        public static IEnumerable<News> ReadRSSFeed(string url, out string error)
        {
            try
            {
                XDocument document = XDocument.Load(url);
                XElement channel = document.Root;
                XNamespace ns = channel.GetDefaultNamespace();
                XNamespace dc = channel.GetNamespaceOfPrefix("dc");
                XNamespace media = channel.GetNamespaceOfPrefix("media");

                List<News> items = document.Descendants("item").Select(x => new News()
                {
                    Title = (string)x.Element(ns + "title"),
                    NewsGuid = (string)x.Element(ns + "guid"),
                    Link = (string)x.Element(ns + "link"),
                    Date = (DateTime)x.Element(ns + "pubDate"),
                    Description = (string)x.Element(ns + "description"),
                    Creator = x.Element(dc + "creator") is null ? (string)x.Element(ns + "author") : (string)x.Element(dc + "creator")
                }).Where(d => d.Date.Date == DateTime.Today.Date).ToList();
                error = string.Empty;
                return items;
            }
            catch(Exception ex)
            {
                error = ex.InnerException.Message;
                return Enumerable.Empty<News>();
            }
        }
    }
}
