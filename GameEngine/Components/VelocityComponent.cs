using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class VelocityComponent : IComponent
    {
        public Vector2 velocity = new Vector2(0, 0);
        public float angularVelocity = 0f;
    }
}
