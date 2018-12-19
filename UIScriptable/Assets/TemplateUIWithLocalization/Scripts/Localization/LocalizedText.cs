using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour
    {
        public string key;
        public string defaultValue;

        private Text text;

        private bool textSetted = true;

        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            //  if (LocalizationManager.instanse.languageSetEvent != null)

            text = GetComponent<Text>();

            if (LocalizationManager.instanse != null)
            {
                LocalizationManager.instanse.languageSetEvent.AddListener(SetText);
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

        private void OnEnable()
        {
            //  SetText();
        }

        public void SetText()
        {
            Debug.Log("Setting texxt");
            if (LocalizationManager.instanse != null && text != null)
            {
                string res = LocalizationManager.instanse.GetLocalizedValue(key);
                if (res != null)
                {
                    text.text = res;
                }
                else
                    text.text = defaultValue;
                textSetted = true;
            }
            else
                Debug.Log("No LocalizationManager");
        }



        private void OnDestroy()
        {
            if (LocalizationManager.instanse.languageSetEvent != null)
                LocalizationManager.instanse.languageSetEvent.RemoveListener(SetText);
        }
    }
}