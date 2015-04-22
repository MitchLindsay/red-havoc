using UnityEngine;

namespace Assets.Code.UI.Static
{
    public class StatWindow : Window
    {
        protected Color32 colorPositive = new Color32(61, 229, 118, 255);
        protected Color32 colorNegative = new Color32(229, 61, 61, 255);
        protected Color32 colorNeutral = Color.white;

        protected Color32 colorVeryHighHealth = new Color32(61, 229, 118, 255);
        protected Color32 colorHighHealth = new Color32(188, 249, 48, 255);
        protected Color32 colorMediumHealth = new Color32(251, 235, 116, 255);
        protected Color32 colorLowHealth = new Color32(255, 101, 32, 255);
        protected Color32 colorVeryLowHealth = new Color32(229, 61, 61, 255);
    }
}