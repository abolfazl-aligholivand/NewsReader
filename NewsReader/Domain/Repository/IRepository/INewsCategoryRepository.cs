using NewsReader.Domain.Models;

namespace NewsReader.Domain.Repository.IRepository
{
    public interface INewsCategoryRepository
    {
        Task<NewsCategory> GetAsync(int id);
        Task<NewsCategory> GetAsync(string category);
        Task<IEnumerable<NewsCategory>> GetAllAsync();
        Task<NewsCategory> CreateAsynce(NewsCategory category);
        NewsCategory Update(NewsCategory category);
        Task<bool> DeleteAsynce(int id);
    }
}
