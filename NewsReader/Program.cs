using Google.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NewsReader.Domain.Data;
using NewsReader.Domain.GoogleTranslate;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository;
using NewsReader.Domain.Repository.IRepository;
using NewsReader.Helpers;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Repository and Services Injection
builder.Services.AddTransient<ILogger>(s => s.GetService<ILogger<Program>>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHostedService<ReadNewsBackgroundTask>();
builder.Services.AddSingleton<FileManager>();
builder.Services.AddSingleton<SecurityUtility>();
builder.Services.AddSingleton<SendMessage>();
builder.Services.AddTransient<IWebsiteRepository, WebsiteRepository>();
builder.Services.AddTransient<IWebsiteCategoryRepository, WebsiteCategoryRepository>();
builder.Services.AddTransient<INewsCategoryRepository, NewsCategoryRepository>();
builder.Services.AddTransient<IKeywordRepository, KeywordRepository>();
builder.Services.AddTransient<INewsRepository, NewsRepository>();
builder.Services.AddTransient<INewsKeywordRepository, NewsKeywordRepository>();
builder.Services.AddTransient<ITranslateProvider, TranslateProvider>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<NewsReaderContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionStringLocal"), bl =>
    {
        bl.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    }));

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        },
        Scheme = "Bearer",
        Name = "Authorization",
        In = ParameterLocation.Header,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
        });

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NewsReader", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
