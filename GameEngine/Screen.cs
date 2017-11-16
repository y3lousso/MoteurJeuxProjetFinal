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
    class Screen
    {
        private GameEngine gameEngine;
        private Form form = new Form();



        // TEST : Instantiate 2 new panels
        Panel blackPanel = new Panel();
        Panel whitePanel = new Panel();
        int xPos = 5;

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
            form.KeyUp += OnKeyUp;
            form.FormClosed += OnFormClosed;
            form.KeyPreview = true;

            // Set screen properties
            form.Text = gameName;
            SetScreenProperties(width, height);

            // Display a help button on the form.
            form.HelpButton = false;
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
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            gameEngine.GetInputManager().ManageKeyPress(e);
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            gameEngine.GetInputManager().ManageKeyRelease(e);
        }

        public void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) {
                form.Hide();
                gameEngine.CloseGame();
            }
        }
        public void UpdateTest()
        {
            xPos++;
            blackPanel.Location = new Point(xPos, 10);
        }

        public void ClearScreen()
        {
            form.Controls.Clear();
        }

        public void AddImage(Panel panel, Vector2 position)
        {
            // Same here with the white one
            panel.BackColor = Color.Red;
            panel.Location = new Point((int)position.X, (int)position.Y);
            panel.Size = new Size(90, 90);
            // That's the point, container controls exposes the Controls
            // collection that you could use to add controls programatically
            form.Controls.Add(panel);
        }
    }
}
