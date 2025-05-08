using Microsoft.AspNetCore.Mvc;
using NIA.OnlineApp.Data.Entities;
using NIA.OnlineApp.InteractiveAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIA.OnlineApp.InteractiveAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeUtilController : ControllerBase
    {
        private readonly ITypeUtilService _typeUtilService;

        public TypeUtilController(ITypeUtilService typeUtilService)
        {
            _typeUtilService = typeUtilService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attributes = await _typeUtilService.GetAllAsync();
            return Ok(attributes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTypeId(int id)
        {
            var item = await _typeUtilService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TypeUtil typeUtil)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _typeUtilService.AddAsync(typeUtil);
            return success ? Ok("Created successfully") : StatusCode(500, "Insert failed");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TypeUtil typeUtil)
        {
            if (id != typeUtil.Id)
                return BadRequest("ID mismatch");

            var success = await _typeUtilService.UpdateAsync(typeUtil);
            return success ? Ok("Updated successfully") : StatusCode(500, "Update failed");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _typeUtilService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            var success = await _typeUtilService.DeleteAsync(item);
            return success ? Ok("Deleted successfully") : StatusCode(500, "Delete failed");
        }
    }
}
