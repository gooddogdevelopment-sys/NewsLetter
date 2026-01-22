using NewsLetter.AiProviders;

namespace NewsLetter;

public class NewsLetterJob (IEmailService emailService, IPromptService promptService, IGeminiContentProvider geminiProvider)
{
    public async Task Execute()
    {
        var prompt = promptService.GetNewsLetterPrompt();
        var responseContent = await geminiProvider.GenerateContent("gemini-2.5-flash", prompt);
        await emailService.SendEmailAsync("brandondeckeritprojects@gmail.com", "Daily Dev Tip", responseContent);
    }
}