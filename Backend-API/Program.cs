using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Backend_API;
using Microsoft.OpenApi.Any;
using Backend_API.Options;
using Refit;
using Backend_API.Refit;
using Microsoft.OpenApi.Models;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRefitClient<IAvatarClient>()
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri("https://api.dicebear.com/7.x/lorelei/svg");
    });
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.JWT));
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
builder.Services.AddCustomServices();
builder.Services.AddRepositoryServices();
builder.Services.AddJWTSupport(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHangfireSupport(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.MapType<DateOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString("2000-01-01")
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });

    var filepath = Path.Combine(System.AppContext.BaseDirectory, "Backend-API.xml");
    options.IncludeXmlComments(filepath);
}); 

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    Startup.CreateRolesAndPowerUser(scope.ServiceProvider).Wait();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
    app.MapHangfireDashboard("/hangfire");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

