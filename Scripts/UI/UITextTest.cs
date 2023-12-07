using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class UITextTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtUI;
    private string nombre = "Link";
    // Start is called before the first frame update

    string GetStringForUI()
    {
        return LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationTable", "UserUI");
    }

    void SetString()
    {
        txtUI.text = GetStringForUI()+" "+nombre;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetString();
        }
    }
}
