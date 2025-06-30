using Microsoft.EntityFrameworkCore;
using NewsfeederApi.Data;
using NewsfeederApi.DTOs;
using NewsfeederApi.Models;

namespace NewsfeederApi.Services
{
    public class ArticleService
    {
        private readonly NewsfeederContext _context;

        public ArticleService(NewsfeederContext context)
        {
            _context = context;
        }

        public async Task<List<ArticleDto>> GetAllAsync()
        {
            return await _context.Articles
                .Include(a => a.Source)
                .Include(a => a.Category)
                .Select(a => new ArticleDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    URL = a.URL,
                    PublishedAt = a.PublishedAt,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    Summary = a.Summary,
                    Sentiment = a.Sentiment,
                    SourceId = a.SourceId,
                    SourceTitle = a.Source.Title,
                    CategoryId = a.CategoryId,
                    CategoryName = a.Category.Name
                })
                .ToListAsync();
        }
        /* Här kan vi skapa dynamiska LINQ queries för att enkelt filtrera fram alla artiklar vi vill ha.
        Det är bara lägga till variabler i ArticleQueryDto och lägga på fler IF statements */
        public async Task<List<ArticleDto>> GetFilteredAsync(ArticleQueryDto query)
        {
            var q = _context.Articles
                .Include(a => a.Source)
                .Include(a => a.Category)
                .AsQueryable();

            if (query.MinSentiment.HasValue)
                q = q.Where(a => a.Sentiment >= query.MinSentiment.Value);

            if (query.Categories != null && query.Categories.Any())
            {
                q = q.Where(a => query.Categories.Contains(a.Category.Name));
            }

            return await q.Select(a => new ArticleDto
            {
                Id = a.Id,
                Title = a.Title,
                URL = a.URL,
                PublishedAt = a.PublishedAt,
                Latitude = a.Latitude,
                Longitude = a.Longitude,
                Summary = a.Summary,
                Sentiment = a.Sentiment,
                SourceId = a.SourceId,
                SourceTitle = a.Source.Title,
                CategoryId = a.CategoryId,
                CategoryName = a.Category.Name
            }).ToListAsync();
        }
        public async Task<ArticleDto?> GetByIdAsync(int id)
        {
            return await _context.Articles
                .Include(a => a.Source)
                .Include(a => a.Category)
                .Where(a => a.Id == id)
                .Select(a => new ArticleDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    URL = a.URL,
                    PublishedAt = a.PublishedAt,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    Summary = a.Summary,
                    Sentiment = a.Sentiment,
                    SourceId = a.SourceId,
                    SourceTitle = a.Source.Title,
                    CategoryId = a.CategoryId,
                    CategoryName = a.Category.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ArticleDto> CreateAsync(CreateArticleDto dto)
        {
            var article = new Article
            {
                Title = dto.Title,
                URL = dto.URL,
                PublishedAt = dto.PublishedAt,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Summary = dto.Summary,
                Sentiment = dto.Sentiment,
                SourceId = dto.SourceId,
                CategoryId = dto.CategoryId
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            var source = await _context.Sources.FindAsync(dto.SourceId);
            var category = await _context.Categories.FindAsync(dto.CategoryId);

            return new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                URL = article.URL,
                PublishedAt = article.PublishedAt,
                Latitude = article.Latitude,
                Longitude = article.Longitude,
                Summary = article.Summary,
                Sentiment = article.Sentiment,
                SourceId = dto.SourceId,
                SourceTitle = source?.Title ?? "",
                CategoryId = dto.CategoryId,
                CategoryName = category?.Name ?? ""
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateArticleDto dto)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;

            article.Title = dto.Title;
            article.URL = dto.URL;
            article.PublishedAt = dto.PublishedAt;
            article.Latitude = dto.Latitude;
            article.Longitude = dto.Longitude;
            article.Summary = dto.Summary;
            article.Sentiment = dto.Sentiment;
            article.SourceId = dto.SourceId;
            article.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
