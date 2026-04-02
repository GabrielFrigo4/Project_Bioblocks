using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;

public class FirestoreRepository : MonoBehaviour, IFirestoreRepository
{
    private FirebaseFirestore db;
    private bool isInitialized;
    public bool IsInitialized => isInitialized;
    private ListenerRegistration _userDataListener;

    public void Initialize()
    {
        if (isInitialized) return;

        try
        {
            db = FirebaseFirestore.DefaultInstance;
            isInitialized = true;
            Debug.Log("Firestore initialized successfully");
        }
        catch (Exception e)
        {
            Debug.LogError($"Firestore initialization failed: {e.Message}");
            throw;
        }
    }

    public async Task<UserData> GetUserData(string userId)
    {
        try
        {
            DocumentSnapshot snapshot = await db.Collection("Users").Document(userId).GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                Debug.LogError($"Documento do usuário {userId} não encontrado");
                return null;
            }

            UserData user = ConvertFromFirestore(snapshot.ToDictionary());
            Debug.Log($"[FirestoreRepository] UserData carregado - NickName: {user.NickName}, Score: {user.Score}");
            return user;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao buscar dados do usuário: {ex.Message}");
            throw;
        }
    }

    private UserData ConvertFromFirestore(Dictionary<string, object> data)
    {
        UserData userData = new UserData();

        userData.UserId      = data.ContainsKey("UserId")   ? (string)data["UserId"]   : "";
        userData.NickName    = data.ContainsKey("NickName")  ? (string)data["NickName"] : "";
        userData.Name        = data.ContainsKey("Name")      ? (string)data["Name"]     : "";
        userData.Email       = data.ContainsKey("Email")     ? (string)data["Email"]    : "";
        userData.ProfileImageUrl = data.ContainsKey("ProfileImageUrl")
            ? (string)data["ProfileImageUrl"] ?? ""
            : "";
        userData.Score     = data.ContainsKey("Score")     ? Convert.ToInt32(data["Score"])     : 0;
        userData.WeekScore = data.ContainsKey("WeekScore") ? Convert.ToInt32(data["WeekScore"]) : 0;
        userData.QuestionTypeProgress = data.ContainsKey("QuestionTypeProgress")
            ? Convert.ToInt32(data["QuestionTypeProgress"])
            : (data.ContainsKey("Progress") ? Convert.ToInt32(data["Progress"]) : 0);
        userData.PlayerLevel = data.ContainsKey("PlayerLevel") ? Convert.ToInt32(data["PlayerLevel"]) : 1;
        userData.TotalValidQuestionsAnswered   = data.ContainsKey("TotalValidQuestionsAnswered")
            ? Convert.ToInt32(data["TotalValidQuestionsAnswered"]) : 0;
        userData.TotalQuestionsInAllDatabanks  = data.ContainsKey("TotalQuestionsInAllDatabanks")
            ? Convert.ToInt32(data["TotalQuestionsInAllDatabanks"]) : 0;
        userData.IsUserRegistered = data.ContainsKey("IsUserRegistered")
            ? Convert.ToBoolean(data["IsUserRegistered"]) : false;

        if (data.ContainsKey("CreatedTime") && data["CreatedTime"] is Timestamp timestamp)
            userData.CreatedTime = timestamp;
        else
            userData.CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow);

        if (data.ContainsKey("ResetDatabankFlags") && data["ResetDatabankFlags"] is Dictionary<string, object> resetFlagsData)
        {
            userData.ResetDatabankFlags = new Dictionary<string, bool>();
            foreach (var kvp in resetFlagsData)
                userData.ResetDatabankFlags[kvp.Key] = Convert.ToBoolean(kvp.Value);
        }

        userData.AnsweredQuestions = new Dictionary<string, List<int>>();
        if (data.ContainsKey("AnsweredQuestions") && data["AnsweredQuestions"] is Dictionary<string, object> answeredQuestionsData)
        {
            foreach (var kvp in answeredQuestionsData)
            {
                if (kvp.Value is IEnumerable<object> list)
                    userData.AnsweredQuestions[kvp.Key] = list.Select(x => Convert.ToInt32(x)).ToList();
            }
        }

        return userData;
    }

    public async Task CreateUserDocument(UserData userData)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");
            if (string.IsNullOrEmpty(userData.UserId))
                throw new ArgumentException("UserId não pode ser vazio");

            var requiredFields = new Dictionary<string, object>
            {
                { "UserId", userData.UserId },
                { "NickName", userData.NickName },
                { "Name", userData.Name },
                { "Email", userData.Email },
                { "Score", userData.Score },
                { "WeekScore", userData.WeekScore },
                { "QuestionTypeProgress", userData.QuestionTypeProgress },
                { "IsUserRegistered", userData.IsUserRegistered },
                { "CreatedTime", userData.CreatedTime },
                { "ProfileImageUrl", userData.ProfileImageUrl },
                { "AnsweredQuestions", new Dictionary<string, object>() }
            };

            DocumentReference docRef = db.Collection("Users").Document(userData.UserId);
            await docRef.SetAsync(requiredFields);
            
            await db.Collection("Nicknames")
                .Document(userData.NickName.ToLower())
                .SetAsync(new Dictionary<string, object> { { "userId", userData.UserId } });

            Debug.Log($"Documento do usuário criado com sucesso: {userData.UserId}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao criar documento do usuário: {e.Message}");
            throw;
        }
    }

    public async Task UpdateUserScore(string userId, int newScore, int questionNumber, string databankName, bool isCorrect)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");

            UserDataStore.UpdateScore(newScore);

            DocumentReference docRef = db.Collection("Users").Document(userId);

            await db.RunTransactionAsync(async transaction =>
            {
                DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(docRef);
                Dictionary<string, object> updates = new Dictionary<string, object> { { "Score", newScore } };

                if (snapshot.Exists)
                {
                    Dictionary<string, List<int>> answeredQuestions = snapshot.GetValue<Dictionary<string, List<int>>>("AnsweredQuestions");

                    if (answeredQuestions == null)
                        answeredQuestions = new Dictionary<string, List<int>>();

                    if (isCorrect)
                    {
                        if (!answeredQuestions.ContainsKey(databankName))
                            answeredQuestions[databankName] = new List<int>();
                        answeredQuestions[databankName].Add(questionNumber);
                    }

                    updates["AnsweredQuestions"] = answeredQuestions;
                }
                else
                {
                    Debug.LogError("User document not found during score update!");
                    return;
                }

                transaction.Update(docRef, updates);
            });

            Debug.Log($"Score atualizado para {newScore} e questionNumber {questionNumber} adicionado em {databankName}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao atualizar score: {e.Message}");
            throw;
        }
    }

    public async Task UpdateUserData(UserData userData)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");
            if (string.IsNullOrEmpty(userData.UserId))
                throw new ArgumentException("UserId não pode ser vazio");

            DocumentReference docRef = db.Collection("Users").Document(userData.UserId);
            Dictionary<string, object> userDataDict = userData.ToDictionary();
            await docRef.UpdateAsync(userDataDict);
            Debug.Log($"Dados do usuário {userData.UserId} atualizados com sucesso");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao atualizar dados do usuário: {e.Message}");
            throw;
        }
    }

    public void ListenToUserData(
    string userId,
    Action<int> onScoreChanged = null,
    Action<int> onWeekScoreChanged = null,
    Action<Dictionary<string, List<int>>> onAnsweredQuestionsChanged = null)
    {
        _userDataListener?.Stop();
        _userDataListener = db.Collection("Users").Document(userId)
            .Listen(snapshot =>
            {
                if (!snapshot.Exists) return;

                Dictionary<string, object> data = snapshot.ToDictionary();
                UserData currentUserData = UserDataStore.CurrentUserData;

                // Score
                if (data.ContainsKey("Score"))
                {
                    int newScore = Convert.ToInt32(data["Score"]);
                    UserDataStore.UpdateScore(newScore);
                    onScoreChanged?.Invoke(newScore);
                }

                // WeekScore
                if (data.ContainsKey("WeekScore"))
                {
                    int newWeekScore = Convert.ToInt32(data["WeekScore"]);
                    if (currentUserData != null)
                        UserDataStore.UpdateWeekScore(newWeekScore);
                    onWeekScoreChanged?.Invoke(newWeekScore);
                }

                // TotalValidQuestionsAnswered
                if (data.ContainsKey("TotalValidQuestionsAnswered") && currentUserData != null)
                {
                    int total = Convert.ToInt32(data["TotalValidQuestionsAnswered"]);
                    if (currentUserData.TotalValidQuestionsAnswered != total)
                    {
                        currentUserData.TotalValidQuestionsAnswered = total;
                        UserDataStore.UpdateTotalValidQuestionsAnswered(total);
                        Debug.Log($"[FirestoreRepository] TotalValidQuestionsAnswered atualizado: {total}");
                    }
                }

                // PlayerLevel
                if (data.ContainsKey("PlayerLevel") && currentUserData != null)
                {
                    int level = Convert.ToInt32(data["PlayerLevel"]);
                    if (currentUserData.PlayerLevel != level)
                    {
                        currentUserData.PlayerLevel = level;
                        UserDataStore.UpdatePlayerLevel(level);
                        Debug.Log($"[FirestoreRepository] PlayerLevel atualizado: {level}");
                    }
                }

                // AnsweredQuestions
                if (onAnsweredQuestionsChanged != null && data.ContainsKey("AnsweredQuestions"))
                {
                    try
                    {
                        Dictionary<string, List<int>> answeredQuestions = new Dictionary<string, List<int>>();
                        var answeredQuestionsData = data["AnsweredQuestions"] as Dictionary<string, object>;

                        if (answeredQuestionsData != null)
                        {
                            foreach (var kvp in answeredQuestionsData)
                            {
                                string databankName  = kvp.Key;
                                var    questionsList = kvp.Value as IEnumerable<object>;

                                if (questionsList != null)
                                {
                                    answeredQuestions[databankName] = questionsList
                                        .Select(q => Convert.ToInt32(q))
                                        .ToList();

                                    AnsweredQuestionsListStore.UpdateAnsweredQuestionsCount(
                                        userId,
                                        databankName,
                                        answeredQuestions[databankName].Count
                                    );
                                }
                            }

                            // Atualiza também o UserDataStore com as questões respondidas
                            if (currentUserData != null)
                                currentUserData.AnsweredQuestions = answeredQuestions;

                            onAnsweredQuestionsChanged.Invoke(answeredQuestions);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[FirestoreRepository] Erro ao processar AnsweredQuestions: {ex.Message}");
                    }
                }
            });

        Debug.Log($"[FirestoreRepository] Listener iniciado para userId: {userId}");
    }

    public void StopListening()
    {
        if (_userDataListener != null)
        {
            _userDataListener.Stop();
            _userDataListener = null;
            Debug.Log("[FirestoreRepository] Listener cancelado.");
        }
    }

    private void OnDestroy()
    {
        StopListening();
    }

    public async Task UpdateUserProgress(string userId, int progress)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");

            DocumentReference docRef = db.Collection("Users").Document(userId);
            await docRef.UpdateAsync(new Dictionary<string, object> { { "Progress", progress } });
            Debug.Log($"Progresso do usuário atualizado para {progress}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao atualizar progresso do usuário: {e.Message}");
            throw;
        }
    }

    public async Task ResetAnsweredQuestions(string userId, string databankName)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");

            DocumentReference docRef = db.Collection("Users").Document(userId);

            await db.RunTransactionAsync(async transaction =>
            {
                DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(docRef);

                if (snapshot.Exists)
                {
                    Dictionary<string, List<int>> answeredQuestions = snapshot.GetValue<Dictionary<string, List<int>>>("AnsweredQuestions");

                    if (answeredQuestions != null && answeredQuestions.ContainsKey(databankName))
                    {
                        answeredQuestions.Remove(databankName);
                        transaction.Update(docRef, "AnsweredQuestions", answeredQuestions);
                    }
                    else
                    {
                        Debug.LogWarning($"AnsweredQuestions para {databankName} não encontrada para o usuário {userId}");
                    }
                }
                else
                {
                    Debug.LogError($"Usuário {userId} não encontrado!");
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao remover AnsweredQuestions: {e.Message}");
            throw;
        }
    }

    public async Task DeleteDocument(string collection, string documentId)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");

            // Nota: após refatoração do AuthRepository, isso virá via IAuthRepository injetado
            var user = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
            if (user == null) throw new Exception("Usuário não está autenticado");

            string token = await user.TokenAsync(true);

            DocumentReference docRef = db.Collection(collection).Document(documentId);

            int maxRetries = 3;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    await docRef.DeleteAsync();
                    Debug.Log($"Documento {documentId} deletado com sucesso da coleção {collection}");
                    return;
                }
                catch (Exception e) when (i < maxRetries - 1)
                {
                    Debug.LogWarning($"Tentativa {i + 1} falhou: {e.Message}. Tentando novamente...");
                    await Task.Delay(1000);
                    token = await user.TokenAsync(true);
                }
            }

            throw new Exception($"Falha ao deletar documento após {maxRetries} tentativas");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao deletar documento: {e.Message}");
            throw;
        }
    }

    public async Task<bool> AreNicknameTaken(string nickName)
    {
        DocumentSnapshot snapshot = await db.Collection("Nicknames")
            .Document(nickName.ToLower())
            .GetSnapshotAsync();
        return snapshot.Exists;
    }

    public async Task UpdateUserProfileImageUrl(string userId, string imageUrl)
    {
        try
        {
            DocumentReference userRef = db.Collection("Users").Document(userId);
            await userRef.UpdateAsync(new Dictionary<string, object> { { "ProfileImageUrl", imageUrl } });
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao atualizar URL da imagem de perfil: {ex.Message}");
            throw;
        }
    }

    public async Task<List<UserData>> GetAllUsersData()
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");

            QuerySnapshot querySnapshot = await db.Collection("Users").GetSnapshotAsync();
            List<UserData> users = new List<UserData>();

            foreach (DocumentSnapshot doc in querySnapshot.Documents)
            {
                users.Add(ConvertFromFirestore(doc.ToDictionary()));
            }

            return users;
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao buscar todos os usuários: {e.Message}");
            throw;
        }
    }

    public async Task UpdateUserWeekScore(string userId, int additionalScore)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");

            DocumentReference docRef = db.Collection("Users").Document(userId);

            await db.RunTransactionAsync(async transaction =>
            {
                DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(docRef);

                if (snapshot.Exists)
                {
                    int currentWeekScore = snapshot.ContainsField("WeekScore")
                        ? Convert.ToInt32(snapshot.GetValue<object>("WeekScore"))
                        : 0;

                    int newWeekScore = currentWeekScore + additionalScore;
                    transaction.Update(docRef, "WeekScore", newWeekScore);

                    if (UserDataStore.CurrentUserData != null && UserDataStore.CurrentUserData.UserId == userId)
                        UserDataStore.UpdateWeekScore(newWeekScore);
                }
                else
                {
                    Debug.LogError("User document not found during week score update!");
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao atualizar score semanal: {e.Message}");
            throw;
        }
    }

   // FirestoreRepository.cs

    public async Task UpdateUserScores(string userId, int additionalScore, int questionNumber, string databankName, bool isCorrect)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");

            DocumentReference docRef = db.Collection("Users").Document(userId);

            await db.RunTransactionAsync(async transaction =>
            {
                DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(docRef);

                if (!snapshot.Exists)
                {
                    Debug.LogError("[FirestoreRepository] Usuário não encontrado na transação de score.");
                    return;
                }

                int currentScore = snapshot.ContainsField("Score")
                    ? Convert.ToInt32(snapshot.GetValue<object>("Score")) : 0;
                int currentWeekScore = snapshot.ContainsField("WeekScore")
                    ? Convert.ToInt32(snapshot.GetValue<object>("WeekScore")) : 0;

                int newScore = currentScore + additionalScore;
                int newWeekScore = currentWeekScore + additionalScore;

                // Score nunca fica negativo
                if (newScore < 0) newScore = 0;
                if (newWeekScore < 0) newWeekScore = 0;

                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "Score",     newScore },
                    { "WeekScore", newWeekScore }
                };

                // Adiciona questão respondida corretamente
                if (isCorrect && !string.IsNullOrEmpty(databankName) && questionNumber > 0)
                {
                    var answeredQuestions = snapshot.ContainsField("AnsweredQuestions")
                        ? snapshot.GetValue<Dictionary<string, List<int>>>("AnsweredQuestions")
                        : new Dictionary<string, List<int>>();

                    answeredQuestions ??= new Dictionary<string, List<int>>();

                    if (!answeredQuestions.ContainsKey(databankName))
                        answeredQuestions[databankName] = new List<int>();

                    // Evita duplicatas
                    if (!answeredQuestions[databankName].Contains(questionNumber))
                    {
                        answeredQuestions[databankName].Add(questionNumber);
                        updates["AnsweredQuestions"] = answeredQuestions;
                    }
                }

                transaction.Update(docRef, updates);

                if (UserDataStore.CurrentUserData != null)
                {
                    UserDataStore.CurrentUserData.Score = newScore;
                    UserDataStore.CurrentUserData.WeekScore = newWeekScore;
                }
            });

            if (UserDataStore.CurrentUserData != null)
            {
                UserDataStore.UpdateScore(UserDataStore.CurrentUserData.Score);
            }

            Debug.Log($"[FirestoreRepository] UpdateUserScores concluído via transação. Score: {UserDataStore.CurrentUserData?.Score}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[FirestoreRepository] Erro em UpdateUserScores: {e.Message}");
            throw;
        }
    }

    public async Task EnsureWeekScoreField()
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");

            QuerySnapshot querySnapshot = await db.Collection("Users").GetSnapshotAsync();
            WriteBatch batch = db.StartBatch();
            int userCount = 0;

            foreach (DocumentSnapshot doc in querySnapshot.Documents)
            {
                if (!doc.ContainsField("WeekScore"))
                {
                    batch.Update(doc.Reference, "WeekScore", 0);
                    userCount++;

                    if (userCount >= 450)
                    {
                        await batch.CommitAsync();
                        batch = db.StartBatch();
                        userCount = 0;
                    }
                }
            }

            if (userCount > 0)
                await batch.CommitAsync();

            Debug.Log("Verificação de WeekScore concluída");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao verificar campo WeekScore: {e.Message}");
            throw;
        }
    }

    public async Task UpdateUserField(string userId, string fieldName, object value)
    {
        try
        {
            if (!isInitialized) throw new Exception("Firestore não inicializado");

            DocumentReference docRef = db.Collection("Users").Document(userId);
            await docRef.UpdateAsync(new Dictionary<string, object> { { fieldName, value } });
            Debug.Log($"{fieldName} atualizado para {value}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao atualizar {fieldName}: {e.Message}");
            throw;
        }
    }
}