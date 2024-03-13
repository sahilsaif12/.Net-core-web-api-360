
using asp.net_core_web_api_proect_1.Middleware;
using asp.net_core_web_api_proect_1.Misc;
using asp.net_core_web_api_proect_1.Services;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IDetailsService,DetailsService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kyc 360 Web Api", Version = "v1" });
    
    //c.UseInlineDefinitionsForEnums();
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}/kycWebApi.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    var xmlPath = String.Format(@"{0}\kycWebApi.XML", System.AppDomain.CurrentDomain.BaseDirectory);
    c.IncludeXmlComments(xmlPath);

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RetryMechanism>();

app.UseAuthorization();

app.MapControllers();

app.Run();
