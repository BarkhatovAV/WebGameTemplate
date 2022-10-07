using Agava.VKGames;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InviteFriendsButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnInviteButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnInviteButtonClick);
    }

#if YANDEX_GAMES
    private void Awake()
    {
        gameObject.SetActive(false);
    }
#endif

    public void OnInviteButtonClick()
    {
        SocialInteraction.InviteFriends(OnRewardedCallback);
    }

    private void OnRewardedCallback()
    {
        //Reward the player with in-game money
    }
}
