using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Dtos;
using AppointmentSystem.Extensions;
using AppointmentSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController(ISettingService settingService) : ControllerBase
    {
        private readonly ISettingService _settingService = settingService;

        [HttpGet]
        public async Task<IActionResult> GetSettings()
        {
            return this.ToActionResult(await _settingService.GetSettings());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSettings([FromBody] SettingsDto dto)
        {
            return this.ToActionResult(await _settingService.UpdateSettings(dto));
        }
    }
}