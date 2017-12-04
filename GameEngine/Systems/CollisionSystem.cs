﻿using System.Collections.Generic;
using System.Numerics;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Nodes;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    
    class CollisionSystem : ISystem
    {
        private GameEngine _gameEngine;
        private List<EntityNode> _collisionEntityNodes;

        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            InitEntities(gameEngine.GetSceneManager().GetCurrentScene().GetEntities());
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _collisionEntityNodes.Count; i++)
            {
                CollisionNode node1 = (CollisionNode) _collisionEntityNodes[i].Node;
                PositionComponent positionComponent1 = node1.PositionComponent;
                BoxCollisionComponent boxCollisionComponent1 = node1.BoxCollisionComponent;
                for (int j = i+1; j < _collisionEntityNodes.Count; j++)
                {
                    CollisionNode node2 = (CollisionNode) _collisionEntityNodes[j].Node;
                    PositionComponent positionComponent2 = node2.PositionComponent;
                    BoxCollisionComponent boxCollisionComponent2 = node2.BoxCollisionComponent;
                    if (positionComponent1.position.X < positionComponent2.position.X + boxCollisionComponent2.size.X &&
                       positionComponent1.position.X + boxCollisionComponent1.size.X > positionComponent2.position.X &&
                       positionComponent1.position.Y < positionComponent2.position.Y + boxCollisionComponent2.size.Y &&
                       positionComponent1.position.Y + boxCollisionComponent1.size.Y > positionComponent2.position.Y )
                    {
                        // Collision detected
                        PhysicsComponent physicsComponent1 = node1.PhysicsComponent;
                        PhysicsComponent physicsComponent2 = node2.PhysicsComponent;
                        physicsComponent1._forces.Add(new Vector2(0, -20000f));
                        physicsComponent2._forces.Add(new Vector2(0, +20000f));
                        
                        // Create the collision Event and throw it :
                        CollisionEvent gameEvent = new CollisionEvent(
                            _gameEngine.GetSceneManager().GetCurrentScene(),
                            _collisionEntityNodes[i].Entity,
                            node1,
                            _collisionEntityNodes[j].Entity,
                            node2);
                        _gameEngine.GetEventManager().AddEvent(gameEvent);
                    }    
                }
            }
        }

        public void End()
        {
        }

        public bool IsCompatible(Entity entity)
        {
            return entity.GetComponentOfType(typeof(PositionComponent)) != null &&
                   entity.GetComponentOfType(typeof(PhysicsComponent)) != null &&
                   entity.GetComponentOfType(typeof(BoxCollisionComponent)) != null;
        }

        public void AddEntity(Entity entity)
        {
            if (IsCompatible(entity))
            {
                CollisionNode newCollisionNode = new CollisionNode
                {
                    PositionComponent = (PositionComponent) entity.GetComponentOfType(typeof(PositionComponent)),
                    PhysicsComponent = (PhysicsComponent) entity.GetComponentOfType(typeof(PhysicsComponent)),
                    BoxCollisionComponent = (BoxCollisionComponent) entity.GetComponentOfType(typeof(BoxCollisionComponent))
                };
                EntityNode entityNode = new EntityNode
                {
                    Node = newCollisionNode,
                    Entity = entity
                };
                _collisionEntityNodes.Add(entityNode);
            }
        }

        public void EditEntity(Entity oldEntity, Entity newEntity)
        {
            if (IsCompatible(newEntity) && IsCompatible(oldEntity))
            {
                EntityNode entityNode = _collisionEntityNodes.Find(node => node.Entity == oldEntity);
                if (!entityNode.Equals(null))
                {
                    entityNode.Entity = newEntity;
                    entityNode.Node = new CollisionNode
                    {
                        PositionComponent = (PositionComponent) newEntity.GetComponentOfType(typeof(PositionComponent)),
                        PhysicsComponent = (PhysicsComponent) newEntity.GetComponentOfType(typeof(PhysicsComponent)),
                        BoxCollisionComponent = (BoxCollisionComponent) newEntity.GetComponentOfType(typeof(BoxCollisionComponent))
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
            EntityNode entityNode = _collisionEntityNodes.Find(node => node.Entity == entity);
            _collisionEntityNodes.Remove(entityNode);
        }

        public void InitEntities(List<Entity> entities)
        {
            _collisionEntityNodes = new List<EntityNode>();
            foreach (Entity entity in entities)
            {
                AddEntity(entity);
            }
        }
    }
}
