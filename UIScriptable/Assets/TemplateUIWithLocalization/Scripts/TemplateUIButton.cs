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
        Image image;
        Button button;

        protected override void OnSkinUI()
        {
            base.OnSkinUI();

            image = GetComponent<Image>();
            button = GetComponent<Button>();

            button.transition = Selectable.Transition.ColorTint;
            button.targetGraphic = image;

            if (skinData != null)
            {
                image.sprite = skinData.buttonSprite;
                image.type = Image.Type.Sliced;
                // button.spriteState = skinData.buttonSpriteState;
            }


        }
    }
}