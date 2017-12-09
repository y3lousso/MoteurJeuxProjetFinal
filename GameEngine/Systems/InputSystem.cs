﻿using System.Collections.Generic;
using MoteurJeuxProjetFinal.GameEngine.Components;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class InputSystem : ISystem
    {
        
        private GameEngine _gameEngine;
        private List<Entity> _entities;

        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            InitEntities(gameEngine.GetSceneManager().GetCurrentScene().GetEntities());
        }

        public void Update(float deltaTime)
        {
            foreach(Entity entity in _entities)
            {
                InputComponent inputComponent = (InputComponent) entity.GetComponentOfType(typeof(InputComponent));
                // get inputs from input manager then ...               
                inputComponent.inputXY = _gameEngine.GetInputManager().GetInputs().InputXY;
                // apply them as a force to the physic component
                ((PhysicsComponent) entity.GetComponentOfType(typeof(PhysicsComponent)))._forces.Add(inputComponent.inputXY * inputComponent.inputTweaker);
            }
        }

        public void End()
        {
        }

        public bool IsCompatible(Entity entity)
        {
            return entity.GetComponentOfType(typeof(InputComponent)) != null &&
                   entity.GetComponentOfType(typeof(PhysicsComponent)) != null;
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
    }
}
