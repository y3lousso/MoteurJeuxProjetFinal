using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MoteurJeuxProjetFinal.GameEngine
{
    class DisplayWindow : Form
    {
        private GameEngine _gameEngine;

        public DisplayLayer DisplayLayer = new DisplayLayer();
        
        private List<ImagePanel> _imagesPanels = new List<ImagePanel>();

        public delegate void AddPanelDelegate(Panel mainPanel, Panel panelToAdd);
        private AddPanelDelegate addPanelDelegate;

        public delegate void ClearPanelDelegate(Panel panel);
        private ClearPanelDelegate clearPanelDelegate;

        public delegate void RemovePanelDelegate(Panel mainPanel, Panel panelToRemove);
        private RemovePanelDelegate removePanelDelegate;
        
        private struct ImagePanel
        {
            public Panel Panel;
            public RenderNode RenderNode;
        }

        public void Init(GameEngine _gameEngine)
        {
            this._gameEngine = _gameEngine;
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
            _gameEngine.GetInputManager().ManageKeyPress(e);
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            _gameEngine.GetInputManager().ManageKeyRelease(e);
        }

        public void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                _gameEngine.CloseGame();
            }
        }

        public void AddImageToDisplayLayer(DisplayLayer displayLayer, RenderNode renderNode)
        {
            // Same here with the white one
            Panel panelToAdd = new Panel();
            panelToAdd.BackgroundImage = Image.FromFile(_gameEngine.imagePath + renderNode.Image);
            panelToAdd.BackgroundImageLayout = ImageLayout.Stretch;
            panelToAdd.BackColor = Color.Transparent;
            panelToAdd.Location = new Point((int)renderNode.Position.X, (int)renderNode.Position.Y);
            panelToAdd.Size = new Size((int)renderNode.Size.X, (int)renderNode.Size.Y);
            panelToAdd.TabIndex = 0;

            if (InvokeRequired)
            {
                addPanelDelegate = AddPanelMethod;
                Invoke(addPanelDelegate, displayLayer, panelToAdd);
            }
            else
            {
                displayLayer.Controls.Add(panelToAdd);
            }
            _imagesPanels.Add(new ImagePanel
            {
                RenderNode = renderNode,
                Panel = panelToAdd
            });
        }

        public void UpdateImageFromDisplayLayer(DisplayLayer displayLayer, RenderNode oldRenderNode, RenderNode newRenderNode)
        {
            ImagePanel panelToUpdate = _imagesPanels.Find(ip => ip.RenderNode.Equals(oldRenderNode));
            panelToUpdate.Panel.BackgroundImage = Image.FromFile(_gameEngine.imagePath + newRenderNode.Image);
            panelToUpdate.Panel.Location = new Point((int)newRenderNode.Position.X, (int)newRenderNode.Position.Y);
            panelToUpdate.Panel.Size = new Size((int)newRenderNode.Size.X, (int)newRenderNode.Size.Y);
            panelToUpdate.RenderNode = newRenderNode;
        }

        public void RemoveImageFromDisplayLayer(DisplayLayer displayLayer, RenderNode renderNode)
        {
            ImagePanel panelToRemove = _imagesPanels.Find(ip => ip.RenderNode.Equals(renderNode));
            displayLayer.Controls.Remove(panelToRemove.Panel);
            _imagesPanels.Remove(panelToRemove);
        }

        public void ClearDisplayLayer()
        {
            if (InvokeRequired)
            {
                clearPanelDelegate = ClearPanelMethod;
                Invoke(clearPanelDelegate, new object[] { DisplayLayer });
            }
            else
            {
                DisplayLayer.Controls.Clear();
            }
            _imagesPanels = new List<ImagePanel>();
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
            DisplayLayer.Controls.Clear();

            // Draw Background Image
            DisplayLayer.BackgroundImage = Image.FromFile(_gameEngine.imagePath + _gameEngine.GetSceneManager().GetCurrentScene().backgroundImage);
            DisplayLayer.BackgroundImageLayout = ImageLayout.Stretch;
            DisplayLayer.Location = new Point(0, 0);
            DisplayLayer.Size = new Size(Width, Height);
            Controls.Add(DisplayLayer);

            Refresh();
        }
    }
}
