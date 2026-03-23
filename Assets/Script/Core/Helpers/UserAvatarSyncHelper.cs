using UnityEngine;

/// <summary>
/// Helper est�tico para sincronizar mudan�as de avatar entre ProfileScene e UserTopBar
/// Segue o mesmo padr�o do projeto para manter consist�ncia
/// </summary>
public static class UserAvatarSyncHelper
{
    /// <summary>
    /// Notifica a UserTopBar que o avatar foi atualizado
    /// Deve ser chamado ap�s o upload bem-sucedido de uma nova imagem no ProfileImageManager
    /// </summary>

    public static void NotifyAvatarChanged(string newImageUrl, UserHeaderManager userHeader = null)
    {
        if (userHeader == null)
            userHeader = Object.FindFirstObjectByType<UserHeaderManager>();

        if (userHeader != null)
        {
            userHeader.UpdateAvatarFromUrl(newImageUrl);
            Debug.Log($"[AvatarSync] UserTopBar notificada: {newImageUrl}");
        }
        else
        {
            Debug.LogWarning("[AvatarSync] UserTopBarManager não disponível");
        }
    }

    public static void RefreshAvatar(UserHeaderManager userHeader = null)
    {
        if (userHeader == null)
            userHeader = Object.FindFirstObjectByType<UserHeaderManager>();

        if (userHeader != null)
        {
            userHeader.RefreshUserAvatar();
            Debug.Log("[AvatarSync] Avatar atualizado");
        }
        else
        {
            Debug.LogWarning("[AvatarSync] UserTopBarManager não disponível");
        }
    }
}