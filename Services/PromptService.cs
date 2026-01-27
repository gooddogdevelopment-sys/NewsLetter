namespace NewsLetter.Services;

public interface IPromptService
{
    string GetNewsLetterPrompt();
}

public class PromptService : IPromptService
{
    private readonly List<string> Topics = 
    [
        "Performance & Memory Management",
        "Best Practices",
        "Clean Code",
        "Modern C# Safety & Robustness",
        "API & Microservices Architecture",
        "Modern C#"
    ];
    public string GetNewsLetterPrompt()
    {
        const string prompt = """
                              Act as a Senior Software Architect.
                              Generate a concise daily tip for a newsletter regarding C#.
                              The focus should be on best practices, performance, or clean code.
                              Include a short C# code snippet if relevant. 
                              Format the code snippet with newline characters and indentation.
                              """;
        return prompt;
    }
    
}