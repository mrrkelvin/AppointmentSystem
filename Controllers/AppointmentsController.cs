using AppointmentSystem.Dtos;
using AppointmentSystem.Extensions;
using AppointmentSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController(IAppointmentService appointmentService) : ControllerBase
    {
        private readonly IAppointmentService _appointmentService = appointmentService;

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