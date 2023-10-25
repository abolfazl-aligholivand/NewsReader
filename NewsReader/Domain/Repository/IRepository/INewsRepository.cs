using NewsReader.Domain.DTO.NewsDTO;
using NewsReader.Domain.Models;

namespace NewsReader.Domain.Repository.IRepository
{
    public interface INewsRepository
    {
        Task<News> GetAsync(Guid id);
        Task<News> GetAsync(string newsGuid);
        Task<IEnumerable<News>> GetAllAsync(GetNewsFilterDTO filter);
        Task<News> CreateAsynce(News news);
        News Update(News news);
        Task<bool> DeleteAsynce(Guid id);
    }
}
