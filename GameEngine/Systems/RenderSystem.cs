﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MoteurJeuxProjetFinal.GameEngine.Components;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class RenderSystem : ISystem
    {        
        private GameEngine _gameEngine;
        private List<Entity> _entities;

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
            foreach (Entity entity in _entities)
            {
                RenderComponent renderComponent = (RenderComponent) entity.GetComponentOfType(typeof(RenderComponent));
                PositionComponent positionComponent = (PositionComponent) entity.GetComponentOfType(typeof(PositionComponent));
                _gameEngine.GetDisplayWindow().AddImageToDisplayLayer(_gameEngine.GetDisplayWindow().displayLayer, positionComponent.position, renderComponent.size, renderComponent.image);                
            }
            _gameEngine.GetDisplayWindow().Refresh();
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
                _entities.Add(entity);
            }
        }

        public void EditEntity(Entity oldEntity, Entity newEntity)
        {
            if (IsCompatible(newEntity) && IsCompatible(oldEntity))
            {
                int index = _entities.IndexOf(oldEntity);
                if (index != -1)
                {
                    _entities[index] = newEntity;
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
            _entities.Remove(entity);
        }

        public void InitEntities(List<Entity> entities)
        {
            _entities = new List<Entity>();
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
                foreach (Entity entity in _entities)
                {
                    RenderComponent renderComponent = (RenderComponent) entity.GetComponentOfType(typeof(RenderComponent));
                    PositionComponent positionComponent = (PositionComponent) entity.GetComponentOfType(typeof(PositionComponent));
                    _gameEngine.GetDisplayWindow().AddImageToDisplayLayer(_gameEngine.GetDisplayWindow().displayLayer, positionComponent.position, renderComponent.size, renderComponent.image);                
                }
                //_gameEngine.GetDisplayWindow().Refresh();
            }

        }
    }
}
