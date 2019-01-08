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
        public enum ButtonType {ORDINARY, SIDEMENU}
        public ButtonType buttonType = ButtonType.ORDINARY;

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
                switch (buttonType)
                {
                    case ButtonType.ORDINARY:
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
                            break;
                        }
                    case ButtonType.SIDEMENU:
                        {
                            image.sprite = skinData.sideMenuButtonSprite;
                            image.type = Image.Type.Sliced;
                            // button.spriteState = skinData.buttonSpriteState;
                           
                            if (useDefaultColor)
                            {
                                image.color = skinData.sideMenuButtonColor;
                            }
                            break;
                        }
                }
                
            }


        }
    }
}