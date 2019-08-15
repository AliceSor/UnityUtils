using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Localization
{
    public class LocalizedDataEditor : EditorWindow
    {
        public LocalizationData localizationData;

        [MenuItem("Window/Localized Data Editor")]
        static void Init()
        {
            EditorWindow.GetWindow(typeof(LocalizedDataEditor)).Show();
        }

        private void OnGUI()
        {
            if (localizationData != null)
            {
                SerializedObject serializedObject = new SerializedObject(this);
                SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
                EditorGUILayout.PropertyField(serializedProperty, true);
                serializedObject.ApplyModifiedProperties();

                if (GUILayout.Button("SaveData"))
                {
                    SaveLocalizedData();
                }
            }

            if (GUILayout.Button("LoadData"))
            {
                LoadLocalizationData();
            }

            if (GUILayout.Button("Create new Data"))
            {
                CreateNewData();
            }
        }

        private void LoadLocalizationData()
        {
            string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");
            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            }
        }

        private void SaveLocalizedData()
        {
            string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");

            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = JsonUtility.ToJson(localizationData);
                File.WriteAllText(filePath, dataAsJson);
            }
        }

        private void CreateNewData()
        {
            localizationData = new LocalizationData();
        }
    }
}
