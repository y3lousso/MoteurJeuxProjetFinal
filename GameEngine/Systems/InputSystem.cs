﻿﻿using System.Collections.Generic;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Nodes;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class InputSystem : ISystem
    {
        
        private GameEngine _gameEngine;
        private List<EntityNode> _inputEntityNodes = new List<EntityNode>();

        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            foreach (Entity entity in _gameEngine.GetSceneManager().GetCurrentScene().GetEntities())
            {
                AddEntity(entity);
            }
        }

        public void Update(float deltaTime)
        {
            foreach(EntityNode inputEntityNode in _inputEntityNodes)
            {
                InputNode inputNode = (InputNode) inputEntityNode.Node;
                // get inputs from input manager then ...               
                inputNode.inputComponent.inputXY = _gameEngine.GetInputManager().GetInputs().InputXY;
                // apply them as a force to the physic component
                inputNode.physicsComponent._forces.Add(inputNode.inputComponent.inputXY * inputNode.inputComponent.inputTweaker);
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
                InputNode newInputNode = new InputNode
                {
                    inputComponent = (InputComponent) entity.GetComponentOfType(typeof(InputComponent)),
                    physicsComponent = (PhysicsComponent) entity.GetComponentOfType(typeof(PhysicsComponent))
                };
                EntityNode entityNode = new EntityNode
                {
                    Node = newInputNode,
                    Entity = entity
                };
                _inputEntityNodes.Add(entityNode);
            }
            
        }

        public void EditEntity(Entity oldEntity, Entity newEntity)
        {
            if (IsCompatible(newEntity) && IsCompatible(oldEntity))
            {
                EntityNode entityNode = _inputEntityNodes.Find(node => node.Entity == oldEntity);
                if (!entityNode.Equals(null))
                {
                    entityNode.Entity = newEntity;
                    entityNode.Node = new InputNode
                    {
                        inputComponent = (InputComponent) newEntity.GetComponentOfType(typeof(InputComponent)),
                        physicsComponent = (PhysicsComponent) newEntity.GetComponentOfType(typeof(PhysicsComponent))
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
            EntityNode entityNode = _inputEntityNodes.Find(node => node.Entity == entity);
            if (!entityNode.Equals(null))
            {
                _inputEntityNodes.Remove(entityNode);
            }        
        }
    }
}
