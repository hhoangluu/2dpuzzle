using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public static class LocaleCode
{
    public const string ENGLISH = "en";
    public const string CHINESE_SIMPILIFIED = "zh";
    public const string CHINESE_TRADITIONAL = "zh-TW";
    public const string FRENCH = "fr";
    public const string GERMAN = "de";
    public const string INDONESIAN = "id-ID";
    public const string ITALIAN = "it";
    public const string JAPAN = "jp";
    public const string KOREAN = "ko";
    public const string PORTUGUESE = "pt";
    public const string RUSSIAN = "ru";
    public const string SPAIN = "es-ES";
    public const string THAI = "th";
    public const string TURKISH = "tr-TR";
    public const string VIETNAMESE = "vi";
}
public class AutoLocale : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        Debug.Log("@LOCALE " + Application.systemLanguage.ToString());
        //     Debug.Log("@LOCALE " + Application.systemLanguage.ToString() + " " + SystemLanguage.English + " " (Application.systemLanguage== SystemLanguage.French))
        SetLanguageDefault(LocaleCode.ENGLISH);
        //switch (Application.systemLanguage)
        //{
        //    case SystemLanguage.English:
        //        SetLanguageDefault(LocaleCode.ENGLISH);
        //        break;
        //    case SystemLanguage.ChineseSimplified:
        //        SetLanguageDefault(LocaleCode.CHINESE_SIMPILIFIED);
        //        break;
        //    case SystemLanguage.ChineseTraditional:
        //        SetLanguageDefault(LocaleCode.CHINESE_TRADITIONAL);
        //        break;
        //    case SystemLanguage.French:
        //        SetLanguageDefault(LocaleCode.FRENCH);
        //        break;
        //    case SystemLanguage.German:
        //        SetLanguageDefault(LocaleCode.GERMAN);
        //        break;
        //    case SystemLanguage.Indonesian:
        //        SetLanguageDefault(LocaleCode.INDONESIAN);
        //        break;
        //    case SystemLanguage.Italian:
        //        SetLanguageDefault(LocaleCode.ITALIAN);
        //        break;
        //    case SystemLanguage.Japanese:
        //        SetLanguageDefault(LocaleCode.JAPAN);
        //        break;
        //    case SystemLanguage.Korean:
        //        SetLanguageDefault(LocaleCode.KOREAN);
        //        break;
        //    case SystemLanguage.Portuguese:
        //        SetLanguageDefault(LocaleCode.PORTUGUESE);
        //        break;
        //    case SystemLanguage.Russian:
        //        SetLanguageDefault(LocaleCode.RUSSIAN);
        //        break;
        //    case SystemLanguage.Spanish:
        //        SetLanguageDefault(LocaleCode.SPAIN);
        //        break;
        //    case SystemLanguage.Thai:
        //        SetLanguageDefault(LocaleCode.THAI);
        //        break;
        //    case SystemLanguage.Turkish:
        //        SetLanguageDefault(LocaleCode.TURKISH);
        //        break;
        //    case SystemLanguage.Vietnamese:
        //        SetLanguageDefault(LocaleCode.VIETNAMESE);
        //        break;
        //    default:
        //        SetLanguageDefault(LocaleCode.ENGLISH);
        //        break;
        //}
        //    LocalizationSettings.SelectedLocale.Identifier.CultureInfo.
    }

    // Update is called once per frame
    void SetLanguageDefault(string code)
    {
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            //  Debug.Log("@LOCALE " + LocalizationSettings.AvailableLocales.Locales[i].Identifier.Code + " ... " + code);
            if (LocalizationSettings.AvailableLocales.Locales[i].Identifier.Code == code)
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
                Debug.Log("@LOCALE SELECT" + LocalizationSettings.AvailableLocales.Locales[i].Identifier.Code + " ... " + code);

            }
        }
    }
}
