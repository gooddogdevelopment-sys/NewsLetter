using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsLetter;
using NewsLetter.AiProviders;
using Quartz;
using Resend;

var builder = Host.CreateApplicationBuilder(args);

var geminiApiKey = builder.Configuration["GeminiApiKey"] 
                   ?? throw new InvalidOperationException("Gemini API Key is missing.");

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("NewsletterJob");
    q.AddJob<NewsLetterJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("NewsletterTrigger")
        // .StartNow()
        // .WithCronSchedule("0 0 9 ? * MON-FRI") // 9 AM Monday-Friday
        .WithCronSchedule("0 * * ? * *") // every minute at second 0
        ); 
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

builder.Services.Configure<ResendClientOptions>(options =>
{
    options.ApiToken = builder.Configuration["SendGridApiKey"]
                       ?? throw new KeyNotFoundException("Email api key not found");
});

builder.Services.AddHttpClient<IResend, ResendClient>();

builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddScoped<IGeminiContentProvider>(_ => new GeminiContentProvider(geminiApiKey));
builder.Services.AddScoped<IEmailService, EmailService>();

var host = builder.Build();
host.Run();