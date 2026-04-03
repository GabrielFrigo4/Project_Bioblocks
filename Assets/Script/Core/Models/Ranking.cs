using Firebase.Firestore;

[System.Serializable]
[FirestoreData]
public class Ranking
{
   public string UserId { get; set; }

    [FirestoreProperty("nickName")]
    public string userName { get; set; }

    [FirestoreProperty("score")]
    public int userScore { get; set; }

    [FirestoreProperty("weekScore")]
    public int userWeekScore { get; set; }

    [FirestoreProperty("profileImageUrl")]
    public string profileImageUrl { get; set; }

    // Construtor vazio obrigatório para o FirestoreData desserializar
    public Ranking() { }

    public Ranking(string userId, string userName, int userScore, int userWeekScore, string profileImageUrl)
    {
        UserId = userId;
        this.userName = userName;
        this.userScore = userScore;
        this.userWeekScore = userWeekScore;
        this.profileImageUrl = profileImageUrl;
    }

    // Mantidos para compatibilidade com código existente
    public Ranking(string userName, int userScore, string profileImageUrl)
        : this("", userName, userScore, 0, profileImageUrl) { }

    public Ranking(string userName, int userScore, int userWeekScore, string profileImageUrl)
        : this("", userName, userScore, userWeekScore, profileImageUrl) { }
}