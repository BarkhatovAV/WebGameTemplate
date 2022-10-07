using System;
using System.Collections;
using Agava.YandexGames;
using Agava.VKGames;
using UnityEngine;

public class SDKInitializer : MonoBehaviour
{
    public event Action Initialized;

    private IEnumerator Start()
    {
#if UNITY_EDITOR
        yield break;
#endif

#if YANDEX_GAMES
        yield return YandexGamesSdk.Initialize(OnYandexSDKInitialize);
#endif

#if VK_GAMES
        yield return Agava.VKGames.VKGamesSdk.Initialize(OnVKSDKInitialize);
#endif
    }

    private void OnYandexSDKInitialize()
    {
        Initialized?.Invoke();
    }

    private void OnVKSDKInitialize()
    {
        Initialized?.Invoke();
    }
}
