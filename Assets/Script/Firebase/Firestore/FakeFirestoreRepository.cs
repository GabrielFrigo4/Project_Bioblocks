using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;

/// <summary>
/// Implementação fake do IFirestoreRepository para uso em testes e desenvolvimento.
/// Não faz nenhuma chamada real ao Firebase — opera inteiramente em memória.
/// 
/// Como usar em testes:
///   var fakeFirestore = new FakeFirestoreRepository();
///   fakeFirestore.SetFakeUser(new UserData { UserId = "test-user", Score = 100 });
///   var manager = new PlayerLevelManager();
///   manager.Initialize(fakeFirestore);
/// </summary>
public class FakeFirestoreRepository : IFirestoreRepository
{
    // -------------------------------------------------------
    // Estado interno — configure antes de rodar o teste
    // -------------------------------------------------------
    private readonly Dictionary<string, UserData> _users = new();
    private readonly List<UserData> _allUsers = new();

    // Contadores para verificar chamadas em testes
    public int UpdateUserFieldCallCount { get; private set; }
    public int UpdateUserScoresCallCount { get; private set; }
    public string LastUpdatedField { get; private set; }
    public object LastUpdatedValue { get; private set; }

    // -------------------------------------------------------
    // Configuração do fake
    // -------------------------------------------------------

    public void SetFakeUser(UserData user)
    {
        _users[user.UserId] = user;
        if (!_allUsers.Exists(u => u.UserId == user.UserId))
            _allUsers.Add(user);
    }

    // -------------------------------------------------------
    // IFirestoreRepository
    // -------------------------------------------------------

    public bool IsInitialized => true;

    public void Initialize() { /* Nada a fazer no fake */ }

    public Task<UserData> GetUserData(string userId)
    {
        _users.TryGetValue(userId, out var user);
        return Task.FromResult(user);
    }

    public Task CreateUserDocument(UserData userData)
    {
        _users[userData.UserId] = userData;
        return Task.CompletedTask;
    }

    public Task UpdateUserData(UserData userData)
    {
        _users[userData.UserId] = userData;
        return Task.CompletedTask;
    }

    public Task UpdateUserScore(string userId, int newScore, int questionNumber, string databankName, bool isCorrect)
    {
        if (_users.TryGetValue(userId, out var user))
            user.Score = newScore;
        return Task.CompletedTask;
    }

    public Task UpdateUserScores(string userId, int additionalScore, int questionNumber, string databankName, bool isCorrect)
    {
        UpdateUserScoresCallCount++;
        if (_users.TryGetValue(userId, out var user))
        {
            user.Score += additionalScore;
            user.WeekScore += additionalScore;
        }
        return Task.CompletedTask;
    }

    public Task UpdateUserWeekScore(string userId, int additionalScore)
    {
        if (_users.TryGetValue(userId, out var user))
            user.WeekScore += additionalScore;
        return Task.CompletedTask;
    }

    public Task UpdateUserField(string userId, string fieldName, object value)
    {
        UpdateUserFieldCallCount++;
        LastUpdatedField = fieldName;
        LastUpdatedValue = value;

        if (_users.TryGetValue(userId, out var user))
        {
            // Atualiza o campo correspondente no objeto em memória
            switch (fieldName)
            {
                case "PlayerLevel":                   user.PlayerLevel = Convert.ToInt32(value); break;
                case "TotalValidQuestionsAnswered":   user.TotalValidQuestionsAnswered = Convert.ToInt32(value); break;
                case "TotalQuestionsInAllDatabanks":  user.TotalQuestionsInAllDatabanks = Convert.ToInt32(value); break;
                case "Score":                         user.Score = Convert.ToInt32(value); break;
                case "WeekScore":                     user.WeekScore = Convert.ToInt32(value); break;
                case "ProfileImageUrl":               user.ProfileImageUrl = value?.ToString(); break;
            }
        }
        return Task.CompletedTask;
    }

    public Task UpdateUserProgress(string userId, int progress)
    {
        if (_users.TryGetValue(userId, out var user))
            user.QuestionTypeProgress = progress;
        return Task.CompletedTask;
    }

    public Task UpdateUserProfileImageUrl(string userId, string imageUrl)
    {
        if (_users.TryGetValue(userId, out var user))
            user.ProfileImageUrl = imageUrl;
        return Task.CompletedTask;
    }

    public Task ResetAnsweredQuestions(string userId, string databankName)
    {
        if (_users.TryGetValue(userId, out var user))
            user.AnsweredQuestions?.Remove(databankName);
        return Task.CompletedTask;
    }

    public Task ResetAllWeeklyScores()
    {
        foreach (var user in _users.Values)
            user.WeekScore = 0;
        return Task.CompletedTask;
    }

    public Task EnsureWeekScoreField() => Task.CompletedTask;

    public Task DeleteDocument(string collection, string documentId)
    {
        _users.Remove(documentId);
        return Task.CompletedTask;
    }

    public Task<bool> AreNicknameTaken(string nickName)
    {
        bool taken = _allUsers.Exists(u => u.NickName == nickName);
        return Task.FromResult(taken);
    }

    public Task<List<UserData>> GetAllUsersData()
    {
        return Task.FromResult(new List<UserData>(_allUsers));
    }

    public void ListenToUserData(
        string userId,
        Action<int> onScoreChanged = null,
        Action<int> onWeekScoreChanged = null,
        Action<Dictionary<string, List<int>>> onAnsweredQuestionsChanged = null)
    {
        // No fake, dispara os callbacks imediatamente com os dados em memória
        if (!_users.TryGetValue(userId, out var user)) return;

        onScoreChanged?.Invoke(user.Score);
        onWeekScoreChanged?.Invoke(user.WeekScore);
        onAnsweredQuestionsChanged?.Invoke(user.AnsweredQuestions);
    }

    public void StopListening()
    {
        // Em testes, não faz nada
    }
}