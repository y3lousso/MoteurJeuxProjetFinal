using System.Numerics;
using System.Windows.Forms;

namespace MoteurJeuxProjetFinal.GameEngine
{
    class InputManager
    {
        private GameEngine gameEngine;

        public Inputs inputs = new Inputs();

        public void Init(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
        }

        public void ManageKeyPress(KeyEventArgs e)
        {            
            switch (e.KeyCode)
            {
                case Keys.Up:
                    inputs.inputXY.Y = -1; // may need to invert this if 0,0 is top left
                    break;
                case Keys.Down:
                    inputs.inputXY.Y = 1;
                    break;
                case Keys.Left:
                    inputs.inputXY.X = -1;
                    break;
                case Keys.Right:
                    inputs.inputXY.X = 1;
                    break;
                case Keys.Space:
                    inputs.space = true;
                    break;
                case Keys.Escape:
                    gameEngine.CloseGame();
                    break;
                default:
                    break;
            }
            e.Handled = true;
        }

        public void ManageKeyRelease(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    inputs.inputXY.Y = 0;
                    break;
                case Keys.Down:
                    inputs.inputXY.Y = 0;
                    break;
                case Keys.Left:
                    inputs.inputXY.X = 0;
                    break;
                case Keys.Right:
                    inputs.inputXY.X = 0;
                    break;
                case Keys.Space:
                    inputs.space = false;
                    break;
                default:
                    break;
            }
            e.Handled = true;
        }

        public struct Inputs
        {
            public Vector2 inputXY;
            public bool space;
        }
    }
}
