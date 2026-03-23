using UnityEngine;
using System.Collections.Generic;

public class LocalRepositoryTest : MonoBehaviour
{
    void Start()
    {
        TestLocalRepository();
    }

    void TestLocalRepository()
    {
        var repo = new LocalRankingRepository(AppContext.LocalDatabase);
        
        var rankings = new List<RankingEntity>
        {
            new RankingEntity { UserId = "user1", UserName = "Alice", TotalScore = 1000, WeekScore = 500 },
            new RankingEntity { UserId = "user2", UserName = "Bob", TotalScore = 900, WeekScore = 600 },
            new RankingEntity { UserId = "user3", UserName = "Charlie", TotalScore = 800, WeekScore = 400 }
        };
        
        repo.SaveRankings(rankings);
        
        var allRankings = repo.GetAllRankings();
        Debug.Log($"✅ SQLTeste - Total rankings: {allRankings.Count}");
        
        var top20 = repo.GetTop20Rankings();
        Debug.Log($"✅ SQLTeste - Top 20: {top20.Count}");
        Debug.Log($"✅ SQLTeste - 1º lugar: {top20[0].UserName} - Week: {top20[0].WeekScore}");
        
        var userRanking = repo.GetRankingByUserId("user2");
        Debug.Log($"✅ SQLTeste - Bob encontrado: {userRanking?.UserName}");
        
        var position = repo.GetUserRankPosition("user2");
        Debug.Log($"✅ SQLTeste - Posição do Bob: {position}");
        
        repo.DeleteAllRankings();
    }
}