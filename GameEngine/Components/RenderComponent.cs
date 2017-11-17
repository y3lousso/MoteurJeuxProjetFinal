using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace MoteurJeuxProjetFinal
{
    class RenderComponent : IComponent
    {
        public Image image;
        public Vector2 size = new Vector2(500, 500);
    }
}
