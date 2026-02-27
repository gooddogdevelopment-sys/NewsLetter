using Microsoft.Extensions.Options;
using NewsLetter.AiProviders;
using NewsLetter.Models;
using NewsLetter.Services;
using Quartz;

namespace NewsLetter;

public class NewsLetterJob (
    IEmailService emailService, 
    IPromptService promptService,
    INewsLetterService newsLetterService,
    IGeminiContentProvider geminiProvider, 
    IOptions<EmailServiceOptions> options
    ): IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await SendAsync();
    }

    public async Task SendAsync()
    {
        try
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

            await newsLetterService.AddNewsletterAsync(
                new Newsletter
            {
                Title = responseContent.Title,
                CodeSnippet = responseContent.CodeSnippet,
                Overview = responseContent.Overview,
                Subject = "Daily Dev Tip", //TODO: Make this configurable
                SendDate = default,
                AiProvider = "GEMINI", //TODO: Make this configurable
                AiModel = "gemini-3-flash-preview" //TODO: Make this configurable
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}