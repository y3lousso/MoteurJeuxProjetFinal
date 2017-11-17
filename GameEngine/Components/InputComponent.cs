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
        public Vector2 inputXY = new Vector2(0, 0);
        public bool space = false;
    }
}
