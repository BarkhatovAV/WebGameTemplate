using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;
using System.Collections;

public class LanguagesChanger : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

#if YANDEX_GAMES
    private IEnumerator Start()
    {
        DontDestroyOnLoad(_leanLocalization.gameObject);

        while (!YandexGamesSdk.IsInitialized)
        {
            yield return new WaitForSeconds(0.05f);
            if (YandexGamesSdk.IsInitialized)
                LoadLocalization();
        }
    }

    private void LoadLocalization()
    {
        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case "ru":
                _leanLocalization.SetCurrentLanguage("Russian");
                break;
            case "tr":
                _leanLocalization.SetCurrentLanguage("Turkish");
                break;
            case "en":
                _leanLocalization.SetCurrentLanguage("English");
                break;
            default:
                _leanLocalization.SetCurrentLanguage("Russian");
                break;
        }
    }
#endif

#if VK_GAMES
    private void Start()
    {
        DontDestroyOnLoad(_leanLocalization.gameObject);
        _leanLocalization.SetCurrentLanguage("Russian");
    }
#endif
}
