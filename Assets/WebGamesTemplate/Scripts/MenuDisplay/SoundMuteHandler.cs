using Agava.WebUtility;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundMuteHandler : MonoBehaviour
{
    [SerializeField] private Sprite _mute;
    [SerializeField] private Sprite _unmute;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private Ad _ad;

    private string _isSoundOn = "isSoundOn";
    private bool _isSoundMute;

    private void OnEnable()
    {
        _button.onClick.AddListener(SoundMuteButtonOn);
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        _ad.VideoOpened += OnVideoOpened;
        _ad.VideoClosed += OnVideoClosed;
    }


    private void OnDisable()
    {
        _button.onClick.RemoveListener(SoundMuteButtonOn);
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        _ad.VideoOpened += OnVideoOpened;
        _ad.VideoClosed += OnVideoClosed;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        if (!_isSoundMute)
        {
            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0 : 1; 
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(_isSoundOn))
        {
            _isSoundMute = !Convert.ToBoolean(PlayerPrefs.GetInt(_isSoundOn));
        }
        else
        {
            PlayerPrefs.SetInt(_isSoundOn, Convert.ToInt32(true));
            _isSoundMute = false;
        }

        if (_isSoundMute == true)
        {
            DisableSound();
            _image.sprite = _mute;
        }
        else
        {
            EnableSound();
            _image.sprite = _unmute;
        }
    }

    private void SoundMuteButtonOn()
    {
        if (_isSoundMute == false)
        {
            _isSoundMute = true;
            PlayerPrefs.SetInt(_isSoundOn, Convert.ToInt32(!_isSoundMute));
            _image.sprite = _mute;
            DisableSound();
        }
        else
        {
            _isSoundMute = false;
            _image.sprite = _unmute;
            PlayerPrefs.SetInt(_isSoundOn, Convert.ToInt32(!_isSoundMute));
            EnableSound();
        }
    }

    private void OnVideoClosed()
    {
        if (!_isSoundMute)
        {
            EnableSound();
        }
    }

    private void OnVideoOpened()
    {
        DisableSound();
    }

    private void EnableSound()
    {
        AudioListener.pause = false;
        AudioListener.volume = 1;
    }

    private void DisableSound()
    {
        AudioListener.pause = true;
        AudioListener.volume = 0;
    }
}

