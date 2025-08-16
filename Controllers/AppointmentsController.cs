using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Data;
using AppointmentSystem.Dtos;
using AppointmentSystem.Extensions;
using AppointmentSystem.Services;
using AppointmentSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private IAppointmentService _appointmentService;
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments() =>
            this.ToActionResult(await _appointmentService.GetAppointmentsAsync());

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto dto)
        {
            var result = await _appointmentService.CreateAppointmentAsync(dto);
            return this.ToActionResult(result);
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var result = await _appointmentService.CancelAppointmentAsync(id);
            return this.ToActionResult(result);
        }
    }
}