using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Units.Entities
{
    public enum FactionController
    {
        None,
        Player,
        AI
    }

    // Faction.cs - Represents an individual player, contains entities controlled by the faction
    public class Faction : MonoBehaviour
    {
        // Faction name, edited through the unity interface
        public string FactionName = "Faction";

        // Faction controller, edited through the unity interface
        public FactionController FactionController = FactionController.None;

        // Color of the faction, edited through the unity interface
        public Color FactionColor = Color.white;

        // List of controlled entities
        public List<Entity> Entities { get; private set; }

        void Start()
        {
            // Remove all entities in the list
            RemoveAllEntities();
            // Add all the child entities
            AddChildEntities();
        }

        // Adds an entity to the list of controlled entities
        public void AddEntity(Entity entity)
        {
            // Only add the entity if it is not already controlled
            if (!Entities.Contains(entity))
            {
                SetColor(entity, FactionColor);
                Entities.Add(entity);
            }
        }

        // Adds all child entities to the list of controlled entities
        public void AddChildEntities()
        {
            // Find all controlled units
            Unit[] units = gameObject.GetComponentsInChildren<Unit>();
            // Find all controlled structures
            Structure[] structures = gameObject.GetComponentsInChildren<Structure>();

            // Add controlled units
            foreach (Unit unit in units)
                AddEntity(unit);

            // Add controlled structures
            foreach (Structure structure in structures)
                AddEntity(structure);
        }

        // Removes a entity from the list of controlled entities
        public void RemoveUnit(Entity entity)
        {
            // Only remove the entity if it is currently controlled
            if (Entities.Contains(entity))
            {
                SetColor(entity, Color.white);
                Entities.Remove(entity);
            }
        }

        // Removes all entities
        public void RemoveAllEntities()
        {
            Entities = new List<Entity>();
        }

        // Changes a entity's color
        public void SetColor(Entity entity, Color color)
        {
            // Check if entity exists
            if (entity != null)
                entity.GetComponent<SpriteRenderer>().color = color;
        }
    }
}