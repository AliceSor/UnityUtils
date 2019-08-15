using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        private Dictionary<string, string> localizedText;

        private bool isReady = false;
        private string missingString = "Not found";

        public string currentLanguage;
        [Space(10)]
        public LanguichPath[] languages;

        [HideInInspector]
        public UnityEvent languageSetEvent = new UnityEvent();

        public static LocalizationManager instanse;

        private void Start()
        {
            if (instanse == null)
                instanse = this;
            else if (instanse != this)
                Destroy(gameObject);
            //if (languageSetEvent == null)
            //    languageSetEvent = new UnityEvent();
            Debug.Log("start");
            SetLanguage();
            DontDestroyOnLoad(gameObject);

        }

        public void Test()
        {
            Debug.Log("event work");
        }

        public void ChangeLanguage(string language)
        {
            currentLanguage = language;
            SetLanguage();
            if (languageSetEvent != null)
            {
#if !UNITY_ANDROID
                Debug.Log("Invoke");
                //languageSetEvent.AddListener(Test);
                languageSetEvent.Invoke();
#endif
            }
        }

        public void SetLanguage()
        {
            if (languages != null)
            {
                if (languages.Length > 0)
                {
                    if (currentLanguage != null)
                    {
                        foreach (LanguichPath i in languages)
                        {
                            if (i.language == currentLanguage)
                                LoadLocalizedText(i.path);
                        }
                    }

                }
            }

        }

        public void LoadLocalizedText(string fileName)
        {
            localizedText = new Dictionary<string, string>();
#if UNITY_ANDROID && !UNITY_EDITOR
            string filePath = Path.Combine("jar:file://" + Application.dataPath + "!/assets/", fileName);
            StartCoroutine(LoadTextFromAndroid(filePath));
            return;
#elif UNITY_IOS && !UNITY_EDITOR
             string filePath = Path.Combine(Application.dataPath + "/Raw", fileName);
            

#elif UNITY_EDITOR
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
#endif

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

                for (int i = 0; i < loadedData.items.Length; i++)
                {
                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                }
                Debug.Log("Text loaded" + dataAsJson);
                isReady = true;

            }
            else
            {
                Debug.LogError("File not found");
            }
        }

        //just on Android Device
        IEnumerator LoadTextFromAndroid(string filePath)
        {
            WWW www = new WWW(filePath);
            while (!www.isDone)
            {
                yield return null;
            }
           if (string.IsNullOrEmpty(www.error))
            {
                string dataAsJson = www.text;
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

                for (int i = 0; i < loadedData.items.Length; i++)
                {
                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                }
                Debug.Log("Text loaded" + dataAsJson);
                isReady = true;
                if (languageSetEvent != null)
                {

                Debug.Log("Invoke");
                //languageSetEvent.AddListener(Test);
                languageSetEvent.Invoke();

                }
            }
        }

        public string GetLocalizedValue(string key)
        {
            string result = null;

            if (localizedText.ContainsKey(key))
            {
                return localizedText[key];
            }

            return result;
        }

        public bool GetIsReady() { return isReady; }

        private void OnDestroy()
        {
            languageSetEvent.RemoveAllListeners();
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 1000, 2000), "Hello World!");
        }
    } //LocalizationManager

   

    [System.Serializable]
    public struct LanguichPath
    {
        public string language;
        public string path;
    }
}