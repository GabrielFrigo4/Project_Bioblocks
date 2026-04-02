using System;
using System.Threading.Tasks;

public interface IPlayerLevelService
{
    Task IncrementTotalAnswered();
    Task CheckAndHandleLevelUp();
    Task RecalculateTotalAnswered();

    int GetCurrentLevel();
    int GetTotalValidAnswered();
    int GetTotalQuestionsInAllDatabanks();
    float GetProgressInCurrentLevel();
    int GetQuestionsUntilNextLevel();
    int GetQuestionsAtLevelStart();

    event Action<int, int> OnLevelChanged;
    event Action<int> OnLevelProgressUpdated;
}
