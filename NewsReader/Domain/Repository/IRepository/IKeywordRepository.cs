using NewsReader.Domain.Models;

namespace NewsReader.Domain.Repository.IRepository
{
    public interface IKeywordRepository
    {
        Task<Keyword> GetAsync(int id);
        Task<Keyword> GetAsync(string keyword);
        Task<IEnumerable<Keyword>> GetAllAsync();
        Task<Keyword> CreateAsynce(Keyword keyword);
        Keyword Update(Keyword keyword);
        Task<bool> DeleteAsynce(int id);
    }
}
