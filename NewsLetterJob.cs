using Microsoft.Extensions.Options;
using NewsLetter.AiProviders;
using NewsLetter.Models;
using NewsLetter.Services;
using Quartz;

namespace NewsLetter;

public class NewsLetterJob (IEmailService emailService, IPromptService promptService, IGeminiContentProvider geminiProvider, IOptions<EmailServiceOptions> options): IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await SendAsync();
    }

    public async Task SendAsync()
    {
        var prompt = promptService.GetNewsLetterPrompt();
        var responseContent = await geminiProvider.GenerateCodingNewsletterContent(prompt);
        var emailBody = await emailService.ConvertToHtml(responseContent);
        await emailService.SendEmailAsync
        (
            options.Value.ToEmail,
            "Daily Dev Tip",
            emailBody,
            options.Value.FromEmail
        );
    }
}