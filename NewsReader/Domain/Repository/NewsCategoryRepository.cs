using Microsoft.EntityFrameworkCore;
using NewsReader.Domain.Data;
using NewsReader.Domain.Models;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Domain.Repository
{
    public class NewsCategoryRepository : INewsCategoryRepository
    {
        private readonly NewsReaderContext _context;
        public NewsCategoryRepository(NewsReaderContext context)
        {
            _context = context;
        }

        public async Task<NewsCategory> CreateAsynce(NewsCategory category)
        {
            var entity = await _context.NewsCategories.AddAsync(category);
            return entity.Entity;
        }

        public async Task<bool> DeleteAsynce(int id)
        {
            var category = await _context.NewsCategories.Where(d=>d.Id == id).FirstOrDefaultAsync();
            if(category is not null)
            {
                _context.NewsCategories.Remove(category);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<NewsCategory>> GetAllAsync()
        {
            var categories = await _context.NewsCategories.AsNoTracking().ToListAsync();
            return categories;
        }

        public async Task<NewsCategory> GetAsync(int id)
        {
            var category = await _context.NewsCategories.Where(d => d.Id == id).FirstOrDefaultAsync();
            return category;
        }

        public async Task<NewsCategory> GetAsync(string category)
        {
            var result = await _context.NewsCategories.Where(d => d.Category == category).FirstOrDefaultAsync();
            return result;
        }

        public NewsCategory Update(NewsCategory category)
        {
            var result = _context.NewsCategories.Update(category);
            return result.Entity;
        }
    }
}
