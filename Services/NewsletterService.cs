using Microsoft.EntityFrameworkCore;
using NewsLetter.Database;
using NewsLetter.Models;

namespace NewsLetter.Services;

public interface INewsLetterService
{
    Task AddNewsletterAsync(Newsletter newsletter);
    Task<List<Newsletter>> GetAllNewsLettersAsync();
    Task<Newsletter?> GetNewsletterByIdAsync(int id);
}

public class NewsletterService (AppDbContext context) : INewsLetterService
{
    public async Task AddNewsletterAsync(Newsletter newsletter)
    {
        await context.NewsLetters.AddAsync(newsletter);
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Newsletter>> GetAllNewsLettersAsync()
    {
        return await context.NewsLetters.ToListAsync();
    }
    
    public async Task<Newsletter?> GetNewsletterByIdAsync(int id)
    {
        return await context.NewsLetters.FindAsync(id);
    }
}