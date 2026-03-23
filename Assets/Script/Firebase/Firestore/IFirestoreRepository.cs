using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFirestoreRepository
{
    bool IsInitialized { get; }

    void Initialize();

    Task<UserData> GetUserData(string userId);

    Task CreateUserDocument(UserData userData);

    Task UpdateUserData(UserData userData);

    Task UpdateUserScore(string userId, int newScore, int questionNumber, string databankName, bool isCorrect);

    Task UpdateUserScores(string userId, int additionalScore, int questionNumber, string databankName, bool isCorrect);

    Task UpdateUserWeekScore(string userId, int additionalScore);

    Task UpdateUserField(string userId, string fieldName, object value);

    Task UpdateUserProgress(string userId, int progress);

    Task UpdateUserProfileImageUrl(string userId, string imageUrl);

    Task ResetAnsweredQuestions(string userId, string databankName);

    Task ResetAllWeeklyScores();

    Task EnsureWeekScoreField();

    Task DeleteDocument(string collection, string documentId);

    Task<bool> AreNicknameTaken(string nickName);

    Task<List<UserData>> GetAllUsersData();

    void ListenToUserData(
        string userId,
        Action<int> onScoreChanged = null,
        Action<int> onWeekScoreChanged = null,
        Action<Dictionary<string, List<int>>> onAnsweredQuestionsChanged = null
    );
}