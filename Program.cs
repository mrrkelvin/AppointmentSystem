using AppointmentSystem.Data;
using AppointmentSystem.Mappings;
using AppointmentSystem.Models;
using AppointmentSystem.Services;
using AppointmentSystem.Services.Interface;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<ISlotService, SlotService>();
builder.Services.AddScoped<ISettingService, SettingService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Runtime seeding
    if (!await db.Settings.AnyAsync())
    {
        var settings = new List<Setting>
        {
            new Setting { Key = "SlotDurationMinutes", Value = "30" },
            new Setting { Key = "WorkingHoursStart", Value = "09:00" },
            new Setting { Key = "WorkingHoursEnd", Value = "18:00" },
            new Setting { Key = "MaxBookingsPerSlot", Value = "1" }
        };

        db.Settings.AddRange(settings);
        await db.SaveChangesAsync();
    }
}


app.Run();