using JasperFx.CodeGeneration;
using Marten;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using Marten.Services;
using MartenConstructorIssue.Api;
using Npgsql;
using Oakton;
using Weasel.Core;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(options =>
{
    options.CodeGeneration.TypeLoadMode = TypeLoadMode.Auto;
            
    options.UseSystemTextJsonForSerialization(json =>
    {

    });

    options.OnException<NpgsqlException>()
        .RetryWithCooldown(TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(500))
        .Then
        .ScheduleRetry(TimeSpan.FromMinutes(15));
});

builder.Services.AddMarten(options =>
{
    const string connectionString = "";
    
    options.Connection(connectionString);
    options.DatabaseSchemaName = "marten";
    options.AutoCreateSchemaObjects = AutoCreate.All;
    options.GeneratedCodeMode = TypeLoadMode.Auto;
    
    var serializer = new SystemTextJsonSerializer();
    serializer.Customize(json =>
    {
        // Some customer serializers
    });
    
    options.Serializer(serializer);
    options.Projections.Add<SomeClassProjection>(ProjectionLifecycle.Live);
}).UseLightweightSessions()
.IntegrateWithWolverine()
.EventForwardingToWolverine()
.AddAsyncDaemon(DaemonMode.Solo);

builder.Host.ApplyOaktonExtensions();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.RunOaktonCommands(args);
