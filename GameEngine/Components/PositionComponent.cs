using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class PositionComponent : IComponent
    {
        // Data comming from XML
        public Vector2 position = new Vector2(600, 100);
        public float orientation = 0f;

        // Others
    }
}
