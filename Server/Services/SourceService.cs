using Microsoft.EntityFrameworkCore;
using NewsfeederApi.Data;
using NewsfeederApi.Models;

namespace NewsfeederApi.Services
{
    public class SourceService
    {
        private readonly NewsfeederContext _context;

        public SourceService(NewsfeederContext context)
        {
            _context = context;
        }

        public async Task<List<Source>> GetAllAsync()
        {
            return await _context.Sources
                .Include(s => s.Articles)
                .ToListAsync();
        }

        public async Task<Source?> GetByIdAsync(int id)
        {
            return await _context.Sources
                .Include(s => s.Articles)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Source> CreateAsync(Source source)
        {
            _context.Sources.Add(source);
            await _context.SaveChangesAsync();
            return source;
        }

        public async Task<bool> UpdateAsync(int id, Source source)
        {
            if (id != source.Id) return false;

            _context.Entry(source).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var source = await _context.Sources.FindAsync(id);
            if (source == null) return false;

            _context.Sources.Remove(source);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
