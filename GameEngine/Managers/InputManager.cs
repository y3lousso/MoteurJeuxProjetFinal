using System.Numerics;
using System.Windows.Forms;

namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    class InputManager
    {
        private GameEngine _gameEngine;

        private Inputs _inputs;

        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void ManageKeyPress(KeyEventArgs e)
        {            
            switch (e.KeyCode)
            {
                case Keys.Up:
                    _inputs.InputXY.Y = -1; // may need to invert this if 0,0 is top left
                    break;
                case Keys.Down:
                    _inputs.InputXY.Y = 1;
                    break;
                case Keys.Left:
                    _inputs.InputXY.X = -1;
                    break;
                case Keys.Right:
                    _inputs.InputXY.X = 1;
                    break;
                case Keys.Space:
                    _inputs.Space = true;
                    break;
                case Keys.Escape:
                    _gameEngine.CloseGame();
                    break;
            }
            e.Handled = true;
        }

        public void ManageKeyRelease(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    _inputs.InputXY.Y = 0;
                    break;
                case Keys.Down:
                    _inputs.InputXY.Y = 0;
                    break;
                case Keys.Left:
                    _inputs.InputXY.X = 0;
                    break;
                case Keys.Right:
                    _inputs.InputXY.X = 0;
                    break;
                case Keys.Space:
                    _inputs.Space = false;
                    break;
            }
            e.Handled = true;
        }

        public Inputs GetInputs()
        {
            return _inputs;
        }

        public struct Inputs 
        {
            public Vector2 InputXY;
            public bool Space;
        }
    }
}
