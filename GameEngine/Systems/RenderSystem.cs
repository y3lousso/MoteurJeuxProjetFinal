using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Nodes;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class RenderSystem : ISystem
    {        
        GameEngine gameEngine;
        public List<RenderNode> _renderNodes = new List<RenderNode>();

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        //private Thread renderingThread;
        public bool renderProcessOn = false;

        public void Start(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
            foreach (Entity entity in gameEngine.GetSceneManager().GetCurrentScene().GetEntities())
            {
                if (entity.GetComponentOfType(typeof(PositionComponent)) != null &&
                    entity.GetComponentOfType(typeof(RenderComponent)) != null)
                {
                    RenderNode newRenderNode = new RenderNode();
                    newRenderNode.positionComponent = (PositionComponent)(entity.GetComponentOfType(typeof(PositionComponent)));
                    newRenderNode.renderComponent = (RenderComponent)(entity.GetComponentOfType(typeof(RenderComponent)));
                    _renderNodes.Add(newRenderNode);
                }
            }
            //renderProcessOn = true;
            //renderingThread = new Thread(RenderingProcess);
            //renderingThread.Start();
        }

        public void Update(float deltaTime)
        {
            gameEngine.GetDisplayWindow().ClearDisplayLayer();
            foreach (RenderNode renderNode in _renderNodes)
            {
                gameEngine.GetDisplayWindow().AddImageToDisplayLayer(gameEngine.GetDisplayWindow().displayLayer, renderNode.positionComponent.position, renderNode.renderComponent.size, renderNode.renderComponent.image);                
            }
            //gameEngine.GetDisplayWindow().Refresh();
        }

        public void End()
        {
            renderProcessOn = false;
        }

        private void RenderingProcess()
        {
            while (renderProcessOn)
            {
                gameEngine.GetDisplayWindow().ClearDisplayLayer();
                foreach (RenderNode renderNode in _renderNodes)
                {
                    gameEngine.GetDisplayWindow().AddImageToDisplayLayer(gameEngine.GetDisplayWindow().displayLayer, renderNode.positionComponent.position, renderNode.renderComponent.size, renderNode.renderComponent.image);
                }
                //gameEngine.GetDisplayWindow().Refresh();
                Thread.Sleep(15);
            }

        }
    }
}
