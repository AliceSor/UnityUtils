using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TemplateUI
{
    [CreateAssetMenu(menuName = "TemplateUI Data")]
    public class TemplateUIData : ScriptableObject
    {
        public Sprite buttonSprite;

        public Color buttonTextColor;
        public Color ordinaryTextColor;
        public Color headerTextColor;

        public Font buttonTextFont;
        public Font ordinaryTextFont;
        public Font headerTextFont;

        //public SpriteState buttonSpriteState;
    }
}
