using Assets.Code.GUI.WorldSpace;
using Assets.Code.Units.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.ScreenSpace
{
    public class UnitInfoGUI : MonoBehaviour
    {
        // Referenced GameObject
        public GameObject Panel_UnitInfo;

        // GUI text, set in Unity interface
        public Text Text_UnitInfo_Name;
        public Text Text_UnitInfo_Health;
        public Text Text_UnitInfo_Attack;
        public Text Text_UnitInfo_Defense;
        public Text Text_UnitInfo_Movement;

        // GUI images, set in Unity interface
        public Image Image_UnitInfo_Type;
        public Image Image_UnitInfo_Health;
        public Image Image_UnitInfo_Attack;
        public Image Image_UnitInfo_Defense;
        public Image Image_UnitInfo_Movement;

        // Listen for events when object is created
        void OnEnable()
        {
            MouseCursor.OnMouseOverUnit += SetUnitInfo;
        }

        // Stop listening for events if object is destroyed
        void OnDestroy()
        {
            MouseCursor.OnMouseOverUnit -= SetUnitInfo;
        }

        // Set GUI info
        private void SetUnitInfo(GameObject gameObject, int x, int y)
        {
            if (gameObject != null)
            {
                Unit Unit = gameObject.GetComponent<Unit>();
                Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                Color factionColor = gameObject.GetComponent<SpriteRenderer>().color;

                if (Unit != null && sprite != null)
                {
                    Panel_UnitInfo.SetActive(true);

                    SetText(Text_UnitInfo_Name, Unit.UnitName);
                    SetText(Text_UnitInfo_Health, Unit.Health.ToString() + "/" + Unit.MaxHealth.ToString() + " HP");
                    SetText(Text_UnitInfo_Attack, Unit.Attack.ToString());
                    SetText(Text_UnitInfo_Defense, Unit.Defense.ToString());
                    SetText(Text_UnitInfo_Movement, Unit.Movement.ToString());

                    SetImage(Image_UnitInfo_Type, sprite);
                    ShowImage(Image_UnitInfo_Health);

                    SetColor(Image_UnitInfo_Type, factionColor);
                    SetColor(Image_UnitInfo_Health, factionColor);
                    SetColor(Image_UnitInfo_Attack, factionColor);
                    SetColor(Image_UnitInfo_Defense, factionColor);
                    SetColor(Image_UnitInfo_Movement, factionColor);
                }
                else
                    NullUnitInfo();
            }
            else
                NullUnitInfo();
        }

        // Set all Unit info to null values
        private void NullUnitInfo()
        {
            Panel_UnitInfo.SetActive(false);

            SetText(Text_UnitInfo_Name, "No Unit");
            SetText(Text_UnitInfo_Attack, "0");
            SetText(Text_UnitInfo_Defense, "0");
            SetText(Text_UnitInfo_Movement, "0");

            SetImage(Image_UnitInfo_Type, null);
            HideImage(Image_UnitInfo_Health);
        }

        // Set GUI text
        private void SetText(Text textElement, string text)
        {
            if (textElement != null && text != null)
                textElement.text = text;
            else
                textElement.text = "None";
        }

        // Set GUI image
        private void SetImage(Image imageElement, Sprite image)
        {
            if (imageElement != null && image != null)
                imageElement.sprite = image;
            else
                imageElement.sprite = null;
        }

        // Set GUI element color
        private void SetColor(Text textElement, Color color)
        {
            if (textElement != null)
                textElement.color = color;
        }

        // Set GUI element color
        private void SetColor(Image imageElement, Color color)
        {
            if (imageElement != null)
                imageElement.color = color;
        }

        // Show GUI image
        private void ShowImage(Image imageElement)
        {
            imageElement.enabled = true;
        }

        // Hide GUI image
        private void HideImage(Image imageElement)
        {
            imageElement.enabled = false;
        }
    }
}