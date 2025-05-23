﻿using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Services.Interfaces;
using SY.OnlineApp.Services.Services;

namespace SY.OnlineApp.AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _service;

        public RegisterController(IRegisterService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultMessage = await _service.RegisterUserAsync(dto);
                return Ok(new { message = resultMessage });
            }
            catch (ArgumentException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}