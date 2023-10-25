using NewsReader.Domain.Models;

namespace NewsReader.Domain.Repository.IRepository
{
    public interface IWebsiteCategoryRepository
    {
        Task<WebsiteCategory> GetAsync(int id);
        Task<WebsiteCategory> GetAsync(string category);
        Task<IEnumerable<WebsiteCategory>> GetAllAsync();
        Task<WebsiteCategory> CreateAsynce(WebsiteCategory category);
        WebsiteCategory Update(WebsiteCategory category);
        Task<bool> DeleteAsynce(int id);
    }
}
