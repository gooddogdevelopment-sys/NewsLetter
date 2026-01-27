using Resend;

namespace NewsLetter.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, string from);
    Task<string> ConvertToHtml(object outline);
}

public class EmailService (IResend resendClient) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body, string from)
    {
        var resp = await resendClient.EmailSendAsync( 
            new EmailMessage
        {
            From = from,
            To = to,
            Subject = subject,
            HtmlBody = body,
        } );

        Console.WriteLine(resp.Success ? "Successful Email Send" : "Failed Email Send");
    }
    
    public async Task<string> ConvertToHtml(object outline)
    {
        var templateText = await File.ReadAllTextAsync("Templates/DotNetEmailTemplate.html");
        var template = Scriban.Template.Parse(templateText);
        
        var result = await template.RenderAsync(outline);
        return result;
    }
}

