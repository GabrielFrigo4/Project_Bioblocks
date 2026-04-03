using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRankingRepository
{
    Task<Ranking> GetCurrentUserRankingAsync();
    Task<List<Ranking>> GetRankingsAsync(int limit = 50);
    Task<List<Ranking>> GetWeekRankingsAsync(int limit = 50);
}