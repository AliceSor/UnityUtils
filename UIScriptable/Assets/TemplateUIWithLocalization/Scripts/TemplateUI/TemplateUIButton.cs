using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TemplateUI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class TemplateUIButton : TemplateUI
    {
        public bool useDefaultSize = true;
        public bool useDefaultColor = true;

        Image image;
        Button button;
        RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        protected override void OnSkinUI()
        {
            base.OnSkinUI();

            image = GetComponent<Image>();
            button = GetComponent<Button>();

            button.transition = Selectable.Transition.ColorTint;
            button.targetGraphic = image;

            if (skinData != null && rectTransform != null)
            {
                image.sprite = skinData.buttonSprite;
                image.type = Image.Type.Sliced;
                // button.spriteState = skinData.buttonSpriteState;

                if (useDefaultSize)
                {
                    rectTransform.sizeDelta = new Vector2(skinData.buttonWidth, skinData.buttonHeight);
                }
                if (useDefaultColor)
                {
                    image.color = skinData.buttonColor;
                }
            }


        }
    }
}