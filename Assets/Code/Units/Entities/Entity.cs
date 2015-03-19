using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Units.Entities
{
    // Entity.cs - A container class for units and structures
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Entity : MonoBehaviour
    {
        // Unique identifier of the entity
        [HideInInspector]
        public int EntityID = -1;

        // Name of the entity, edited through unity interface
        public string EntityName = "Entity";

        // Sprite of the entity, edited through Unity interface
        public Sprite EntityGraphic = null;

        // GUI elements, edited through Unity interface
        public Text EntityTypeGUIText;
        public Image EntityTypeGUIImage;
    }
}