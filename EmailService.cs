using Resend;

namespace NewsLetter;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public class EmailService (IResend resendClient) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var resp = await resendClient.EmailSendAsync( new EmailMessage
        {
            From = "onboarding@resend.dev",
            To = to,
            Subject = subject,
            HtmlBody = body,
        } );

        Console.WriteLine(resp.Success ? "Successful Email Send" : "Failed Email Send");
    }
}