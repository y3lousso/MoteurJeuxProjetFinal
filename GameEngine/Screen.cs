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
    class Screen
    {
        private GameEngine gameEngine;
        private Form form = new Form();

        public Form GetForm() { return form; }

        

        public void Init(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
        }

        public void SetScreenProperties(int width, int height)
        {
            form.Width = width;
            form.Height = height;
        }

        public void InitForm(string gameName, int width, int height)
        {
            form.KeyDown += OnKeyDown;
            form.FormClosed += OnFormClosed;
            form.KeyPreview = true;

            // Set the caption bar text of the form.   
            form.Text = gameName;
            // Set screen size
            SetScreenProperties(width, height);

            
            // Display a help button on the form.
            form.HelpButton = true;
            // Define the border style of the form to a dialog box.
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            form.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            form.MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            form.StartPosition = FormStartPosition.CenterScreen;        

            // Display the form as a modal dialog box.
            form.Visible = true;
            form.Focus();
            form.Activate();
           // Application.Run(form);
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            gameEngine.GetInputManager().CheckKeyboardInputs(e);
        }

        public void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) {
                form.Hide();
                gameEngine.CloseGame();
            }
        }
    }
}
