using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class RendererComponent : IComponent
    {
        Panel panel = new Panel();
        Screen gameScreen;

        public void SetProperties(Screen _gameScreen)
        {
            gameScreen = _gameScreen;
        }

        public void UpdateRenderer(Vector2 position)
        {
            gameScreen.AddImage(panel, new Vector2(position.X, position.Y));
        }

        public void Update(float deltaTime)
        {

        }
    }
}
