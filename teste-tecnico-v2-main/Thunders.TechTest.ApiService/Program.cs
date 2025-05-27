using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Rebus.Config;
using Rebus.OpenTelemetry.Configuration;
using Rebus.Routing.TypeBased;
using Thunders.TechTest.ApiService;
using Thunders.TechTest.ApiService.Application.Handlers;
using Thunders.TechTest.ApiService.Application.Messages;
using Thunders.TechTest.ApiService.Application.Services;
using Thunders.TechTest.ApiService.Application.Services.Interfaces;
using Thunders.TechTest.ApiService.Infrastructure.Data;
using Thunders.TechTest.ApiService.Infrastructure.Mappers;
using Thunders.TechTest.OutOfBox.Database;
using Thunders.TechTest.OutOfBox.Queues;

var builder = WebApplication.CreateBuilder(args);

var features = Features.BindFromConfiguration(builder.Configuration);

builder.AddServiceDefaults();
builder.Services.AddControllers();

builder.Services.AddScoped<PassagemVeiculoService>();
builder.Services.AddScoped<IMessageSender, RebusMessageSender>();
builder.Services.AddScoped<IPassagemVeiculoService, PassagemVeiculoService>();
builder.Services.AutoRegisterHandlersFromAssemblyOf<RelatorioProcessarHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();

var rabbitConn = builder.Configuration.GetConnectionString("RabbitMq");

builder.Services
    .AddRebus(cfg => cfg
        .Transport(t => t.UseRabbitMq(rabbitConn, "relatorio-queue"))
        .Routing(r => r.TypeBased()
                       .MapAssemblyOf<RelatorioProcessarMessage>("relatorio-queue")))
    .AutoRegisterHandlersFromAssemblyOf<RelatorioProcessarHandler>();

var conn = builder.Configuration.GetConnectionString("SqlServerInstance");

builder.Services.AddDbContext<PedagioContext>(options =>
    options.UseSqlServer(conn, sql => sql.EnableRetryOnFailure()));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Prova técnica da Thunders - Michelli Moya",
        Version = "v1",
        Description = "Exercícios pedidos na prova."
    });
    c.EnableAnnotations();
});

if (features.UseMessageBroker)
{
    builder.Services.AddBus(builder.Configuration, new SubscriptionBuilder());
}

if (features.UseEntityFramework)
{
    builder.Services.AddSqlServerDbContext<DbContext>(builder.Configuration);
}

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ApiService"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRebusInstrumentation() 
            .AddConsoleExporter();     
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
var dbContext = scope.ServiceProvider.GetRequiredService<PedagioContext>();

    const int maxRetries = 5;
    int retries = 0;
    bool migrated = false;

    while (!migrated && retries < maxRetries)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"ConnectionString: {builder.Configuration.GetConnectionString("DefaultConnection")}");
            dbContext.Database.Migrate();
            migrated = true;
            System.Diagnostics.Debug.WriteLine("Migrations aplicadas com sucesso.");
        }
        catch (Exception ex)
        {
            retries++;
            System.Diagnostics.Debug.WriteLine($"Erro ao aplicar migrations (tentativa {retries}/{maxRetries}): {ex.Message}");
            if (retries == maxRetries)
            {
                Console.WriteLine("Falha ao aplicar migrations após várias tentativas.");
                throw;
            }

            Thread.Sleep(5000);
        }
    }
}

app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
