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

        public Panel dynamicPanel= new Panel();
        public Panel staticPanel = new Panel();

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

            // Dynamic display for entities 
            dynamicPanel.BackColor = Color.FromArgb(0,0,0,0);
            dynamicPanel.Location = new Point(0, 0);
            dynamicPanel.Size = new Size(width, height/2);
            dynamicPanel.BackColor = Color.FromArgb(25, Color.Red);
            form.Controls.Add(dynamicPanel);

            // Static display for background layers
            staticPanel.Location = new Point(0, 0);
            staticPanel.Size = new Size(width, height);
            form.Controls.Add(staticPanel);           
       
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

        public void ClearStaticPanel()
        {
            staticPanel.Controls.Clear();
        }

        public void ClearDynamicPanel()
        {
            dynamicPanel.Controls.Clear();
        }

        public void DisplayScene(Scene scene)
        {
            ClearDynamicPanel();
            ClearStaticPanel();

            AddImageToPanel(staticPanel, new Vector2(0, 0), new Vector2(staticPanel.Width, staticPanel.Height), scene.backgroundImage);
        }

        public void AddImageToPanel(Panel panel, Vector2 position, Vector2 size, Image image)
        {
            // Same here with the white one
            Panel panelToAdd = new Panel();
            panelToAdd.BackgroundImage = image;
            //panelToAdd.BackColor = Color.Black;
            panelToAdd.Location = new Point((int)position.X, (int)position.Y);
            panelToAdd.Size = new Size((int)size.X, (int)size.Y);
            panelToAdd.BringToFront();
            panel.Controls.Add(panelToAdd);

        }

        public Form GetForm() { return form; }
    }
}
