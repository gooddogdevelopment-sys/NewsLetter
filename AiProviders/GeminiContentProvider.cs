using Google.GenAI;
using Google.GenAI.Types;
using NewsLetter.Models;
using Newtonsoft.Json;
using Type = Google.GenAI.Types.Type;

namespace NewsLetter.AiProviders;

public interface IGeminiContentProvider
{
    Task<NewsletterOutline> GenerateCodingNewsletterContent(string prompt);
}

public class GeminiContentProvider(string apiKey) : IGeminiContentProvider
{
    private readonly Client _client = new(apiKey: apiKey);

    public async Task<NewsletterOutline> GenerateCodingNewsletterContent(string prompt)
    {
        var responseSchemaConfig = GetResponseSchemaConfig();
        var outputJson = await GenerateContent("gemini-3-flash-preview", prompt, responseSchemaConfig);
        return ParseResponseContent<NewsletterOutline>(outputJson);
    }

    private async Task<string> GenerateContent(string model, string prompt, GenerateContentConfig config)
    {
        try
        {
            Console.WriteLine("Generating content");
            
            var response = await _client.Models.GenerateContentAsync(model, prompt, config);
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

    private static GenerateContentConfig GetResponseSchemaConfig()
    {
        var config = new GenerateContentConfig
        {
            ResponseMimeType = "application/json",
            ResponseSchema = new Schema
            {
                Type = Type.OBJECT,
                Properties = new Dictionary<string, Schema>
                {
                    
                    {
                        "title", new Schema { Type = Type.STRING }
                    },
                    {
                        "overview", new Schema { Type = Type.STRING }
                    },
                    {
                        "tip", new Schema { Type = Type.STRING }
                    },
                    {
                        "code_snippet", new Schema { Type = Type.STRING }
                    },
                }
            }
        };

        return config;
    }
    
    private static T ParseResponseContent<T>(string responseText)
    {
        return JsonConvert.DeserializeObject<T>(responseText)
               ?? throw new JsonSerializationException("Failed to parse response content");
    }
}