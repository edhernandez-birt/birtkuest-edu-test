using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocaleDropdown : MonoBehaviour
{
    public Dropdown dropdown;

    IEnumerator Start()
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;

        // Generate list of available Locales
        var options = new List<Dropdown.OptionData>();
        int selected = 0;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
                selected = i;
            //Adaptamos a nuestros 3 idiomas
            if (locale.name.ToUpperInvariant().Equals("SPANISH (ES)"))
                options.Add(new Dropdown.OptionData("ESPAÑOL"));
            else if (locale.name.ToUpperInvariant().Equals("BASQUE (EU)"))
                options.Add(new Dropdown.OptionData("EUSKARA"));
            else if (locale.name.ToUpperInvariant().Equals("ENGLISH (EN)"))
                options.Add(new Dropdown.OptionData("ENGLISH"));
            //Fin de adaptación
            else
            options.Add(new Dropdown.OptionData(locale.name.ToUpperInvariant()));
        }
        dropdown.options = options;

        dropdown.value = selected;
        dropdown.onValueChanged.AddListener(LocaleSelected);
    }

    static void LocaleSelected(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
