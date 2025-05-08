using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NIA.OnlineApp.Data;
using NIA.OnlineApp.Data.Entities;
using NIA.OnlineApp.InteractiveAPI.Services;

namespace NIA.OnlineApp.InteractiveAPI.Controllers
{
    public class TypeUtilController : Controller
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
            var items = await _typeUtilService.GetByIdAsync(id);
            if (items == null )
                return NotFound();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, TypeUtil typeUtil)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _typeUtilService.AddAsync(id, typeUtil);
            return success ? Ok("Created successfully") : StatusCode(500, "Insert failed");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TypeUtil typeUtil)
        {
            if (id != typeUtil.Id)
                return BadRequest("ID mismatch");

            var success = await _typeUtilService.UpdateAsync(id, typeUtil);
            return success ? Ok("Updated successfully") : StatusCode(500, "Update failed");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var items = await _typeUtilService.GetAllAsync();
            var item = items?.FirstOrDefault();
            if (item == null)
                return NotFound();

            var success = await _typeUtilService.DeleteAsync(id, item);
            return success ? Ok("Deleted successfully") : StatusCode(500, "Delete failed");
        }
    }
}
