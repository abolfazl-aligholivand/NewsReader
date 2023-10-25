using NewsReader.Domain.Mappings;
using NewsReader.Domain.Models;

namespace NewsReader.Domain.DTO.WebsiteDTO
{
    public record WebsiteDTO : IMapFrom<Website>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string FeedLink { get; set; }
        public int CategoryId { get; set; }
    }
}
