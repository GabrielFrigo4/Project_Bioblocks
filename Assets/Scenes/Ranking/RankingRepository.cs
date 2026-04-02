using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class RankingRepository : IRankingRepository
{
    private IAuthRepository _auth      => AppContext.Auth;
    private IFirestoreRepository _firestore => AppContext.Firestore;

    public async Task<UserData> GetCurrentUserDataAsync()
    {
        if (!AppContext.Auth.IsUserLoggedIn())
        {
            Debug.LogError("Usuário não está autenticado");
            return null;
        }

        string userId = AppContext.Auth.CurrentUserId;
        return await AppContext.Firestore.GetUserData(userId);
        
    }

    public async Task<List<Ranking>> GetRankingsAsync()
    {
        try
        {
            var usersData = await GetAllUsersData();

            List<Ranking> rankings = usersData.Select(userData => new Ranking(
                userData.NickName,
                userData.Score,
                userData.WeekScore,
                userData.ProfileImageUrl ?? ""
            )).ToList();

            // Log para depuração
            Debug.Log($"GetRankingsAsync - Amostra de rankings:");
            for (int i = 0; i < Math.Min(3, rankings.Count); i++)
            {
                Debug.Log($"Usuário: {rankings[i].userName}, Score: {rankings[i].userScore}, WeekScore: {rankings[i].userWeekScore}");
            }

            return rankings;
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao buscar rankings: {e.Message}");
            throw;
        }
    }

    public async Task<List<Ranking>> GetWeekRankingsAsync()
    {
        try
        {
            var usersData = await GetAllUsersData();

            List<Ranking> rankings = usersData.Select(userData => new Ranking(
                userData.NickName,
                userData.Score,
                userData.WeekScore,
                userData.ProfileImageUrl ?? ""
            )).ToList();

            return rankings;
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao buscar rankings semanais: {e.Message}");
            throw;
        }
    }

    public async Task<List<UserData>> GetAllUsersData()
    {
        try
        {
            return await AppContext.Firestore.GetAllUsersData();
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao buscar dados dos usuários: {e.Message}");
            throw;
        }
    }

    public async Task UpdateUserWeekScoreAsync(string userId, int additionalScore)
    {
        try
        {
            await AppContext.Firestore.UpdateUserWeekScore(userId, additionalScore);
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao atualizar WeekScore: {e}");
            throw;
        }
    }
}