using GitHubGetter.Context;
using GitHubGetter.Models;
using GitHubGetter.Repositories;
using GitHubGetter.Repositories.Interfaces;
using GitHubGetter.Services;
using GitHubGetter.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

var serviceProvider = new ServiceCollection()
    .AddDbContext<GitHubGetterContext>()
    .AddScoped<IGitHubClient>(provider =>
    {
        return new GitHubClient(new ProductHeaderValue("GitHubGetter"));
    })
    .AddScoped<ICommitDetailsRepository, CommitDetailsRepository>()
    .AddScoped<IGitHubService, GitHubService>()
.BuildServiceProvider();

var githubService = serviceProvider.GetRequiredService<IGitHubService>();
var commitDetailsRespository = serviceProvider.GetRequiredService<ICommitDetailsRepository>();

while (true)
{
    Console.WriteLine("Enter GitHub user name:");
    var userName = Console.ReadLine();
    Console.WriteLine("Enter GitHub repository name:");
    var repositoryName = Console.ReadLine();

    var commits = await githubService.GetCommits(repositoryName, userName);

    if (commits.Count != 0)
    {
        commitDetailsRespository.AddRange(
            commits.Select(x =>
                new CommitDetail
                {
                    Id = Guid.NewGuid(),
                    UserName = userName,
                    RepositoryName = repositoryName,
                    Sha = x.Sha,
                    CommitMessage = x.Commit.Message,
                    Committer = x.Commit.Committer?.Name ?? "Not provided"
                }));

        foreach (var commit in commits)
        {
            Console.WriteLine($"{commit.Repository?.Name ?? repositoryName}/{commit.Sha}: {commit.Commit.Message} [{commit.Commit.Committer?.Name}]");
        }
    }
    else
    {
        Console.WriteLine("No commits found.");
    }

    Console.WriteLine("Press any key. Press 'Esc' to exit.");

    var key = Console.ReadKey(intercept: true);

    if (key.Key == ConsoleKey.Escape)
    {
        break;
    }
}
