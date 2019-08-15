using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    [RequireComponent(typeof(TextComponentHandler))]
    public class LocalizedText : MonoBehaviour
    {
        public string key;
        public string defaultValue;

        private Text text;

        private bool textSetted = true;
        TextMeshProUGUI textmeshPro;

        TextComponentHandler textComponentHandler;

        private bool subscribed = false;

        public LocalizedText()
        {
            Subscribe();
        }

        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            //  if (LocalizationManager.instanse.languageSetEvent != null)

            textComponentHandler = GetComponent<TextComponentHandler>();

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
            LocalizationManager.texts.Add(this);
        }

        private void OnEnable()
        {
             SetText();
        }

        public void SetText()
        {
            Debug.Log("Setting texxt");

            // try
            // {
            //     text = GetComponent<Text>();
            //     textmeshPro = GetComponent<TextMeshProUGUI>();
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e);
            //     throw;
            // }
             textComponentHandler = GetComponent<TextComponentHandler>();

            if (LocalizationManager.instanse != null)
            {
                string res = LocalizationManager.instanse.GetLocalizedValue(key);
                if (res != null)
                {
                    textComponentHandler.SetText(res);
                    // if (text != null)
                    //     text.text = res;

                    // else if (textmeshPro != null)
                    //     textmeshPro.SetText(res);
                    // else
                    //     Debug.Log("Cannot find text component");
                }
                else
                {
                    textComponentHandler.SetText(defaultValue);
                    // if (text != null)
                    //     text.text = defaultValue;

                    // else if (textmeshPro != null)
                    //     textmeshPro.SetText(defaultValue);
                    // else
                    //     Debug.Log("Cannot find text component");
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
