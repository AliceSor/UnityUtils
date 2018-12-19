using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Localization;

public class Example : MonoBehaviour {

	public void GoToAnotherScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetLanguage(string language)
    {
        if (LocalizationManager.instanse != null)
            LocalizationManager.instanse.ChangeLanguage(language);
        else
            Debug.Log("No LocalizationManager");
    }
}
