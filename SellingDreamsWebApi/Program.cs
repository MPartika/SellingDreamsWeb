using Microsoft.EntityFrameworkCore;
using SellingDreamsCommandHandler;
using SellingDreamsInfrastructure;
using SellingDreamsService;
using SellingDreamsWebApi;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("AppDomain", AppDomain.CurrentDomain)
    .WriteTo.Console()
    .WriteTo.Http(requestUri: "http://localhost:5044", queueLimitBytes: null)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();
builder.Services.AddDatabase();
builder.Services.ConfigureApplicationServices();
builder.Services.AddCommandHandlers();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "Handled {RequestPath}";

    // Emit debug-level events instead of the defaults
    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Information;

    // Attach additional properties to the request completion event
    options.EnrichDiagnosticContext = async (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);

        var requestBody = await ConfigureLogging.GetRequestBody(httpContext.Request);
        diagnosticContext.Set("RequestBody", requestBody);
    };
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InfrastructureDbContext>();

    db.Database.Migrate();
}

app.Run();
