using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NewsReader.Domain.Data;
using NewsReader.Domain.Models;
using NewsReader.Domain.Repository.IRepository;
using System.Diagnostics.CodeAnalysis;

namespace NewsReader.Domain.Repository
{
    public class WebsiteCategoryRepository : IWebsiteCategoryRepository
    {
        private readonly NewsReaderContext _context;
        public WebsiteCategoryRepository(NewsReaderContext context)
        {
            _context = context;
        }

        public async Task<WebsiteCategory> CreateAsynce(WebsiteCategory category)
        {
            var entity=await _context.WebsiteCategories.AddAsync(category);
            return entity.Entity;
        }

        public async Task<bool> DeleteAsynce(int id)
        {
            var category = await _context.WebsiteCategories.Where(d => d.Id == id).FirstOrDefaultAsync();
            if(category is not null)
            {
                _context.WebsiteCategories.Remove(category);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<WebsiteCategory>> GetAllAsync()
        {
            var categories = await _context.WebsiteCategories.AsNoTracking().ToListAsync();
            return categories;
        }

        public async Task<WebsiteCategory> GetAsync(int id)
        {
            var category = await _context.WebsiteCategories.Where(d => d.Id == id).FirstOrDefaultAsync();
            return category;
        }

        public async Task<WebsiteCategory> GetAsync(string category)
        {
            var categoryEntity = await _context.WebsiteCategories.Where(d => d.Category == category).FirstOrDefaultAsync();
            return categoryEntity;
        }

        public WebsiteCategory Update(WebsiteCategory category)
        {
            var result = _context.WebsiteCategories.Update(category);
            return result.Entity;
        }
    }
}
