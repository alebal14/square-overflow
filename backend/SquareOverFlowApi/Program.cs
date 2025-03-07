using Microsoft.OpenApi.Models;
using SquareOverFlowCore;
using SquareOverFlowCore.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;

if (string.IsNullOrEmpty(config["Values:DataFilePath"]))
{
    throw new Exception("Required configuration 'DataFilePath' is missing");
}

builder.Services.AddSingleton<ISquareService, SquareService>();
builder.Services.AddSingleton<IStorageService, StorageService>();

builder.Services.AddSwaggerGen(d =>
{
    d.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Square Overflow API",
        Version = "v1",
        Description = "API so strong it can handle infinite squares (probably)",
        Contact = new OpenApiContact
        {
            Name = "Alexandra Balogh",
            Email = "alexandra.b.balogh@gmail.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        d.IncludeXmlComments(xmlPath);
    }
    else
    {
        Console.WriteLine($"Warning: XML documentation file not found at {xmlPath}");
        Console.WriteLine("Make sure you have <GenerateDocumentationFile>true</GenerateDocumentationFile> in your .csproj file");
    }

    d.CustomOperationIds(apiDesc =>
        apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .WithHeaders("Content-Type")
            .WithMethods("GET", "POST", "DELETE")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .WithExposedHeaders("Content-Disposition")
            .SetPreflightMaxAge(TimeSpan.FromMinutes(3));
    });
});

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Square Overflow API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
