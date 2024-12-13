using GitHubGetter.Services.Interfaces;
using Octokit;

namespace GitHubGetter.Services;

public class GitHubService : IGitHubService
{
    private readonly IGitHubClient _gitHubClient;

    public GitHubService(IGitHubClient gitHubClient)
    {
        _gitHubClient = gitHubClient;
    }

    public async Task<IReadOnlyList<GitHubCommit>> GetCommits(string repositoryName, string userName)
    {
        if (string.IsNullOrWhiteSpace(repositoryName))
        {
            Console.WriteLine($"{nameof(repositoryName)} value is invalid.");

            return [];
        }


        if (string.IsNullOrWhiteSpace(userName))
        {
            Console.WriteLine($"{nameof(userName)} value is invalid.");

            return [];
        }

        try
        {
            return await _gitHubClient.Repository.Commit.GetAll(userName, repositoryName);
        }
        catch (NotFoundException ex)
        {
            Console.WriteLine($"Repository or user not found. {ex.Message}");

            return [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while retrieving commits: {ex.Message}");

            return [];
        }
    }
}
