using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RestDemo.BLL.IServices;
using RestDemo.BLL.Services;
using RestDemo.Data;
using RestDemo.Mapping;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//Mapster
MappingProfiles.RegisterMappings();
builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);

//Serilog
// 2. Use Serilog and read from appsettings.json
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

//services
builder.Services.AddScoped<IUserService, UserService>();

//versioing
var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

apiVersioningBuilder.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"Project API {description.GroupName}");
        }
    });
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();