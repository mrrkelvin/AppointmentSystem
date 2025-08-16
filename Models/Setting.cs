using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AppointmentSystem.Models
{
    public class Setting
    {
        public int Id { get; set; }

        public required string Key { get; set; }

        public required string Value { get; set; }
    }
}