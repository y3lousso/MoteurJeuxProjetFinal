using Engine.Components;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Engine.Systems
{
    internal class RenderSystem : ISystem
    {
        private GameEngine _gameEngine;

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
            _gameEngine.GetDisplayWindow().UpdateDisplayLayer();
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
                // Draw the new entity
                _gameEngine.GetDisplayWindow().AddEntityInDisplayLayer(entity);
            }
        }

        public void EditEntity(Entity oldEntity, Entity newEntity)
        {
            if (IsCompatible(newEntity) && IsCompatible(oldEntity))
            {
                // TODO handle explicit edition
                int index = -1;//_entities.IndexOf(oldEntity);
                if (index != -1)
                {
                    //_entities[index] = newEntity;
                    // No need to update manually the DisplayWindow, the update will be processed in the Update
                    // of the RenderSystem (the system will detect the changement in the component of the entity)
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
            _gameEngine.GetDisplayWindow().RemoveEntityFromDisplayLayer(entity);
        }

        public void InitEntities(List<Entity> entities)
        {
            _gameEngine.GetDisplayWindow().ClearDisplayLayer();
            foreach (Entity entity in entities)
            {
                AddEntity(entity);
            }
        }
    }
}