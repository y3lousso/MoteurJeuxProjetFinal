using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class PhysicsComponent : IComponent
    {
        public List<Vector2> _forces = new List<Vector2>();
        public int masse = 1;
        public bool useGravity = false;
        public bool useAirFriction = true;
        public float airFrictionTweaker = .9f; // between 0 and 1
    }
}
