
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using SecurityCameras.CLI.Commands;
using SecurityCameras.CLI.Interfaces;
using SecurityCameras.CLI.Services;
using SecurityCameras.Common.Interfaces;
using SecurityCameras.Common.Services;

var builder = CoconaApp.CreateBuilder();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICsvDataService, CsvDataService>();
builder.Services.AddSingleton<ICameraSearchService, CameraSearchService>();
builder.Services.AddSingleton<IOutputFormatter, OutputFormatter>();

var app = builder.Build();
app.AddCommands<SearchCommand>();
app.Run();