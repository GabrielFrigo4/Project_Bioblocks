using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

public class RankingRepository : IRankingRepository
{
    private FirebaseFirestore _db   => FirebaseFirestore.DefaultInstance;
    private IAuthRepository   _auth => AppContext.Auth;

    // ─────────────────────────────────────────────────────────
    // IRankingRepository
    // ─────────────────────────────────────────────────────────
    public async Task<Ranking> GetCurrentUserRankingAsync()
    {
        if (!_auth.IsUserLoggedIn())
        {
            Debug.LogError("[RankingRepository] Usuário não autenticado.");
            return null;
        }

        string userId = _auth.CurrentUserId;

        try
        {
            DocumentSnapshot snap = await _db
                .Collection("Rankings")
                .Document(userId)
                .GetSnapshotAsync();

            if (!snap.Exists)
            {
                Debug.LogWarning($"[RankingRepository] Rankings/{userId} não encontrado.");
                return null;
            }

            Ranking ranking = snap.ConvertTo<Ranking>();
            ranking.UserId  = userId;
            return ranking;
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingRepository] GetCurrentUserRanking falhou: {e.Message}");
            throw;
        }
    }

    public async Task<List<Ranking>> GetRankingsAsync(int limit = 50)
    {
        try
        {
            QuerySnapshot snap = await _db
                .Collection("Rankings")
                .OrderByDescending("score")
                .Limit(limit)
                .GetSnapshotAsync();

            return ToRankingList(snap);
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingRepository] GetRankingsAsync falhou: {e.Message}");
            throw;
        }
    }

    public async Task<List<Ranking>> GetWeekRankingsAsync(int limit = 50)
    {
        try
        {
            QuerySnapshot snap = await _db
                .Collection("Rankings")
                .OrderByDescending("weekScore")
                .Limit(limit)
                .GetSnapshotAsync();

            return ToRankingList(snap);
        }
        catch (Exception e)
        {
            Debug.LogError($"[RankingRepository] GetWeekRankingsAsync falhou: {e.Message}");
            throw;
        }
    }

    // ─────────────────────────────────────────────────────────
    // Helper
    // ─────────────────────────────────────────────────────────
    private List<Ranking> ToRankingList(QuerySnapshot snap)
    {
        var result = new List<Ranking>(snap.Count);

        foreach (DocumentSnapshot doc in snap.Documents)
        {
            try
            {
                Ranking ranking = doc.ConvertTo<Ranking>();
                ranking.UserId  = doc.Id;   // chave do documento = userId
                result.Add(ranking);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[RankingRepository] Doc {doc.Id} inválido, ignorando: {e.Message}");
            }
        }

        return result;
    }
}