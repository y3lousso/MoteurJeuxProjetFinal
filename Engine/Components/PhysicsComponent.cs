using System.Collections.Generic;
using System.Numerics;

namespace Engine.Components
{
    public class PhysicsComponent : IComponent
    {
        // Data comming from XML
        public int masse = 1;

        public bool useGravity = false;
        public bool useAirFriction = true;
        public float airFrictionTweaker = .9f; // between 0 and 1

        // Others
        public List<Vector2> _forces = new List<Vector2>();
    }
}