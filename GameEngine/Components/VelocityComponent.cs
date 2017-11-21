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
        // Data comming from XML
        public float maxVelocity = 500f;
        
        // Others
        public Vector2 velocity = new Vector2(0, 0);
        public float angularVelocity = 0f;



    }
}
