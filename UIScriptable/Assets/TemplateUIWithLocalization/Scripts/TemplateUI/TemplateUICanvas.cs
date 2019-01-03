using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TemplateUI
{

    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    public class TemplateUICanvas : TemplateUI
    {
        private CanvasScaler canvasScaler;

        private void Start()
        {
            canvasScaler = GetComponent<CanvasScaler>();
        }

        protected override void OnSkinUI()
        {
            base.OnSkinUI();

            if (skinData != null && canvasScaler != null)
            {
                canvasScaler.uiScaleMode = skinData.scaleMode;
                canvasScaler.referenceResolution = skinData.referenceResolution;
            }
        }
    }
}
