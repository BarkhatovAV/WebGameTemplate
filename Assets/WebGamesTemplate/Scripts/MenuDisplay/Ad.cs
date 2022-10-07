using System;
using Agava.VKGames;
using UnityEngine;
using Agava.YandexGames;

public class Ad : MonoBehaviour
{
    [SerializeField] private SDKInitializer _sdkInitializer;

    private bool _isInitialize = false;

    public event Action Rewarded;
    public event Action VideoOpened;
    public event Action VideoClosed;

    private void OnEnable() => _sdkInitializer.Initialized += OnInitialized;

    private void OnDisable() => _sdkInitializer.Initialized -= OnInitialized;

    public void InterestialAdShow()
    {
        if (!_isInitialize)
            return;

#if YANDEX_GAMES
        InterstitialAd.Show();
#endif

#if VK_GAMES
        Interstitial.Show();
#endif
    }

    public void VideoAdShow()
    {
        if (!_isInitialize)
            return;

#if UNITY_EDITOR
        OnRewardedCallback();
        OnVideoCloseCallback();
        return;
#endif

#if YANDEX_GAMES
        Agava.YandexGames.VideoAd.Show(OnVideoOpenCallback, OnRewardedCallback, OnVideoCloseCallback, OnVideoErrorCallback);
#endif

#if VK_GAMES
        Agava.VKGames.VideoAd.Show(OnRewardedCallback);
#endif
    }

    private void OnInitialized()
    {
        _isInitialize = true;
    }

    private void OnVideoOpenCallback()
    {
        VideoOpened?.Invoke();
    }

    private void OnVideoCloseCallback()
    {
        VideoClosed?.Invoke();
    }

    private void OnRewardedCallback()
    {
        Rewarded?.Invoke();
    }

    private void OnVideoErrorCallback(string message)
    {
        Debug.LogError(message);
    }
}
