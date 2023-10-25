using NewsReader.Domain.Helpers;

namespace NewsReaderTest
{
    public class RSSFeedReaderTest
    {
        [Fact]
        public void FeedReaderTest()
        {
            var url = "https://feeds.feedburner.com/TheHackersNews";
            var todayNews = FeedReader.ReadRSSFeed(url, out string error);
            Assert.NotEmpty(todayNews);
        }
    }
}