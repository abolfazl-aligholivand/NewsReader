using Microsoft.EntityFrameworkCore;
using NewsReader.Domain.Data;
using NewsReader.Domain.DTO.NewsDTO;
using NewsReader.Domain.Models;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Domain.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsReaderContext _context;
        public NewsRepository(NewsReaderContext context)
        {
            _context = context;
        }

        public async Task<News> CreateAsynce(News news)
        {
            var entity = await _context.Newses.AddAsync(news);
            return entity.Entity;
        }

        public async Task<bool> DeleteAsynce(Guid id)
        {
            var news = await _context.Newses.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (news is not null)
            {
                _context.Newses.Remove(news);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<News>> GetAllAsync(GetNewsFilterDTO filter)
        {
            var newses = _context.Newses
                .Include(i => i.FKCategory).Include(i => i.FKWebsite)
                .Where(d => filter.Date == null ? true : d.Date.Date == filter.Date.Value.Date)
                .AsNoTracking();
            var result = await newses.Skip(filter.PageSize * (filter.PageNumber - 1))
                    .Take(filter.PageSize).ToListAsync();

            return result;
        }

        public async Task<News> GetAsync(Guid id)
        {
            var news = await _context.Newses.Where(d => d.Id == id)
                .Include(i => i.FKCategory).Include(i => i.FKWebsite)
                .FirstOrDefaultAsync();
            return news;
        }

        public async Task<News> GetAsync(string newsGuid)
        {
            var news = await _context.Newses.Where(d => d.NewsGuid == newsGuid).FirstOrDefaultAsync();
            return news;
        }

        public News Update(News news)
        {
            var newsEntity = _context.Newses.Update(news);
            return newsEntity.Entity;
        }
    }
}
