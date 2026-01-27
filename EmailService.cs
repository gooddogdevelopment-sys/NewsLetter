using Resend;

namespace NewsLetter;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, string from);
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
}

public class EmailServiceOptions
{
    public required string FromEmail { get; set; } 
    public required string ToEmail { get; set; }
}