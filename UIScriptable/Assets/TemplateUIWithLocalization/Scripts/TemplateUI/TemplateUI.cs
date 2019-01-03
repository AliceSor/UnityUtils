using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TemplateUI
{
    [ExecuteInEditMode()]
    public class TemplateUI : MonoBehaviour
    {

        public TemplateUIData skinData;

        protected virtual void OnSkinUI()
        {

        }

        public virtual void Awake()
        {
            OnSkinUI();
        }

        public virtual void Update()
        {
            if (Application.isEditor)
            {
                OnSkinUI();
            }
        }

    }
}
