using Octokit;

namespace GitHubGetter.Services.Interfaces;

public interface IGitHubService
{
    Task<IReadOnlyList<GitHubCommit>> GetCommits(string repositoryName, string userName);
}
