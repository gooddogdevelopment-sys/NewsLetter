using Google.GenAI;

namespace NewsLetter.AiProviders;

public interface IGeminiContentProvider
{
    Task<string> GenerateContent(string model, string prompt);
}

public class GeminiContentProvider(string apiKey) : IGeminiContentProvider
{
    private readonly Client _client = new(apiKey: apiKey);

    public async Task<string> GenerateContent(string model, string prompt)
    {
        try
        {
            Console.WriteLine("Generating content");
            var response = await _client.Models.GenerateContentAsync(model, prompt);
            var responseText = response.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;
            if (responseText is null) throw new NullReferenceException("No content in Gemini response body");
            
            Console.WriteLine("Successfully generated content");
            return responseText;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}