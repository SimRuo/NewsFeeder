using Microsoft.AspNetCore.Mvc;
using NewsfeederApi.DTOs;
using NewsfeederApi.Models;
using NewsfeederApi.Services;

namespace NewsfeederApi.Controllers

/// <summary>
/// API Controllersena hanterar bara HTTP routing och svar. Dom pekar vidare på
/// services som innehåller affärslogik.
/// </summary>
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleService _service;

        public ArticlesController(ArticleService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetFiltered([FromQuery] ArticleQueryDto query)
        {
            return await _service.GetFilteredAsync(query);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetById(int id)
        {
            var article = await _service.GetByIdAsync(id);
            return article == null ? NotFound() : Ok(article);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleDto>> Create(CreateArticleDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateArticleDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
