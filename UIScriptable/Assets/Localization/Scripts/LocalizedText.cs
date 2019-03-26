using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class LocalizedText : MonoBehaviour
    {
        public string key;
        public string defaultValue;

        private Text text;

        private bool textSetted = true;
        TextMeshProUGUI textmeshPro;

        private bool subscribed = false;

        public LocalizedText()
        {
           // Debug.Log("Constructor");
            Subscribe();
        }
        
        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            //  if (LocalizationManager.instanse.languageSetEvent != null)

            text = GetComponent<Text>();
            textmeshPro = GetComponent<TextMeshProUGUI>();

            if (LocalizationManager.instanse != null)
            {
                LocalizationManager.instanse.languageSetEvent.AddListener(SetText);
                subscribed = true;
                Debug.Log("Add listener");
                if (LocalizationManager.instanse.GetIsReady())
                    SetText();
                else
                {
                    while (!LocalizationManager.instanse.GetIsReady())
                        yield return null;
                    SetText();
                }
            }
            else
                Debug.Log("No LocalizationManager");

        }

        public void Subscribe()
        {
//            if (!subscribed)
//            {
//                if (LocalizationManager.instanse != null)
//                {
//                    LocalizationManager.instanse.languageSetEvent.AddListener(SetText);
//                    subscribed = true;
//                    Debug.Log("Add listener");
//                    if (LocalizationManager.instanse.GetIsReady())
//                        SetText();
//
//                }
//                else
//                    Debug.Log("No LocalizationManager");
//            }
            LocalizationManager.texts.Add(this);
            
//                Debug.Log(LocalizationManager.texts.Count);
            
        }

        private void OnEnable()
        {
             SetText();
        }

        public void SetText()
        {
            Debug.Log("Setting texxt");

            try
            {
                text = GetComponent<Text>();
                textmeshPro = GetComponent<TextMeshProUGUI>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            if (LocalizationManager.instanse != null)
            {
                string res = LocalizationManager.instanse.GetLocalizedValue(key);
                if (res != null)
                {
                    if (text != null)
                        text.text = res;

                    else if (textmeshPro != null)
                        textmeshPro.SetText(res);
                    else
                        Debug.Log("Cannot find text component");
                }
                else
                {
                    if (text != null)
                        text.text = defaultValue;

                    else if (textmeshPro != null)
                        textmeshPro.SetText(defaultValue);
                    else
                        Debug.Log("Cannot find text component");
                }
                textSetted = true;
            }
            else
                Debug.Log("No LocalizationManager");
        }



        private void OnDestroy()
        {
            if (LocalizationManager.instanse != null)
            {
                if (LocalizationManager.instanse.languageSetEvent != null)
                    LocalizationManager.instanse.languageSetEvent.RemoveListener(SetText);
            }
        }
    }
}
