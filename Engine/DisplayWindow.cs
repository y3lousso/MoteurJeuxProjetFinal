using Engine.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

namespace Engine
{
    public class DisplayWindow : Form
    {
        private GameEngine _gameEngine;

        public DisplayLayer DisplayLayer = new DisplayLayer();

        private List<EntityPanel> _entityPanels = new List<EntityPanel>();

        public delegate void AddPanelDelegate(Panel mainPanel, Panel panelToAdd);

        private AddPanelDelegate addPanelDelegate;

        public delegate void ClearPanelDelegate(Panel panel);

        private ClearPanelDelegate clearPanelDelegate;

        public delegate void RemovePanelDelegate(Panel mainPanel, Panel panelToRemove);

        private RemovePanelDelegate removePanelDelegate;

        /// <summary>
        /// A structure to associate the entity (the usefull informations) and the panel rendered
        /// </summary>
        private struct EntityPanel
        {
            public Panel Panel;
            public Entity Entity;
            public string Image;
            public Vector2 Size;
            public Vector2 Position;
        }

        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
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

        public void AddEntityInDisplayLayer(Entity entityToAdd)
        {
            RenderComponent renderComponent = (RenderComponent)entityToAdd.GetComponentOfType(typeof(RenderComponent));
            PositionComponent positionComponent = (PositionComponent)entityToAdd.GetComponentOfType(typeof(PositionComponent));

            // Create a panel
            Panel panelToAdd = new Panel
            {
                BackgroundImage = Image.FromFile(_gameEngine.imagePath + renderComponent.image),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent,
                Location = new Point((int)positionComponent.position.X, (int)positionComponent.position.Y),
                Size = new Size((int)renderComponent.size.X, (int)renderComponent.size.Y),
                TabIndex = 0
            };
            panelToAdd.Click += OnImageClick;

            // Add the panel
            if (InvokeRequired)
            {
                addPanelDelegate = AddPanelMethod;
                Invoke(addPanelDelegate, DisplayLayer, panelToAdd);
            }
            else
            {
                DisplayLayer.Controls.Add(panelToAdd);
            }
            // Store the Panel and the Entity :
            _entityPanels.Add(new EntityPanel
            {
                Panel = panelToAdd,
                Entity = entityToAdd,
                Image = renderComponent.image,
                Size = renderComponent.size,
                Position = positionComponent.position
            });
        }

        public void UpdateDisplayLayer()
        {
            for (int i = 0; i < _entityPanels.Count; i++)
            {
                EntityPanel entityPanel = _entityPanels[i];

                RenderComponent renderComponent = (RenderComponent)entityPanel.Entity.GetComponentOfType(typeof(RenderComponent));
                PositionComponent positionComponent = (PositionComponent)entityPanel.Entity.GetComponentOfType(typeof(PositionComponent));

                if (!entityPanel.Position.Equals(positionComponent.position) ||
                    !entityPanel.Image.Equals(renderComponent.image) ||
                    !entityPanel.Size.Equals(renderComponent.size))
                {
                    entityPanel.Panel.BackgroundImage = Image.FromFile(_gameEngine.imagePath + renderComponent.image);
                    entityPanel.Panel.Location = new Point((int)positionComponent.position.X, (int)positionComponent.position.Y);
                    entityPanel.Panel.Size = new Size((int)renderComponent.size.X, (int)renderComponent.size.Y);

                    entityPanel.Image = renderComponent.image;
                    entityPanel.Size = renderComponent.size;
                    entityPanel.Position = positionComponent.position;
                }
            }
        }

        public void RemoveEntityFromDisplayLayer(Entity entityToRemove)
        {
            // Remove the render node and the panel
            EntityPanel panelToRemove = _entityPanels.Find(ep => ep.Entity.Equals(entityToRemove));
            if (InvokeRequired)
            {
                removePanelDelegate = AddPanelMethod;
                Invoke(removePanelDelegate, DisplayLayer, panelToRemove);
            }
            else
            {
                DisplayLayer.Controls.Remove(panelToRemove.Panel);
            }
            _entityPanels.Remove(panelToRemove);
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
            _entityPanels = new List<EntityPanel>();
        }

        private static void ClearPanelMethod(Panel displayLayer)
        {
            displayLayer.Controls.Clear();
        }

        private static void AddPanelMethod(Panel mainPanel, Panel panelToAdd)
        {
            mainPanel.Controls.Add(panelToAdd);
        }

        public Form GetForm()
        {
            return this;
        }

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

        private void OnImageClick(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;

            EntityPanel entityPanel = _entityPanels.Find(ep => ep.Panel == panel);
            _gameEngine.GetEventManager().AddEvent(new EntityClickEvent(entityPanel.Entity));
        }
    }
}