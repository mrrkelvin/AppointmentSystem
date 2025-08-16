using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Extensions;
using AppointmentSystem.Services;
using AppointmentSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlotsController : ControllerBase
    {
        private ISlotService _slotService;

        public SlotsController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSlots() =>
            this.ToActionResult(await _slotService.GetSlotsAsync());

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateSlots([FromQuery] DateOnly date) =>
            this.ToActionResult(await _slotService.GenerateSlotAsync(date));
    }
}