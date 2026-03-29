using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAnsweredQuestionsManager
{
    bool IsManagerInitialized { get; }

    Task ForceUpdate();
    Task<List<string>> FetchUserAnsweredQuestionsInTargetDatabase(string target);
    Task MarkQuestionAsAnswered(string databankName, int questionNumber);
    Task<bool> HasRemainingQuestions(string currentDatabase, List<string> currentQuestionList);
    void ResetManager();
}
