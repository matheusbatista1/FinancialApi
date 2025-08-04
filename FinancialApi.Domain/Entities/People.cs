using System.Security.Principal;

namespace FinancialApi.Domain.Entities;

public class People
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Document { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public List<Account> Accounts { get; set; } = new();
}