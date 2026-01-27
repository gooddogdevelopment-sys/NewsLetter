using Microsoft.Extensions.Options;
using NewsLetter.AiProviders;
using Quartz;

namespace NewsLetter;

public class NewsLetterJob (IEmailService emailService, IPromptService promptService, IGeminiContentProvider geminiProvider, IOptions<EmailServiceOptions> options): IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var prompt = promptService.GetNewsLetterPrompt();
        var responseContent = await geminiProvider.GenerateContent("gemini-2.5-flash", prompt);
        await emailService.SendEmailAsync
        (
            options.Value.ToEmail,
            "Daily Dev Tip",
            responseContent,
            options.Value.FromEmail
        );
    }
}