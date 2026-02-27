using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsLetter;
using NewsLetter.AiProviders;
using NewsLetter.Database;
using NewsLetter.Models;
using NewsLetter.Services;
using Resend;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var geminiApiKey = builder.Configuration["GEMINI_API_KEY"] 
                   ?? throw new InvalidOperationException("Gemini API Key is missing.");

builder.Services.Configure<ResendClientOptions>(options =>
{
    options.ApiToken = builder.Configuration["EMAIL_API_KEY"]
                       ?? throw new KeyNotFoundException("Email api key not found");
});

builder.Services.AddHttpClient<IResend, ResendClient>();

builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.Configure<EmailServiceOptions>(options =>
{
    options.FromEmail = builder.Configuration["FROM_EMAIL"] ?? throw new InvalidOperationException("No FROM_EMAIL detected");
    options.ToEmail = builder.Configuration["TO_EMAIL"] ?? throw new InvalidOperationException("No TO_EMAIL detected");
});
builder.Services.AddScoped<IGeminiContentProvider>(_ => new GeminiContentProvider(geminiApiKey));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INewsLetterService, NewsletterService>();

builder.Services.AddScoped<NewsLetterJob>();

var host = builder.Build();

using var scope = host.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await db.Database.MigrateAsync();

var job = scope.ServiceProvider.GetRequiredService<NewsLetterJob>();

await job.SendAsync();