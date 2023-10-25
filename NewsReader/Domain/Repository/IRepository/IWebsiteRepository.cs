using NewsReader.Domain.Models;

namespace NewsReader.Domain.Repository.IRepository
{
    public interface IWebsiteRepository
    {
        Task<Website> GetAsync(int id);
        Task<IEnumerable<Website>> GetAllAsync();
        Task<Website> CreateAsynce(Website website);
        Website Update(Website website);
        Task<bool> DeleteAsynce(int id);
    }
}
