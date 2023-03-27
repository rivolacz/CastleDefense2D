using Project.Localization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguagePicker : MonoBehaviour
{
    public List<Language> Languages = new List<Language>();
    private int currentLanguageIndex = 0;
    [SerializeField]
    private TMP_Text LanguageNameText;
    [SerializeField]
    Image flagImage;

    private const string languageSaveKey = "language";

    public void NextLanguage()
    {
        ChangeLanguage(1);
    }

    public void PreviousLanguage() 
    {
        ChangeLanguage(-1);
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey(languageSaveKey))
        {
            string savedLanguage = PlayerPrefs.GetString(languageSaveKey);
            Language language = Languages.First(language => language.Name == savedLanguage);
            if (language != null)
            {
                WordsDictionary.SetNewLanguage(language);
                return;
            }
        }
        WordsDictionary.SetNewLanguage(Languages.First());
    }

    private void OnDisable()
    {
        SaveLanguage(Languages[currentLanguageIndex]);
    }

    public void ChangeLanguage(int offset)
    {
        currentLanguageIndex += offset;
        currentLanguageIndex = UpdateIndex(currentLanguageIndex);
        Language language = Languages[currentLanguageIndex];
        flagImage.sprite = language.Flag;
        LanguageNameText.text = language.Name;
        WordsDictionary.SetNewLanguage(language);
    }

    
    public int UpdateIndex(int index)
    {
        if(index >= Languages.Count)
        {
            index = 0;
        }
        else if(index < 0)
        {
            index = Languages.Count - 1;
        }
        return index;
    }

    public void SaveLanguage(Language language)
    {
        PlayerPrefs.SetString(languageSaveKey, language.Name);
    }
}
