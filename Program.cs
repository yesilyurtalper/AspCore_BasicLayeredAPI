
using System.Text.Json.Serialization;
using BasicLayeredService.API.Filters;
using BasicLayeredService.API.Extensions;
using BasicLayeredService.API.DBContext;
using BasicLayeredService.API.Services.Persistence;
using BasicLayeredService.API.Constants;
using BasicLayeredService.API.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.DTOs;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers(options => {
    options.Filters.Add<ValidationFilter>();
}).
    ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    }).
    AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

services.AddScoped<ResultLoggerFilter>();

builder.Host.AddCustomSerilog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
if (builder.Environment.IsDevelopment())
    services.AddSwaggerServices();

//string conStr = "server=localhost;port=3306;database=BasicLayeredService;user=root;password=pass;";
//string conStr = configuration.GetConnectionString("DefaultConnection");
string conStr = Environment.GetEnvironmentVariable("DB_CON_STR");
services.AddDbContext<BaseItemDbContext>(options => options.UseMySQL(conStr));

services.AddScoped<IBaseItemRepo<Post, PostQueryDto>, DbBaseItemRepo<Post, PostQueryDto>>();
services.AddScoped<IBaseItemRepo<Event, EventQueryDto>, DbEventRepo>();

services.AddAuthServices();
services.AddHttpClients();

services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

//Migrate latest database changes during startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BaseItemDbContext>();
    dbContext.Database.Migrate();
}

//custom global exception handling middleware
app.UseExceptionMiddleware();
app.UseErrorMiddleware();

//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//var elasticAPMSettings = new Dictionary<string, string>();
//elasticAPMSettings.Add("ElasticApm:SecretToken","");
//elasticAPMSettings.Add("ElasticApm:ServerUrl", Environment.GetEnvironmentVariable("ELK_APMURL"));
//elasticAPMSettings.Add("ElasticApm:ServiceName", "ECommerce.ItemService");
//app.UseAllElasticApm(new ConfigurationBuilder().AddInMemoryCollection(elasticAPMSettings).Build());

//app.UseCors("all");
app.UseCors(builder =>
    builder.WithOrigins("http://localhost:3000")//Environment.GetEnvironmentVariable("ALLOWED_ORIGINS").Split(","))
    .AllowAnyMethod().
    AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization(APIConstants.BasicLayeredServiceClient);

app.Run();
