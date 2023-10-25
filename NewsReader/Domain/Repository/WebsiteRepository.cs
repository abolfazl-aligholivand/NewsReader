using Microsoft.EntityFrameworkCore;
using NewsReader.Domain.Data;
using NewsReader.Domain.Models;
using NewsReader.Domain.Repository.IRepository;

#nullable disable

namespace NewsReader.Domain.Repository
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private readonly NewsReaderContext _context;
        public WebsiteRepository(NewsReaderContext context)
        {
            _context = context;
        }

        public async Task<Website> CreateAsynce(Website website)
        {
            var result = await _context.Websites.AddAsync(website);
            return result.Entity;
        }

        public async Task<bool> DeleteAsynce(int id)
        {
            var website = await _context.Websites.Where(d=>d.Id== id).SingleOrDefaultAsync();
            if(website is not null)
            {
                _context.Websites.Remove(website);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Website>> GetAllAsync()
        {
            var websites = await _context.Websites.AsNoTracking().ToListAsync();
            return websites;
        }

        public async Task<Website> GetAsync(int id)
        {
            var website = await _context.Websites
                .Where(d => d.Id == id).FirstOrDefaultAsync();

            return website;
        }

        public Website Update(Website website)
        {
            var result = _context.Websites.Update(website);
            return result.Entity;
        }
    }
}
