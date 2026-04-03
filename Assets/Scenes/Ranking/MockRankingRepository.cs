using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FakeRankingRepository : IRankingRepository
{
    private readonly List<Ranking> _mockRankings;
    private readonly Ranking       _mockCurrentUser;

    public FakeRankingRepository()
    {
        _mockRankings = new List<Ranking>
        {
            new Ranking("uid_1", "Asoka",            1000, 300, ""),
            new Ranking("uid_2", "Zico",              850, 210, ""),
            new Ranking("uid_3", "Naruto",            700, 180, ""),
            new Ranking("uid_4", "Yoda",              600, 150, ""),
            new Ranking("uid_5", "Captain Kirk",      500, 120, ""),
            new Ranking("uid_6", "Hermione",          480, 115, ""),
            new Ranking("uid_7", "Tony Stark",        460, 100, ""),
            new Ranking("uid_mock", "CurrentPlayer",  400,  90, ""),
        };

        _mockCurrentUser = new Ranking("uid_mock", "CurrentPlayer", 400, 90, "");
    }

    public Task<Ranking> GetCurrentUserRankingAsync()
        => Task.FromResult(_mockCurrentUser);

    public Task<List<Ranking>> GetRankingsAsync(int limit = 50)
        => Task.FromResult(_mockRankings
            .OrderByDescending(r => r.userScore)
            .Take(limit)
            .ToList());

    public Task<List<Ranking>> GetWeekRankingsAsync(int limit = 50)
        => Task.FromResult(_mockRankings
            .OrderByDescending(r => r.userWeekScore)
            .Take(limit)
            .ToList());
}
