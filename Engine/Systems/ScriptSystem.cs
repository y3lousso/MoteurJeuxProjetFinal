using Engine.Components;
using System.Collections.Generic;

namespace Engine.Systems
{
    internal class ScriptSystem : ISystem
    {
        private GameEngine _gameEngine;
        private List<Entity> _entities;

        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            InitEntities(_gameEngine.GetSceneManager().GetCurrentScene().GetEntities());
        }

        public void Update(float deltaTime)
        {
            foreach (Entity entity in _entities)
            {
                ScriptComponent scriptComponent = (ScriptComponent)entity.GetComponentOfType(typeof(ScriptComponent));
                scriptComponent.Script.Update();
            }
        }

        public void End()
        {
        }

        public bool IsCompatible(Entity entity)
        {
            return entity.GetComponentOfType(typeof(ScriptComponent)) != null;
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

            // Start and register all the scripts
            _gameEngine.GetEventManager().UnregisterAllListeners();
            foreach (Entity entity in _entities)
            {
                ScriptComponent scriptComponent = (ScriptComponent)entity.GetComponentOfType(typeof(ScriptComponent));
                // Set the entity :
                scriptComponent.Script.SetEntity(entity);
                // Start the script :
                scriptComponent.Script.Start(_gameEngine.GetActionManager());
                // Register the script :
                _gameEngine.GetEventManager().RegisterListener(scriptComponent.Script, entity);
            }
        }
    }
}