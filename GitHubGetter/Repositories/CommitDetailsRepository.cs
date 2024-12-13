using GitHubGetter.Context;
using GitHubGetter.Models;
using GitHubGetter.Repositories.Interfaces;

namespace GitHubGetter.Repositories;

public class CommitDetailsRepository : ICommitDetailsRepository
{
    private readonly GitHubGetterContext _context;

    public CommitDetailsRepository(GitHubGetterContext gitHubGetterContext)
    {
        _context = gitHubGetterContext;
        _context.Database.EnsureCreated();
    }

    public void Add(CommitDetail commitDetail)
    {
        if (commitDetail == null)
        {
            throw new ArgumentNullException($"${nameof(commitDetail)} value is invalid.");
        }

        if (_context.CommitDetails.Where(x => x.Sha == commitDetail.Sha).Any())
        {
            throw new Exception($"Record with SHA: {commitDetail.Sha} already exists in database.");
        }

        _context.Add(commitDetail);

        _context.SaveChanges();
    }

    public void AddRange(IEnumerable<CommitDetail> commitDetails)
    {
        if (!commitDetails.Any())
        {
            throw new ArgumentException($"${nameof(commitDetails)} is empty.");
        }

        var commitsToAdd = commitDetails.Where(x => !_context.CommitDetails.Any(y => y.Sha == x.Sha));

        _context.AddRange(commitsToAdd);

        _context.SaveChanges();
    }
}

