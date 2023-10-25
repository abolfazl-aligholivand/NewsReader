using Microsoft.EntityFrameworkCore;
using NewsReader.Domain.Data;
using NewsReader.Domain.Models;
using NewsReader.Domain.Repository.IRepository;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace NewsReader.Domain.Repository
{
    public class NewsKeywordRepository : INewsKeywordRepository
    {
        private readonly NewsReaderContext _context;
        public NewsKeywordRepository(NewsReaderContext context)
        {
            _context = context;
        }

        public async Task<NewsKeyword> CreateAsynce(NewsKeyword newsKeyword)
        {
            var entity = await _context.NewsKeywords.AddAsync(newsKeyword);
            return entity.Entity;
        }

        public async Task<bool> DeleteAsynce(Guid id)
        {
            var newsKeyword = await _context.NewsKeywords.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (newsKeyword is not null)
            {
                _context.NewsKeywords.Remove(newsKeyword);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<NewsKeyword>> GetAllAsync()
        {
            var newsKeywords = await _context.NewsKeywords.AsNoTracking().ToListAsync();
            return newsKeywords;
        }

        public async Task<IEnumerable<NewsKeyword>> GetAllByKeywordIdAsync(int keywordId)
        {
            //var newsKeywords = await _context.NewsKeywords
            //    .Where(d=>d.FKKeywordId == keywordId).AsNoTracking().ToListAsync();

            //return newsKeywords;
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NewsKeyword>> GetAllByNewsIdAsync(Guid newsId)
        {
            //var newsKeywords = await _context.NewsKeywords
            //    .Where(d => d.FKNewsId == newsId).AsNoTracking().ToListAsync();

            //return newsKeywords;
            throw new NotImplementedException();
        }

        public async Task<NewsKeyword> GetAsync(Guid id)
        {
            var newsKeyword = await _context.NewsKeywords.Where(d => d.Id == id).FirstOrDefaultAsync();
            return newsKeyword;
        }

        public NewsKeyword Update(NewsKeyword newsKeyword)
        {
            var result = _context.NewsKeywords.Update(newsKeyword);
            return result.Entity;
        }
    }
}
