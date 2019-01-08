using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TemplateUI
{
    [RequireComponent(typeof(Image))]
    public class TemplateUIPanel : TemplateUI
    {
        public enum PanelType { ORDINARY, BACKGROUND, TOPBAR, SIDEMENU_BG}
        public PanelType panelType = PanelType.ORDINARY;

        private Image image;
        RectTransform rectTransform;

        private void Start()
        {
            image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
        }

        protected override void OnSkinUI()
        {
            base.OnSkinUI();

            if (skinData != null && image != null)
            {
                switch (panelType)
                {
                    case PanelType.ORDINARY:
                        {
                            break;
                        }
                    case PanelType.BACKGROUND:
                        {
                            image.color = skinData.backgroundColor;
                            break;
                        }
                    case PanelType.TOPBAR:
                        {
                            image.color = skinData.topBarColor;
                            if (rectTransform != null)
                            {
                                // rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, skinData.topBarHeight);
                                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, skinData.topBarHeight);
                            }
                            break;
                        }
                    case PanelType.SIDEMENU_BG:
                        {
                            image.color = skinData.sideMenuBackgroundColor;
                            break;
                        }
                }
            }
        }
    }
}
