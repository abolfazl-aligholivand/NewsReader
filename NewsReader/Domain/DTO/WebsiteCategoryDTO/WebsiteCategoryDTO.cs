using NewsReader.Domain.Mappings;
using NewsReader.Domain.Models;

namespace NewsReader.Domain.DTO.WebsiteCategoryDTO
{
    public record WebsiteCategoryDTO : IMapFrom<WebsiteCategory>
    {
        public int Id { get; set; }
        public string Category { get; set; }
    }
}
