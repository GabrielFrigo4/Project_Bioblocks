using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Firestore;

public class UserFeedbackManager : MonoBehaviour
{
    private INavigationService _navigation;
    [Header("Configuração")]
    [SerializeField] private Transform questionsContainer;
    [SerializeField] private CanvasGroup questionsCanvasGroup;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;

    [Header("Loading Spinner Configuration")]
    [SerializeField] private float spinnerRotationSpeed = 100f;
    [SerializeField] private Image loadingSpinner;
    [SerializeField] private GameObject logoLoading;

    [Header("Prefabs")]
    [SerializeField] private GameObject textQuestionPrefab;
    [SerializeField] private GameObject ratingQuestionPrefab;
    [SerializeField] private GameObject toggleQuestionPrefab;

    [Header("Navegação")]
    [SerializeField] private int questionsPerPage = 3;
    [SerializeField] private bool groupByCategory = true;

    [System.Serializable] public class QuestionChangedEvent : UnityEvent<int, FeedbackQuestion> { }
    [System.Serializable] public class QuestionsLoadedEvent : UnityEvent<List<FeedbackQuestion>> { }
    public QuestionChangedEvent OnQuestionChanged = new QuestionChangedEvent();
    public QuestionsLoadedEvent OnQuestionsLoaded = new QuestionsLoadedEvent();

    private IFeedbackDatabase currentDatabase;
    private Dictionary<string, object> feedbackResults = new Dictionary<string, object>();
    private List<IFeedbackQuestionController> questionControllers = new List<IFeedbackQuestionController>();
    private int currentPageIndex = 0;
    private List<FeedbackQuestion> allQuestions = new List<FeedbackQuestion>();

    public FeedbackQuestion GetQuestionAtIndex(int index)
    {
        if (index >= 0 && index < allQuestions.Count)
        {
            return allQuestions[index];
        }
        return null;
    }

    private void Start()
    {
        _navigation = AppContext.Navigation;
        logoLoading.SetActive(false);

        if (questionsContainer == null)
        {
            Debug.LogError("QuestionsContainer não foi atribuído no Inspector!");
            return;
        }

        if (questionsCanvasGroup == null)
        {
            Debug.LogError("QuestionsCanvasGroup não foi atribuído no Inspector!");
            return;
        }

        if (nextButton == null || prevButton == null || submitButton == null || backButton == null)
        {
            Debug.LogError("Um ou mais botões de navegação não foram atribuídos no Inspector!");
            return;
        }

        currentDatabase = FindFirstObjectByType<UserFeedbackQuestionsDatabase>();
        if (currentDatabase == null)
        {
            Debug.LogError("Nenhum banco de dados de feedback encontrado na cena!");
            return;
        }

        allQuestions = currentDatabase.GetQuestions();
        if (allQuestions == null || allQuestions.Count == 0)
        {
            Debug.LogError("Nenhuma questão encontrada no banco de dados!");
            return;
        }

        backButton.onClick.AddListener(OnBackButtonClick);

        InstantiateQuestions();
        UpdateNavigationButtons();
        OnQuestionsLoaded?.Invoke(allQuestions);
    }

    private void Update()
    {
        if (loadingSpinner != null && logoLoading.activeSelf)
        {
            loadingSpinner.transform.Rotate(0f, 0f, -spinnerRotationSpeed * Time.deltaTime);
        }
    }

    private void ShowLoading(bool show)
    {
        if (logoLoading != null)
            logoLoading.SetActive(show);

        if (questionsCanvasGroup != null)
        {
            questionsCanvasGroup.alpha = show ? 0f : 1f;
            questionsCanvasGroup.interactable = !show;
            questionsCanvasGroup.blocksRaycasts = !show;
        }

        if (nextButton != null)
            nextButton.interactable = !show;
        if (prevButton != null)
            prevButton.interactable = !show;
        if (submitButton != null)
            submitButton.interactable = !show;
        if (backButton != null)
            backButton.interactable = !show;
    }

    private void InstantiateQuestions()
    {
        foreach (Transform child in questionsContainer)
        {
            Destroy(child.gameObject);
        }
        questionControllers.Clear();

        List<FeedbackQuestion> sortedQuestions = allQuestions;
        if (groupByCategory)
        {
            sortedQuestions.Sort((a, b) => string.Compare(a.category, b.category));
        }

        foreach (var question in sortedQuestions)
        {
            GameObject prefab = null;

            switch (question.feedbackAnswerType)
            {
                case FeedbackAnswerType.Text:
                    prefab = textQuestionPrefab;
                    break;
                case FeedbackAnswerType.Rating:
                    prefab = ratingQuestionPrefab;
                    break;
                case FeedbackAnswerType.Toggle:
                    prefab = toggleQuestionPrefab;
                    break;
            }

            if (prefab != null)
            {
                GameObject questionObj = Instantiate(prefab, questionsContainer);
                IFeedbackQuestionController controller = questionObj.GetComponent<IFeedbackQuestionController>();
                if (controller != null)
                {
                    controller.SetupQuestion(question);
                    questionControllers.Add(controller);
                }
            }
        }

        UpdateQuestionsVisibility();
        if (allQuestions != null && allQuestions.Count > 0)
        {
            OnQuestionChanged?.Invoke(0, allQuestions[0]);
        }
    }

    private void UpdateQuestionsVisibility()
    {
        int startIndex = currentPageIndex * questionsPerPage;

        for (int i = 0; i < questionControllers.Count; i++)
        {
            bool isVisible = (i >= startIndex && i < startIndex + questionsPerPage);
            questionControllers[i].SetVisible(isVisible);
        }
    }

    private void UpdateNavigationButtons()
    {
        if (prevButton != null)
            prevButton.gameObject.SetActive(currentPageIndex > 0);

        bool isLastPage = (currentPageIndex + 1) * questionsPerPage >= questionControllers.Count;

        if (nextButton != null)
            nextButton.gameObject.SetActive(!isLastPage);

        if (submitButton != null)
            submitButton.gameObject.SetActive(isLastPage);

        if (backButton != null)
            backButton.gameObject.SetActive(!isLastPage);
    }

    public void NextPage()
    {
        if (ValidateCurrentPage())
        {
            CollectCurrentPageAnswers();
            currentPageIndex++;

            // Verifica se há controladores antes de atualizar
            if (questionControllers != null && questionControllers.Count > 0)
            {
                UpdateQuestionsVisibility();
                UpdateNavigationButtons();

                int startIndex = currentPageIndex * questionsPerPage;
                if (startIndex < questionControllers.Count && allQuestions != null && startIndex < allQuestions.Count)
                {
                    var currentQuestion = allQuestions[startIndex];
                    OnQuestionChanged?.Invoke(startIndex, currentQuestion);
                }
            }
            else
            {
                Debug.LogWarning("Não há questões para avançar");
            }
        }
    }

    public void PreviousPage()
    {
        currentPageIndex--;
        UpdateQuestionsVisibility();
        UpdateNavigationButtons();

        int startIndex = currentPageIndex * questionsPerPage;
        if (startIndex >= 0 && startIndex < allQuestions.Count)
        {
            var currentQuestion = allQuestions[startIndex];
            OnQuestionChanged?.Invoke(startIndex, currentQuestion);
        }
    }

    private bool ValidateCurrentPage()
    {
        int startIndex = currentPageIndex * questionsPerPage;
        int endIndex = Mathf.Min(startIndex + questionsPerPage, questionControllers.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            if (!questionControllers[i].Validate())
            {
                return false;
            }
        }

        return true;
    }

    private void CollectCurrentPageAnswers()
    {
        int startIndex = currentPageIndex * questionsPerPage;
        int endIndex = Mathf.Min(startIndex + questionsPerPage, questionControllers.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            var result = questionControllers[i].GetResult();
            feedbackResults[result.Key] = result.Value;
        }
    }

    public void OnSubmitButtonClick()
    {
        if (submitButton != null)
            submitButton.interactable = false;

        _ = HandleSubmitFeedback();
    }

    private async Task HandleSubmitFeedback()
    {
        ShowLoading(true);

        try
        {
            if (UserDataStore.CurrentUserData == null)
            {
                Debug.LogError("UserData não inicializado!");
                ShowErrorScreen();
                return;
            }

            bool success = await SubmitFeedback();

            if (success)
            {
                Debug.Log("Feedback submetido com sucesso");
                _navigation.NavigateTo("ProfileScene");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro durante o envio do feedback: {e.Message}");
            ShowErrorScreen();
        }
        finally
        {
            ShowLoading(false);
        }
    }

    public async Task<bool> SubmitFeedback()
    {
        if (!ValidateCurrentPage())
            return false;

        string userId = UserDataStore.CurrentUserData.UserId;

        try
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            DocumentReference userFeedbackDoc = db.Collection("UserFeedback").Document(userId);
            DocumentSnapshot docSnapshot = await userFeedbackDoc.GetSnapshotAsync();

            if (docSnapshot.Exists)
            {
                CollectionReference historyCollection = userFeedbackDoc.Collection("history");
                Dictionary<string, object> currentData = docSnapshot.ToDictionary();
                currentData["isLatest"] = false;
                await historyCollection.AddAsync(currentData);
            }

            CollectCurrentPageAnswers();
            feedbackResults["submissionDate"] = DateTime.UtcNow;
            feedbackResults["databaseName"] = currentDatabase.GetDatabaseName();
            feedbackResults["userId"] = userId;
            feedbackResults["isLatest"] = true;
            feedbackResults["appVersion"] = Application.version;
            feedbackResults["userDaysActive"] = CalculateDaysActive();

            await userFeedbackDoc.SetAsync(feedbackResults);

            Debug.Log($"UserFeedback enviado com sucesso para o usuário: {userId}");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao enviar UserFeedback: {e.Message}");
            ShowErrorScreen();
            return false;
        }
    }

    private int CalculateDaysActive()
    {
        try
        {
            DateTime createdDate = UserDataStore.CurrentUserData.GetCreatedDateTime();
            TimeSpan timeUsing = DateTime.UtcNow - createdDate;
            return Mathf.Max(1, (int)timeUsing.TotalDays);
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao calcular dias ativos: {e.Message}");
            return 1;
        }
    }

    public void OnBackButtonClick()
    {
        ClearAllAnswers();
        _navigation.NavigateTo("ProfileScene");
    }

    private void ClearAllAnswers()
    {
        feedbackResults.Clear();

        foreach (var controller in questionControllers)
        {
            controller.ClearAnswer();
        }
    }

    private void ShowErrorScreen()
    {
        // Implementar lógica para mostrar erro
    }
}


