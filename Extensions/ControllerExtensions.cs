using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Common;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ToActionResult<T>(this ControllerBase controller, ServiceResponse<T> response)
        {
            if (!response.Success)
            {
                return controller.StatusCode(response.StatusCode, new { errors = response.ErrorMessages });
            }

            return controller.Ok(response.Data);
        }
    }
}