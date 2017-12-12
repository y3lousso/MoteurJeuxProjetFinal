﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using MoteurJeuxProjetFinal.GameEngine.Components;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class RenderSystem : ISystem
    {        
        
        private struct EntityAndNode
        {
            public Entity Entity;
            public RenderNode RenderNode;
        }
        
        private GameEngine _gameEngine;
        private List<EntityAndNode> _entitiesAndNodes;

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        //private Thread renderingThread;
        public bool renderProcessOn;

        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            InitEntities(gameEngine.GetSceneManager().GetCurrentScene().GetEntities());
        }

        public void Update(float deltaTime)
        {
            bool needToRefresh = false;

            for (int i = 0; i < _entitiesAndNodes.Count; i++)
            {
                // Check if the components have changed :
                EntityAndNode entityAndNode = _entitiesAndNodes[i];
                RenderComponent renderComponent = (RenderComponent) entityAndNode.Entity.GetComponentOfType(typeof(RenderComponent));
                PositionComponent positionComponent = (PositionComponent) entityAndNode.Entity.GetComponentOfType(typeof(PositionComponent));
                if (!positionComponent.position.Equals(entityAndNode.RenderNode.Position) ||
                    !renderComponent.image.Equals(entityAndNode.RenderNode.Image) ||
                    !renderComponent.size.Equals(entityAndNode.RenderNode.Size))
                {
                                        
                    // Redraw the entity
                    RenderNode newRenderNode = CreateRenderNode(entityAndNode.Entity);
                    _gameEngine.GetDisplayWindow().UpdateImageFromDisplayLayer(_gameEngine.GetDisplayWindow().DisplayLayer, entityAndNode.RenderNode, newRenderNode);
                    entityAndNode.RenderNode = newRenderNode;
                    needToRefresh = true;
                }
            }
            if (needToRefresh)
            {
                _gameEngine.GetDisplayWindow().Refresh();
            }
        }

        public void End()
        {
            renderProcessOn = false;
        }

        public bool IsCompatible(Entity entity)
        {
            return entity.GetComponentOfType(typeof(RenderComponent)) != null &&
                   entity.GetComponentOfType(typeof(PositionComponent)) != null;
        }

        public void AddEntity(Entity entity)
        {
            if (IsCompatible(entity))
            {
                RenderNode renderNode = CreateRenderNode(entity);
                _entitiesAndNodes.Add(new EntityAndNode
                {
                    Entity = entity,
                    RenderNode = renderNode
                });
                // Draw the new entity
                _gameEngine.GetDisplayWindow().AddImageToDisplayLayer(_gameEngine.GetDisplayWindow().DisplayLayer, renderNode);                
            }
        }

        public void EditEntity(Entity oldEntity, Entity newEntity)
        {
            if (IsCompatible(newEntity) && IsCompatible(oldEntity))
            {
                int index = _entitiesAndNodes.FindIndex(e => e.Entity == oldEntity);
                if (index != -1)
                {
                    RenderNode renderNode = CreateRenderNode(newEntity);

                    _entitiesAndNodes[index] = new EntityAndNode
                    {
                        Entity = newEntity,
                        RenderNode = renderNode
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
            EntityAndNode entityAndNode = _entitiesAndNodes.Find(e => e.Entity == entity);
            _entitiesAndNodes.Remove(entityAndNode);
            _gameEngine.GetDisplayWindow().RemoveImageFromDisplayLayer(_gameEngine.GetDisplayWindow().DisplayLayer, entityAndNode.RenderNode);
        }

        public void InitEntities(List<Entity> entities)
        {
            _gameEngine.GetDisplayWindow().ClearDisplayLayer();
            _entitiesAndNodes = new List<EntityAndNode>();
            foreach (Entity entity in entities)
            {
                AddEntity(entity);
            }
        }

        private static RenderNode CreateRenderNode(Entity entity)
        {
            // Create the render node
            RenderComponent renderComponent = (RenderComponent) entity.GetComponentOfType(typeof(RenderComponent));
            PositionComponent positionComponent = (PositionComponent) entity.GetComponentOfType(typeof(PositionComponent));
            return new RenderNode
            {
                Image = string.Copy(renderComponent.image),
                Position = new Vector2(positionComponent.position.X, positionComponent.position.Y),
                Size = new Vector2(renderComponent.size.X, renderComponent.size.Y)
            };
        }
    }
}
