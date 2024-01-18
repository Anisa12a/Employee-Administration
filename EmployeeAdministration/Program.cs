using EmployeeAdministration.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AdministrationContext>(
    options =>
    {
        options.UseMySql(builder.Configuration.GetConnectionString("Ef_MySql_Db"),
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));
    });

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corsapp");
app.UseAuthorization();

//app cors

app.MapControllers();

//These are needed for each of POST requests that we have to make them happen:

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "GetTaskById",
        pattern: "api/employees/Tasks",
        defaults: new { controller = "Employee", action = "GetTaskById" });
    {
        endpoints.MapControllers();
    }
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "GetEmployee",
        pattern: "api/administrator/addEmployeeToProject",
        defaults: new { controller = "Administrator", action = "GetEmployee" });
    {
        endpoints.MapControllers();
    }
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "GetTask",
        pattern: "api/administrator/createTask",
        defaults: new { controller = "Administrator", action = "GetTask" });
    {
        endpoints.MapControllers();
    }
});

app.Run();
