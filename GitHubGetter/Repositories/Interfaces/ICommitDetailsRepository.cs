using GitHubGetter.Models;

namespace GitHubGetter.Repositories.Interfaces;

public interface ICommitDetailsRepository
{
    void Add(CommitDetail commitDetail);

    void AddRange(IEnumerable<CommitDetail> commitDetails);
}

