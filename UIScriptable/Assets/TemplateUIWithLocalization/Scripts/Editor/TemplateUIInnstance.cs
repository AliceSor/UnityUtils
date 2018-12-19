using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TemplateUIInnstance : Editor
{
    static GameObject clickedObject;
    [MenuItem("GameObject/Template UI/TemplateButton", priority = 0)]
    public static void AddButton()
    {
        Create("TemplateButton");
    }

    [MenuItem("GameObject/Template UI/TemplateText", priority = 0)]
    public static void AddButtonText()
    {
        Create("TemplateText");
    }

    [MenuItem("GameObject/LocalizationManager", priority = 0)]
    public static void AddLocalizationManager()
    {
        Create("LocalizationManager");
    }



    private static GameObject Create(string objectName)
    {
        GameObject inctance = Instantiate(Resources.Load<GameObject>(objectName));
        inctance.name = objectName;
        clickedObject = UnityEditor.Selection.activeObject as GameObject;
        if (clickedObject != null)
        {
            inctance.transform.SetParent(clickedObject.transform, false);
        }

        return inctance;
    }
}
