# NewsLetter

A .NET 10.0 worker service that generates daily C# tips using Google Gemini AI and delivers them via email using Resend.

## Features

- 🤖 **AI-Powered**: Generates concise, professional C# tips using Google's Gemini Flash model.
- 📧 **Automated Email**: Sends formatted HTML emails via Resend.
- ⏱️ **Scheduled**: Runs on a configurable schedule using Quartz.NET.
- 🐳 **Dockerized**: Includes Docker support for easy deployment.

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (optional)
- API Keys for:
  - [Google Gemini](https://ai.google.dev/)
  - [Resend](https://resend.com/)

## Configuration

Configure the application using [appsettings.json](cci:7://file:///C:/Users/brand/DevelopmentV2/NewsLetter/appsettings.json:0:0-0:0) or Environment Variables.

| Setting | Environment Variable | Description |
|---------|---------------------|-------------|
| Gemini API Key | `GEMINI_API_KEY` | Your Google Gemini API Key |
| Email API Key | `Email__EmailApiKey` | Your Resend API Key |
| From Email | `Email__FromEmail` | Sender email address (must be verified in Resend) |
| To Email | `Email__ToEmail` | Recipient email address |

### appsettings.json Example
```json
{
  "GEMINI_API_KEY": "your_gemini_key",
  "Email": {
    "EmailApiKey": "your_resend_key",
    "FromEmail": "newsletter@yourdomain.com",
    "ToEmail": "you@example.com"
  }
}
```

## Running Locally

1. Restore dependencies:
   ```bash
   dotnet restore
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

## Docker Support

Build and run using Docker Compose:

```bash
docker compose up --build
```

## Tech Stack

- **Framework**: .NET 10.0
- **AI**: Google.GenAI
- **Email**: Resend
- **Scheduling**: Quartz.NET
- **Hosting**: Microsoft.Extensions.Hosting