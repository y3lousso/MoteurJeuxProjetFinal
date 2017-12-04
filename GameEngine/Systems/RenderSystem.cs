﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Nodes;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class RenderSystem : ISystem
    {        
        private GameEngine _gameEngine;
        private List<EntityNode> _renderEntityNodes;

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        //private Thread renderingThread;
        public bool renderProcessOn;

        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            InitEntities(gameEngine.GetSceneManager().GetCurrentScene().GetEntities());
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
            _gameEngine.GetDisplayWindow().Refresh();
        }

        public void End()
        {
            renderProcessOn = false;
        }

        public bool IsCompatible(Entity entity)
        {
            return entity.GetComponentOfType(typeof(PositionComponent)) != null &&
                   entity.GetComponentOfType(typeof(RenderComponent)) != null;
        }

        public void AddEntity(Entity entity)
        {
            if (IsCompatible(entity))
            {
                RenderNode newRenderNode = new RenderNode
                {
                    positionComponent = (PositionComponent) entity.GetComponentOfType(typeof(PositionComponent)),
                    renderComponent = (RenderComponent) entity.GetComponentOfType(typeof(RenderComponent))
                };
                EntityNode entityNode = new EntityNode
                {
                    Node = newRenderNode,
                    Entity = entity
                };
                _renderEntityNodes.Add(entityNode);
            }
        }

        public void EditEntity(Entity oldEntity, Entity newEntity)
        {
            if (IsCompatible(newEntity) && IsCompatible(oldEntity))
            {
                EntityNode entityNode = _renderEntityNodes.Find(node => node.Entity == oldEntity);
                if (!entityNode.Equals(null))
                {
                    entityNode.Entity = newEntity;
                    entityNode.Node = new RenderNode
                    {             
                        positionComponent = (PositionComponent) newEntity.GetComponentOfType(typeof(PositionComponent)),
                        renderComponent = (RenderComponent) newEntity.GetComponentOfType(typeof(RenderComponent))
                    };
                }
            }
            else if (IsCompatible(newEntity) && !IsCompatible(oldEntity))
            {
                AddEntity(newEntity);
            }
            else if (!IsCompatible(newEntity) && IsCompatible(oldEntity))
            {
                RemoveEntity(oldEntity);
            }        
        }

        public void RemoveEntity(Entity entity)
        {
            EntityNode entityNode = _renderEntityNodes.Find(node => node.Entity == entity);
            _renderEntityNodes.Remove(entityNode);
        }
        
        public void InitEntities(List<Entity> entities)
        {
            _renderEntityNodes = new List<EntityNode>();
            foreach (Entity entity in entities)
            {
                AddEntity(entity);
            }
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
            }

        }
    }
}
