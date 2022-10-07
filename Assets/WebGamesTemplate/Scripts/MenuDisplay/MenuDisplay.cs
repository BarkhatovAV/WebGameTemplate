using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField] private LeaderBoardButton _leaderBoardButton;
    [SerializeField] private InviteFriendsButton _inviteFriendsButton;

    private void Start()
    {
        Show();
    }

    public void Show()
    {
#if YANDEX_GAMES || UNITY_EDITOR
        _leaderBoardButton.gameObject.SetActive(true);
#endif

#if VK_GAMES
        if (Application.isMobilePlatform)
        {
            _leaderBoardButton.gameObject.SetActive(true);
        }
        _inviteFriendsButton.gameObject.SetActive(true);
#endif
    }

    public void Hide()
    {
        _leaderBoardButton.gameObject.SetActive(false);

#if VK_GAMES
        _inviteFriendsButton.gameObject.SetActive(false);
#endif
    }
}
