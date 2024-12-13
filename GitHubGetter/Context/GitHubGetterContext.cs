using GitHubGetter.Models;
using Microsoft.EntityFrameworkCore;

namespace GitHubGetter.Context;

public class GitHubGetterContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=GitHubGetter.db");
        }
    }

    public DbSet<CommitDetail> CommitDetails { get; set; }
}
