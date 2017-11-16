using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing;

namespace MoteurJeuxProjetFinal
{
    class InputManager
    {
        private GameEngine gameEngine;

        public void Init(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
        }

        public void CheckKeyboardInputs(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape :
                    gameEngine.CloseGame();
                    break;
                case Keys.Up:
                    gameEngine.CloseGame();
                    break;
                case Keys.Down:
                    gameEngine.CloseGame();
                    break;
                case Keys.Left:
                    gameEngine.CloseGame();
                    break;
                case Keys.Right:
                    gameEngine.CloseGame();
                    break;
                case Keys.Space:
                    gameEngine.CloseGame();
                    break;
                default:
                    break;
            }            
        }
    }
}
