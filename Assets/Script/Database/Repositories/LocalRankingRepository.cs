using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocalRankingRepository : ILocalRankingRepository
{
    private SQLite4Unity3d.SQLiteConnection _db;
    private IDatabaseManager _databaseManager;

    public LocalRankingRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
        _db = databaseManager.GetConnection();
    }

    public void SaveRankings(List<RankingEntity> rankings)
    {
        try
        {
            _databaseManager.ExecuteInTransaction(() =>
            {
                _db.DeleteAll<RankingEntity>();
                
                foreach (var ranking in rankings)
                {
                    ranking.LastUpdated = DateTime.UtcNow;
                    ranking.IsSynced = true;
                    _db.Insert(ranking);
                }
            });

            Debug.Log($"[LocalRankingRepository] Saved {rankings.Count} rankings");
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalRankingRepository] Error saving rankings: {e.Message}");
            throw;
        }
    }

    public List<RankingEntity> GetAllRankings()
    {
        try
        {
            return _db.Table<RankingEntity>()
                     .OrderByDescending(r => r.TotalScore)
                     .ThenByDescending(r => r.WeekScore)
                     .ToList();
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalRankingRepository] Error getting all rankings: {e.Message}");
            return new List<RankingEntity>();
        }
    }

    public List<RankingEntity> GetTop20Rankings()
    {
        try
        {
            return _db.Table<RankingEntity>()
                     .OrderByDescending(r => r.TotalScore)
                     .ThenByDescending(r => r.WeekScore)
                     .Take(20)
                     .ToList();
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalRankingRepository] Error getting top 20 rankings: {e.Message}");
            return new List<RankingEntity>();
        }
    }

    public RankingEntity GetRankingByUserId(string userId)
    {
        try
        {
            return _db.Table<RankingEntity>()
                     .Where(r => r.UserId == userId)
                     .FirstOrDefault();
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalRankingRepository] Error getting ranking for user {userId}: {e.Message}");
            return null;
        }
    }

    public int GetUserRankPosition(string userId)
    {
        try
        {
            var allRankings = GetAllRankings();
            var position = allRankings.FindIndex(r => r.UserId == userId);
            return position + 1;
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalRankingRepository] Error getting rank position for user {userId}: {e.Message}");
            return -1;
        }
    }

    public void UpsertRanking(RankingEntity ranking)
    {
        try
        {
            var existing = GetRankingByUserId(ranking.UserId);
            
            ranking.LastUpdated = DateTime.UtcNow;
            
            if (existing != null)
            {
                ranking.Id = existing.Id;
                _db.Update(ranking);
                Debug.Log($"[LocalRankingRepository] Updated ranking for user {ranking.UserName}");
            }
            else
            {
                _db.Insert(ranking);
                Debug.Log($"[LocalRankingRepository] Inserted new ranking for user {ranking.UserName}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalRankingRepository] Error upserting ranking: {e.Message}");
            throw;
        }
    }

    public void DeleteAllRankings()
    {
        try
        {
            _db.DeleteAll<RankingEntity>();
            Debug.Log("[LocalRankingRepository] All rankings deleted");
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalRankingRepository] Error deleting rankings: {e.Message}");
            throw;
        }
    }

    public int GetRankingsCount()
    {
        try
        {
            return _db.Table<RankingEntity>().Count();
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalRankingRepository] Error getting rankings count: {e.Message}");
            return 0;
        }
    }
}