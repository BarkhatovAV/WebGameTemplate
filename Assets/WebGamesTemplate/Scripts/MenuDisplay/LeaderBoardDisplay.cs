using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _ranks;
    [SerializeField] private TMP_Text[] _leaderNames;
    [SerializeField] private TMP_Text[] _scoreList;
    [SerializeField] private string _leaderboardName = "LeaderBoard";

    private int _playerScore = 69;

    public void OpenVKLeaderboard()
    {
        Agava.VKGames.Leaderboard.ShowLeaderboard(_playerScore);
    }

    public void OpenYandexLeaderboard()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
        if (!PlayerAccount.IsAuthorized)
            PlayerAccount.Authorize();

        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {

            int leadersNumber = result.entries.Length >= _leaderNames.Length ? _leaderNames.Length : result.entries.Length;
            for (int i = 0; i < leadersNumber; i++)
            {
                string name = result.entries[i].player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonimus";

                _leaderNames[i].text = name;
                _scoreList[i].text = result.entries[i].formattedScore;
                _ranks[i].text = result.entries[i].rank.ToString();
            }
        });
    }

    public void SetLeaderboardScore()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            Leaderboard.GetPlayerEntry(_leaderboardName, OnSuccessCallback);
        }
    }

    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        if (result==null || _playerScore > result.score)
        {
            Leaderboard.SetScore(_leaderboardName, _playerScore);
        }      
    }
}
