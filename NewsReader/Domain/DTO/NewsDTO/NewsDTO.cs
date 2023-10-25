using NewsReader.Domain.Mappings;
using NewsReader.Domain.Models;

namespace NewsReader.Domain.DTO.NewsDTO
{
    public class NewsDTO : IMapFrom<News>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Media { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Link { get; set; }
        public string Creator { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int WebsiteId { get; set; }
        public string Website { get; set; }
        public List<string> Keywords { get; set; }
    }
}
