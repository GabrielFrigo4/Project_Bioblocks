using System;
using System.Threading.Tasks;

public interface IPlayerLevelService
{
    // Incrementa o contador de questões válidas respondidas
    Task IncrementTotalAnswered();

    // Verifica se houve level-up após o incremento
    Task CheckAndHandleLevelUp();

    // Recalcula o total a partir do Firebase (usado na migração)
    Task RecalculateTotalAnswered();

    // Getters de estado atual
    int GetCurrentLevel();
    int GetTotalValidAnswered();
    int GetTotalQuestionsInAllDatabanks();
    float GetProgressInCurrentLevel();
    int GetQuestionsUntilNextLevel();

    // Notificado quando o usuário sobe de nível
    // oldLevel → newLevel
    event Action<int, int> OnLevelChanged;

    // Notificado quando o progresso dentro do nível atual muda
    event Action<int> OnLevelProgressUpdated;
}
