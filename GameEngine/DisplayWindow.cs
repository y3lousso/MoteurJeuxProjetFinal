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

        public DisplayLayer displayLayer;

        public delegate void AddPanelDelegate(Panel mainPanel, Panel panelToAdd);
        public AddPanelDelegate addPanelDelegate;

        public delegate void ClearPanelDelegate(Panel panel);
        public ClearPanelDelegate clearPanelDelegate;

        public void Init(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
        }

        public void SetScreenProperties(int width, int height)
        {
            Width = width;
            Height = height;

            // Display a help button on the form.
            HelpButton = false;
            // Define the border style of the form to a dialog box.
            FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            MinimizeBox = false;
        }

        public void InitForm(string gameName, int width, int height)
        {
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
            KeyPreview = true;

            FormClosed += OnFormClosed;            
            

            // Set screen properties
            Text = gameName;
            SetScreenProperties(width, height);

            // Init Components
            InitializeComponent(width, height);

            // Set the start position of the form to the center of the screen.
            StartPosition = FormStartPosition.CenterScreen;

            //this.AddImageToDisplayLayer(staticDisplayLayer, new Vector2(0, 0), new Vector2(500, 500), Plateformer2D.Ressource_Img.background);

            // Display the form as a modal dialog box.
            Visible = true;
            Focus();
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

        public void AddImageToDisplayLayer(DisplayLayer displayLayer, Vector2 position, Vector2 size, Image image)
        {
            // Same here with the white one
            Panel panelToAdd = new Panel();
            panelToAdd.BackgroundImage = image;
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

        private void InitializeComponent(int width, int height)
        {
            // Static and dynamic layers
            displayLayer = new DisplayLayer();
            DoubleBuffered = true;
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            displayLayer.SuspendLayout();
            SuspendLayout();

            // staticDisplayLayer
            displayLayer.BackgroundImage = gameEngine.GetCurrentScene().backgroundImage;
            displayLayer.BackgroundImageLayout = ImageLayout.Stretch;
            displayLayer.Location = new Point(0, 0);
            displayLayer.Name = "staticDisplayLayer";
            displayLayer.Size = new Size(width, height);
            displayLayer.TabIndex = 0;
            Controls.Add(displayLayer);

            // DisplayWindow
            Size = new System.Drawing.Size(width, height);
            displayLayer.ResumeLayout(true);
            ResumeLayout(true);

        }
    }
}
