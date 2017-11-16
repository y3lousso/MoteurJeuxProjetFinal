using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing;
using System.Numerics;

namespace MoteurJeuxProjetFinal
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
                    inputs.inputXY.Y--; // may need to inverte this if 0,0 is top left
                    break;
                case Keys.Down:
                    inputs.inputXY.Y++;
                    break;
                case Keys.Left:
                    inputs.inputXY.X--;
                    break;
                case Keys.Right:
                    inputs.inputXY.X++;
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
        }

        public void ManageKeyRelease(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    inputs.inputXY.Y++;
                    break;
                case Keys.Down:
                    inputs.inputXY.Y--;
                    break;
                case Keys.Left:
                    inputs.inputXY.X++;
                    break;
                case Keys.Right:
                    inputs.inputXY.X--;
                    break;
                case Keys.Space:
                    inputs.space = false;
                    break;
                default:
                    break;
            }
        }

        public struct Inputs
        {
            public Vector2 inputXY;
            public bool space;
        }
    }
}
