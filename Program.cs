using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsLetter;
using NewsLetter.AiProviders;
using Quartz;
using Resend;

var builder = Host.CreateApplicationBuilder(args);

var geminiApiKey = builder.Configuration["GEMINI_API_KEY"] 
                   ?? throw new InvalidOperationException("Gemini API Key is missing.");

builder.Services.AddScoped<NewsLetterJob>();


builder.Services.Configure<ResendClientOptions>(options =>
{
    options.ApiToken = builder.Configuration["EMAIL_API_KEY"]
                       ?? throw new KeyNotFoundException("Email api key not found");
});

builder.Services.AddHttpClient<IResend, ResendClient>();

builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddScoped<IGeminiContentProvider>(_ => new GeminiContentProvider(geminiApiKey));
builder.Services.AddScoped<IEmailService, EmailService>();

var host = builder.Build();
host.Run();