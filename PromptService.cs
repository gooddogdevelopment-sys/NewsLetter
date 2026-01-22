namespace NewsLetter;

public interface IPromptService
{
    string GetNewsLetterPrompt();
}

public class PromptService : IPromptService
{
    public string GetNewsLetterPrompt()
    {
        const string prompt = """
                              Act as a Senior Software Architect. 
                              Generate a concise daily tip for a newsletter regarding C#.
                              The focus should be on best practices, performance, or clean code.
                              Include a short C# code snippet if relevant.
                              Format the entire output as a single HTML string for an email body.
                              """;
        return prompt;
    }
}