using NewsReader.Domain.Models;

namespace NewsReader.Domain.Repository.IRepository
{
    public interface INewsKeywordRepository
    {
        Task<NewsKeyword> GetAsync(Guid id);
        Task<IEnumerable<NewsKeyword>> GetAllAsync();
        Task<IEnumerable<NewsKeyword>> GetAllByNewsIdAsync(Guid newsId);
        Task<IEnumerable<NewsKeyword>> GetAllByKeywordIdAsync(int keywordId);
        Task<NewsKeyword> CreateAsynce(NewsKeyword newsKeyword);
        NewsKeyword Update(NewsKeyword newsKeyword);
        Task<bool> DeleteAsynce(Guid id);
    }
}
