using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TemplateUI
{
    [CreateAssetMenu(menuName = "TemplateUI Data")]
    public class TemplateUIData : ScriptableObject
    {
        [Header("Button Settings")]
        public Sprite buttonSprite;
        public Color buttonColor;
        public float buttonWidth, buttonHeight;
        //public SpriteState buttonSpriteState;

        [Header("Text Settings")]
        public Color buttonTextColor;
        public Color ordinaryTextColor;
        public Color headerTextColor;

        [Space(10)]
        public Font buttonTextFont, ordinaryTextFont, headerTextFont;

        [Space(10)]
        public int buttonTextSize, ordinaryTextSize, headerTextSize;

        [Header("Canvas Settings")]
        public CanvasScaler.ScaleMode scaleMode;
        public Vector2 referenceResolution;

        public Color backgroundColor;
        public Color topBarColor;

        public float topBarHeight;

        [Header("SideBar Settings")]
        public Color sideMenuBackgroundColor;
        public Sprite sideMenuButtonSprite;
        public Color sideMenuButtonColor;
    }
}
