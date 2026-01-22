using NewsLetter.AiProviders;
using Quartz;

namespace NewsLetter;

public class NewsLetterJob (IEmailService emailService, IPromptService promptService, IGeminiContentProvider geminiProvider): IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var prompt = promptService.GetNewsLetterPrompt();
        var responseContent = await geminiProvider.GenerateContent("gemini-2.5-flash", prompt);
        await emailService.SendEmailAsync("brandondeckeritprojects@gmail.com", "Daily Dev Tip", responseContent);
    }
}