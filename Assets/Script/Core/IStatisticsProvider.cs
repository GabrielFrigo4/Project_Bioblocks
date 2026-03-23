/// <summary>
/// Contrato para acesso às estatísticas dos bancos de questões.
/// Usado pelo PlayerLevelManager para obter o total de questões
/// sem depender diretamente do DatabaseStatisticsManager.
/// </summary>
public interface IStatisticsProvider
{
    bool IsInitialized { get; }

    int GetTotalQuestionsCount();
}
