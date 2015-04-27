using Assets.Code.Generic;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class LayerMaskLibrary : Singleton<LayerMaskLibrary>
    {
        public LayerMask TileMask = 0;
        public LayerMask UnitMask = 0;
        public LayerMask PathfindingMask = 0;
    }
}