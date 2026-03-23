using System;
using System.Collections.Generic;

/// <summary>
/// Contrato para navegação entre cenas.
/// Não expõe nenhum tipo do Unity SceneManager diretamente.
/// </summary>
public interface INavigationService
{
    /// <summary>Disparado quando uma cena começa a carregar.</summary>
    event Action<string> OnSceneChanged;

    /// <summary>Disparado quando a cena terminou de carregar.</summary>
    event Action<string> OnNavigationComplete;

    /// <summary>Navega para a cena informada, opcionalmente passando dados.</summary>
    void NavigateTo(string sceneName, Dictionary<string, object> sceneData = null);

    /// <summary>Chamado por botões de navegação — resolve o nome do botão para o nome da cena.</summary>
    void OnNavigationButtonClicked(string buttonName);

    /// <summary>Registra um mapeamento extra de botão → cena.</summary>
    void AddButtonSceneMapping(string buttonName, string sceneName);
}