using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class InputComponent : IComponent
    {
        // Data comming from XML
        public float inputTweaker = 500f;

        // Others
        public Vector2 inputXY = new Vector2(0, 0);
        
    }
}
