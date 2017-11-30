using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing;
using System.Numerics;
using System.Threading;


namespace MoteurJeuxProjetFinal
{
    class DisplayWindow : Form
    {
        private GameEngine gameEngine;

        public DisplayLayer displayLayer = new DisplayLayer();

        public delegate void AddPanelDelegate(Panel mainPanel, Panel panelToAdd);
        public AddPanelDelegate addPanelDelegate;

        public delegate void ClearPanelDelegate(Panel panel);
        public ClearPanelDelegate clearPanelDelegate;

        public void Init(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
        }

        public void InitFormProperties(string gameName, int width, int height)
        {
            // Preparing events
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
            KeyPreview = true;
            FormClosed += OnFormClosed;            
            
            // Set screen properties
            Text = gameName;
            Width = width;
            Height = height;
            HelpButton = false;
            FormBorderStyle = FormBorderStyle.None; 
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            // Display the form as a modal dialog box.
            Visible = true;
            Activate();
            Show();
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
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                gameEngine.CloseGame();
            }
        }

        public void AddImageToDisplayLayer(DisplayLayer displayLayer, Vector2 position, Vector2 size, string image)
        {
            // Same here with the white one
            Panel panelToAdd = new Panel();
            panelToAdd.BackgroundImage = Image.FromFile(gameEngine.imagePath + image);
            panelToAdd.BackgroundImageLayout = ImageLayout.Stretch;
            panelToAdd.BackColor = Color.Transparent;
            panelToAdd.Location = new Point((int)position.X, (int)position.Y);
            panelToAdd.Size = new Size((int)size.X, (int)size.Y);
            panelToAdd.TabIndex = 0;

            if (InvokeRequired)
            {
                addPanelDelegate = new AddPanelDelegate(AddPanelMethod);
                Invoke(addPanelDelegate, new Object[] { displayLayer, panelToAdd });
            }
            else
            {
                displayLayer.Controls.Add(panelToAdd);
            }
        }

        public void ClearDisplayLayer()
        {
            if (InvokeRequired)
            {
                clearPanelDelegate = new ClearPanelDelegate(ClearPanelMethod);
                Invoke(clearPanelDelegate, new Object[] { displayLayer });
            }
            else
            {
                displayLayer.Controls.Clear();
            }
        }

        static void ClearPanelMethod(Panel displayLayer)
        {
            displayLayer.Controls.Clear();
        }

        static void AddPanelMethod(Panel mainPanel, Panel panelToAdd)
        {
            mainPanel.Controls.Add(panelToAdd);
        }

        public Form GetForm() { return this; }

        public void DisplayScene(Scene scene)
        {
            // Clear old scene if needed
            displayLayer.Controls.Clear();

            // Draw Background Image
            displayLayer.BackgroundImage = Image.FromFile(gameEngine.imagePath + gameEngine.GetSceneManager().GetCurrentScene().backgroundImage);
            displayLayer.BackgroundImageLayout = ImageLayout.Stretch;
            displayLayer.Location = new Point(0, 0);
            displayLayer.Size = new Size(Width, Height);
            Controls.Add(displayLayer);

            Refresh();
        }
    }
}
