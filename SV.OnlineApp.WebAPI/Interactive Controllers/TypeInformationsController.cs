using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.InteractiveAPI.Services;

namespace SY.OnlineApp.InteractiveAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeInformationsController : ControllerBase
    {
        private readonly ITypeInformationService _typeInformationService;
        private readonly ITypeUtilService _typeUtilService;

        public TypeInformationsController(ITypeInformationService typeInformationService, ITypeUtilService typeUtilService)
        {
            _typeInformationService = typeInformationService;
            _typeUtilService = typeUtilService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attributes = await _typeInformationService.GetAllAsync();
            return Ok(attributes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTypeId(int id)
        {
            var items = await _typeInformationService.GetByTypeIdAsync(id);
            if (items == null || !items.Any())
                return NotFound();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TypeInformation typeInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _typeInformationService.InsertMultipleAsync(new List<TypeInformation> { typeInformation });
            return success ? Ok("Created successfully") : StatusCode(500, "Insert failed");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TypeInformation typeInformation)
        {
            if (id != typeInformation.Id)
                return BadRequest("ID mismatch");

            var success = await _typeInformationService.UpdateAttributesAsync(id, typeInformation);
            return success ? Ok("Updated successfully") : StatusCode(500, "Update failed");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var items = await _typeInformationService.GetByTypeIdAsync(id);
            var item = items?.FirstOrDefault();
            if (item == null)
                return NotFound();

            var success = await _typeInformationService.DeleteAttributesAsync(id, item);
            return success ? Ok("Deleted successfully") : StatusCode(500, "Delete failed");
        }
    }
}
