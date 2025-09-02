using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("InMemoryDatabase");
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddAutoMapper(am => am.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

Console.WriteLine($"--> CommandService Endpoint {builder.Configuration["CommandService"]}");
var app = builder.Build();

PrepDb.PrepPopulation(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
