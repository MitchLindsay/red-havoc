using UnityEngine;

namespace Assets.Code.Units.Entities
{
    public enum StructureType
    {
        None,
        Base
    }

    // Structure.cs - A single structure
    public class Structure : Entity
    {
        // Structure type, edited through unity interface
        public StructureType StructureType = StructureType.None;

        // Is the structure capturable by a faction? Edited in unity interface
        public bool Capturable = false;
    }
}