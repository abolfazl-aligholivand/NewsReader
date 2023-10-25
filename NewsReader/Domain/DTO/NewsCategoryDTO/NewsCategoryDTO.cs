using NewsReader.Domain.Mappings;
using NewsReader.Domain.Models;

namespace NewsReader.Domain.DTO.NewsCategoryDTO
{
    public class NewsCategoryDTO : IMapFrom<NewsCategory>
    {
        public int Id { get; set; }
        public string Category { get; set; }
    }
}
