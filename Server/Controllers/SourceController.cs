using Microsoft.AspNetCore.Mvc;
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
    public class SourcesController : ControllerBase
    {
        private readonly SourceService _service;

        public SourcesController(SourceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Source>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Source>> GetById(int id)
        {
            var source = await _service.GetByIdAsync(id);
            return source == null ? NotFound() : Ok(source);
        }

        [HttpPost]
        public async Task<ActionResult<Source>> Create(Source source)
        {
            var created = await _service.CreateAsync(source);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Source source)
        {
            var success = await _service.UpdateAsync(id, source);
            return success ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
