using Microsoft.EntityFrameworkCore;
using NewsReader.Domain.Data;
using NewsReader.Domain.Models;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Domain.Repository
{
    public class KeywordRepository : IKeywordRepository
    {
        private readonly NewsReaderContext _context;
        public KeywordRepository(NewsReaderContext context)
        {
            _context = context;
        }

        public async Task<Keyword> CreateAsynce(Keyword keyword)
        {
            var entity = await _context.Keywords.AddAsync(keyword);
            return entity.Entity;
        }

        public async Task<bool> DeleteAsynce(int id)
        {
            var keyword = await _context.Keywords.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (keyword is not null)
            {
                _context.Keywords.Remove(keyword);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Keyword>> GetAllAsync()
        {
            var keywords = await _context.Keywords.AsNoTracking().ToListAsync();
            return keywords;
        }

        public async Task<Keyword> GetAsync(int id)
        {
            var keyword = await _context.Keywords.Where(d => d.Id == id).FirstOrDefaultAsync();
            return keyword;
        }

        public async Task<Keyword> GetAsync(string keyword)
        {
            var keywordEntity = await _context.Keywords.Where(d => d.Title == keyword).FirstOrDefaultAsync();
            return keywordEntity;
        }

        public Keyword Update(Keyword keyword)
        {
            var result = _context.Keywords.Update(keyword);
            return result.Entity;
        }
    }
}
