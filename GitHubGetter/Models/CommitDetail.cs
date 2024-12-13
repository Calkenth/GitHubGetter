namespace GitHubGetter.Models;

public class CommitDetail
{
    public Guid Id { get; set; }

    public required string UserName { get; set; }

    public required string RepositoryName { get; set; }

    public required string Sha { get; set; }

    public required string CommitMessage { get; set; }

    public required string Committer { get; set; }
}
