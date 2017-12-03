﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Nodes;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class RenderSystem : ISystem
    {        
        private GameEngine _gameEngine;
        private List<EntityNode> _renderEntityNodes = new List<EntityNode>();

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        //private Thread renderingThread;
        public bool renderProcessOn;

        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            foreach (Entity entity in _gameEngine.GetSceneManager().GetCurrentScene().GetEntities())
            {
                if (entity.GetComponentOfType(typeof(PositionComponent)) != null &&
                    entity.GetComponentOfType(typeof(RenderComponent)) != null)
                {
                    RenderNode newRenderNode = new RenderNode();
                    newRenderNode.positionComponent = (PositionComponent)(entity.GetComponentOfType(typeof(PositionComponent)));
                    newRenderNode.renderComponent = (RenderComponent)(entity.GetComponentOfType(typeof(RenderComponent)));
                    EntityNode entityNode = new EntityNode
                    {
                        Node = newRenderNode,
                        Entity = entity
                    };
                    _renderEntityNodes.Add(entityNode);
                }
            }
            //renderProcessOn = true;
            //renderingThread = new Thread(RenderingProcess);
            //renderingThread.Start();
        }

        public void Update(float deltaTime)
        {
            _gameEngine.GetDisplayWindow().ClearDisplayLayer();
            foreach (EntityNode renderEntityNode in _renderEntityNodes)
            {
                RenderNode renderNode = (RenderNode) renderEntityNode.Node;
                _gameEngine.GetDisplayWindow().AddImageToDisplayLayer(_gameEngine.GetDisplayWindow().displayLayer, renderNode.positionComponent.position, renderNode.renderComponent.size, renderNode.renderComponent.image);                
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
                _gameEngine.GetDisplayWindow().ClearDisplayLayer();
                foreach (EntityNode renderEntityNode in _renderEntityNodes)
                {
                    RenderNode renderNode = (RenderNode) renderEntityNode.Node;
                    _gameEngine.GetDisplayWindow().AddImageToDisplayLayer(_gameEngine.GetDisplayWindow().displayLayer, renderNode.positionComponent.position, renderNode.renderComponent.size, renderNode.renderComponent.image);
                }
                //gameEngine.GetDisplayWindow().Refresh();
                Thread.Sleep(15);
            }

        }
    }
}
