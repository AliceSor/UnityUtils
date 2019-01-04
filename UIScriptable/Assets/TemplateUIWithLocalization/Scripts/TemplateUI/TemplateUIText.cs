using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TemplateUI
{
    [RequireComponent(typeof(Text))]
    public class TemplateUIText : TemplateUI
    {
        public enum TextType { HEAD, BUTTON, ORDINARY }
        public TextType textType;

        public bool useDefaultTextSize = true;

        private Text text;

        protected override void OnSkinUI()
        {
            base.OnSkinUI();

            text = GetComponent<Text>();

            if (skinData != null && text != null)
            {
                text.horizontalOverflow = HorizontalWrapMode.Overflow;
                text.verticalOverflow = VerticalWrapMode.Overflow;
                switch (textType)
                {
                    case TextType.BUTTON:
                        {
                            text.color = skinData.buttonTextColor;
                            text.font = skinData.buttonTextFont;
                            text.alignment = TextAnchor.MiddleCenter;

                            if (useDefaultTextSize)
                                text.fontSize = skinData.buttonTextSize;
                            break;
                        }
                    case TextType.ORDINARY:
                        {
                            text.color = skinData.ordinaryTextColor;
                            text.font = skinData.ordinaryTextFont;

                            if (useDefaultTextSize)
                                text.fontSize = skinData.ordinaryTextSize;
                            break;
                        }
                    case TextType.HEAD:
                        {
                            text.color = skinData.headerTextColor;
                            text.font = skinData.headerTextFont;

                            if (useDefaultTextSize)
                                text.fontSize = skinData.headerTextSize;
                            break;
                        }
                }
            }
        }
    }
}
