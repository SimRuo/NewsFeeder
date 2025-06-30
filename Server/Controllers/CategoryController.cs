using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsfeederApi.Data;
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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            return category == null ? NotFound() : Ok(category);
        }
        // POST: api/category
        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            var created = await _service.CreateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        // PUT: api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            var success = await _service.UpdateAsync(id, category);
            return success ? NoContent() : BadRequest();
        }
        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}